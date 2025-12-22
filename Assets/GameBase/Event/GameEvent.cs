using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 这是一个说明事件内部逻辑的脚本
// 在这只注释传入一个参数的事件类

/// <summary>
/// 这是一个传入一个参数的事件
/// </summary>
/// <typeparam name="T1">需要传入的参数类型</typeparam>
public class GameEvent<T1> : IGameEvent
{
    // 需要设立的事件回调委托
    Action<T1> callBack;
    public GameEvent(Action<T1> action)
    {
        this.callBack = action;
    }
    public void invoke(IGameEventParameter param)
    {
        if (param == null)
            return;
        // 将传入的接口转化为正确的类型
        GameEventParameter<T1> Param = param as GameEventParameter<T1>;
        //启用该委托，包含在内的所有回调函数
        callBack?.Invoke(Param.param1);
    }
    // 为委托添加新回调函数的方法
    public void AddListener(Action<T1> action)
    {
        this.callBack += action;
    }
    // 为委托移除回调函数的方法
    public void RemoveListener(Action<T1> action)
    {
        this.callBack -= action;
    }
}
public class GameEvent<T1, T2> : IGameEvent
{
    Action<T1, T2> towCallBack;
    public GameEvent(Action<T1, T2> action)
    {
        this.towCallBack = action;
    }
    public void invoke(IGameEventParameter param)
    {
        if (param == null)
            return;
        GameEventParameter<T1, T2> Param = param as GameEventParameter<T1, T2>;
        towCallBack?.Invoke(Param.param1, Param.param2);
    }
    public void AddListener(Action<T1, T2> action)
    {
        this.towCallBack += action;
    }
    public void RemoveListener(Action<T1, T2> action)
    {
        this.towCallBack -= action;
    }
}
public class GameEvent<T1, T2, T3> : IGameEvent
{
    Action<T1, T2, T3> thirCallBack;
    public GameEvent(Action<T1, T2, T3> action)
    {
        this.thirCallBack = action;
    }
    public void invoke(IGameEventParameter param)
    {
        if (param == null)
            return;
        GameEventParameter<T1, T2, T3> Param = param as GameEventParameter<T1, T2, T3>;
        thirCallBack?.Invoke(Param.param1, Param.param2, Param.param3);
    }
    public void AddListener(Action<T1, T2, T3> action)
    {
        this.thirCallBack += action;
    }
    public void RemoveListener(Action<T1, T2, T3> action)
    {
        this.thirCallBack -= action;
    }
}
/// <summary>
/// 代表所有事件类型的接口
/// </summary>
public interface IGameEvent
{
    public void invoke(IGameEventParameter param);
    // public void AddListener(Action );
    // public void RemoveListener();
}
