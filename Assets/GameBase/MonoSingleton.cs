using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private T _instance;
    public T Istance
    {
        get
        {
            _instance ??= FindObjectOfType<T>() ?? CreateInstance(); 
            // 如果_instance为空，则寻找场景中的该实例，并给予赋值，如果没有该类型实例，则创建一个新实例。
            return _instance;        
        }
    }
    protected virtual void Awake()
    {
        if ( _instance != null && _instance != this)// 如果场景中已经有实例并且该实例不是本实例。
        {
            Destroy(gameObject);
            return;
        }
        _instance = this as T;// 如果本实例的派生类或者类型是T，那就转化为T类型并进行赋值
        DontDestroyOnLoad(gameObject);
    }
    
    
       /// <summary>
       /// 在场景中创造游戏对象并实例化单例的方法。
       /// </summary>
       /// <returns>
       /// 场景下实例化的单例。
       /// </returns>
    T CreateInstance()
    {
        T instance;
        GameObject newGameObject = new GameObject(typeof(T).Name);
        instance = newGameObject.AddComponent<T>();
        DontDestroyOnLoad(newGameObject);
        return instance;
    }
}
