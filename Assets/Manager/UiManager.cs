using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;



public class UiManager : MonoSingleton<UiManager>
{
    //声明panel数量属性，绑定Panel缓存内的Panel总数数量
    public int panelCount => panelDic.Count;
    //声明Panel缓存
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    //声明可能会用到的mainCanvas变量
    private MainCavans mainCavans;
    //声明可能会用到的mainCavansTrans变量
    private Transform mainCavansTrans;
    
    protected override void Awake()
    {
        //初始化mainCanvas，寻找场景中只存在一个的MainCanvas的单例
        mainCavans = GameObject.FindFirstObjectByType<MainCavans>();
        //绑定它的transform
        mainCavansTrans = mainCavans.panelsParent;
        base.Awake();
    }
    /// <summary>
    /// 显示面板UI的方法
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    /// <returns>面板的类型</returns>
    public async UniTask<T> ShowPanel<T>() where T: BasePanel
    {
        //异步获取资源
        T panel = await GetPanel<T>();
        //进行渐进显示操作
        panel.Show();
        return panel;
    }
    /// <summary>
    /// 隐藏面板方法
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    /// <param name="callBack">隐藏面板后需要做的操作</param>
    /// <param name="isFade">是隐藏还是直接删除</param>
    public void HidePanel<T>(Action callBack,bool isFade = true)where T:BasePanel
    {
        //以T的类型名作为缓存中的Key
        string panelName = typeof(T).Name;
        //判断缓存中是否存在该面板
        if (panelDic.ContainsKey(panelName))
        {
            //是否需要隐藏
            if (isFade)
            {
                //执行隐藏操作，并且在隐藏完毕后，使面板不可见，并且执行callback操作
                panelDic[panelName].Hide(() =>
                {
                    panelDic[panelName].gameObject.SetActive(false);
                    callBack?.Invoke();
                });
            }
            //无需隐藏，直接摧毁面板
            else
            {
                Destroy(panelDic[panelName].gameObject);
                //缓存移除该面板
                panelDic.Remove(panelName);
                //释放异步加载过程中的缓存
                Addressables.Release(panelName);
            }
        }
    }
    /// <summary>
    /// 非加载获取面板，通过下面的RegistPanel方法使场景中本来存在的面板加入到缓存，然后通过这个方法来获取面板
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    /// <returns>面板的类型</returns>
    public T GetPanelWithoutLoad<T>()where T:BasePanel
    {
        //以T的类型名作为缓存中的Key
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].gameObject.SetActive(true);
            return panelDic[panelName] as T ;
        }
        //如果缓存中没有则返回null
        return null;
    }
    /// <summary>
    /// 获取面板的方法
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    /// <returns>获取的面板类型</returns>
    public async UniTask<T> GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].gameObject.SetActive(true);
            return panelDic[panelName] as T;
        }
        //创建一个GameObject类型的变量，使用LoadPanelAsync进行异步加载
        GameObject panelObj = await LoadPanelAsync(panelName);
        //加载完毕后，在场景中创建该GameObject
        Instantiate(panelObj);
        //设置该面板GameObject为MainPanel的子对象
        panelObj.transform.SetParent(mainCavansTrans, false);
        //获取该面板GameObject中的面板脚本
        T panel = panelObj.GetComponent<T>();
        //写入缓存方便下次使用
        panelDic[panelName] = panel as T;
        //返回该脚本
        return panel;

    }
    /// <summary>
    /// 检查面板是否显示的方法
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    /// <returns>返回的Bool值</returns>
    public bool Isshow<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            //直接返回BasePanel中的面板显示判断变量
            return panelDic[panelName].isShow;
        }
        return false;
    }
    /// <summary>
    /// 注册场景中本来就存在，无需加载的面板于缓存
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    /// <param name="panel">场景中存在的面板脚本</param>
    /// <param name="show">是否需要显示</param>
    public void RegistPanel<T>( T panel,bool show = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        //判断该面板是否已经在缓存中
        if (!panelDic.ContainsKey(panelName))
        {
            if (show) panel.Show();
            panelDic[panelName] = panel;
        }
    }
    /// <summary>
    /// 使用Addressable进行异步加载的方法
    /// </summary>
    /// <param name="panelName">需要加载的资源名</param>
    /// <returns>返回该资源的GameObject类型实例</returns>
    private async UniTask<GameObject> LoadPanelAsync(string panelName)
    {
        //异步加载
        var handle = Addressables.LoadAssetAsync<GameObject>(panelName);
        //等待加载
        await handle.Task;
        //返回结果
        return handle.Result;
    }
}
