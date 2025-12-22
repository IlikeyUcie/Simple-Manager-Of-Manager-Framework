using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此脚本为事件传入参数类型的数据脚本，分别由一个参数，两个参数，三个参数的类型
public class GameEventParameter<T1>:IGameEventParameter
{
    public T1 param1;
    public GameEventParameter(T1 param1)
    {
        this.param1 = param1;
    }
}
public class GameEventParameter<T1,T2> : IGameEventParameter
{
    public T1 param1;
    public T2 param2;
    public GameEventParameter(T1 param1,T2 param2)
    {
        this.param1 = param1;
        this.param2 = param2;
    }
}
public class GameEventParameter<T1,T2,T3> : IGameEventParameter
{
    public T1 param1;
    public T2 param2;
    public T3 param3;
    public GameEventParameter(T1 param1, T2 param2, T3 param3)
    {
        this.param1 = param1;
        this.param2 = param2;
        this.param3 = param3;
    }
}
/// <summary>
///  事件传入参数类型的接口
/// </summary>
public interface IGameEventParameter
{ }
