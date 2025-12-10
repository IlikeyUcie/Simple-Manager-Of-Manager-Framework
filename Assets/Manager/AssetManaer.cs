using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AssetManaer : MonoSingleton<AssetManaer>
{
    protected override void Awake()
    {
        base.Awake();
        //继承单例中的awake
    }
    //声明字典类型的缓存
    private readonly Dictionary<string, Object> _cache = new Dictionary<string, Object>();
    /// <summary>
    /// 主体加载资源方法，本质上是对Resource.LoadAsync<T>()方法的改写拓展
    /// </summary>
    /// <typeparam name="T">
    /// 需要加载的资源类型
    /// </typeparam>
    /// <param name="path">
    /// 需要读取的资源路径
    /// </param>
    /// <param name="OnComplete">
    /// 需要进行回调函数
    /// </param>
    /// <param name="OnProgress">
    /// 体现过程进度的委托
    /// </param>
    /// <param name="OnError">
    /// 返回错误报错的委托
    /// </param>
    /// <returns>
    /// 便于中断协程的Coroutine句柄管理返回值
    /// </returns>
    public Coroutine LoadAsync<T>(string path, UnityAction<T> OnComplete = null,
        UnityAction<float> OnProgress = null, UnityAction<string> OnError = null) where T : Object
    {
        return StartCoroutine(LoadRoutine<T>(path, OnComplete, OnProgress, OnError));
    }
    //协程主体方法
    private IEnumerator LoadRoutine<T>(string path, UnityAction<T> OnComplete,
        UnityAction<float> OnProgress, UnityAction<string> OnError) where T : Object
    {
        //判断缓存中是否以及存在该资源，如果存在，且拥有回调函数则直接把资源传入委托函数并调用。最后退出方法。
        if (_cache.TryGetValue(path, out Object cached))
        {
            OnComplete?.Invoke(cached as T);
            yield break;
        }
        //进行资源加载
        ResourceRequest resourceRequest = Resources.LoadAsync<T>(path);
        //判断路径是否存在
        if (resourceRequest == null)
        {
            OnError?.Invoke($"{path}路径不存在！");
            yield break;
        }
        //进度读取
        while (!resourceRequest.isDone)
        {
            OnProgress?.Invoke(resourceRequest.progress);
            yield return null;
        }
        //结果处理
        T asset = resourceRequest.asset as T;
        //判断资源是否与类型匹配
        if (asset == null)
            OnError?.Invoke($"读取类型与获得类型不匹配！");
        else
        {
            //加入缓存
            _cache[path] = asset;
            OnComplete?.Invoke(asset);
        }
    }
    /// <summary>
    /// 同步加载资源方法
    /// </summary>
    /// <typeparam name="T">
    /// 资源加载类型
    /// </typeparam>
    /// <param name="path">
    /// 资源加载路径
    /// </param>
    /// <returns>
    /// 资源返回值
    /// </returns>
    public T Load<T>(string path)where T:Object
    {
        if (_cache.TryGetValue(path, out Object cached))
            return cached as T;
        T asset = Resources.Load<T>(path);
        if (asset != null)
            _cache[path] = asset;
        return asset;        
    }
    /// <summary>
    /// 清除缓存方法，没有传入地址调用该方法将直接清除缓存内所有资源
    /// </summary>
    /// <param name="path">
    /// 需要清除缓存的地址
    /// </param>
    public void CacheClear(string path = null)
    {
        if (path == null)
            _cache.Clear();
        else
            _cache.Remove(path);
    }
    /// <summary>
    /// 异步加载资源的静态方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="OnComplete"></param>
    /// <param name="OnProgress"></param>
    /// <param name="OnError"></param>
    /// <returns></returns>
    public static Coroutine LoadAsyncStatic<T>(string path, UnityAction<T> OnComplete = null,
        UnityAction<float> OnProgress = null, UnityAction<string> OnError = null)where T : Object
    {
        //如果有实例则调用LoadAsync方法并返回其返回值，如果没有实例返回null
        return Instance != null ? Instance.LoadAsync(path, OnComplete, OnProgress, OnError) : null;
    }
}
