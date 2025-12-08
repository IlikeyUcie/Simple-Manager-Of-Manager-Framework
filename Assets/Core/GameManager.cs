using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    // 创建各Manager字段。
    public InputManager Input;
    public AudioManager Audio;
    public UiManager Ui;
    public SceneLoader SceneLoader;
    public SaveManager Save;
    public EventManager Event;
    public AssetManaer Asset;
    protected override void Awake()
    {
        //实例化各Manager。
        Input.GetOrAdd<InputManager>();
        Audio.GetOrAdd<AudioManager>();
        Ui.GetOrAdd<UiManager>();
        SceneLoader.GetOrAdd<SceneLoader>();
        Save.GetOrAdd<SaveManager>();
        Event.GetOrAdd<EventManager>();
        Asset.GetOrAdd<AssetManaer>();
        //这里遍历了GameManager子对象中的所有MonoBehaviour进行初始化操作。
        foreach (var manager in GetComponentsInChildren<MonoBehaviour>())
        {
            //这里判断，如果遍历的MonoBehaviour对象中找到了继承了Imanager接口的对象则使用接口中的init方法。
            if (manager is IManager im) im.Init();
        }
        //使用单例中的awake方法，防止非惰性实例化造成的单例引用为空。
        base.Awake();
    }
    /// <summary>
    /// 这个方法是退出游戏的方法，在编辑器模式直接暂停游戏，在打包后使用Application.Quit()直接退出程序。
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else  
        Application.Quit();   
#endif
    }

    /// <summary>
    /// 该方法是为Manager引用或者创建实例所创建的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>
    /// 返回的是GameManager子对象中的MonoBehaviour类型的Manager实例
    /// </returns>
    private T GetOrAdd<T>() where T : MonoBehaviour
    {
        var t = GetComponentInChildren<T>();
        if (t != null) return t;
        GameObject go = new GameObject($"[{typeof(T).Name}]");
        go.transform.SetParent(transform);
        t = go.AddComponent<T>();
        return t;
    }
    /// <summary>
    /// 表示Manager所需要继承的接口
    /// </summary>
    interface IManager
    {
        void Init();
    }
}
