using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>
{
    // 储存事件的字典
    public Dictionary<string, IGameEvent> eventGroup = new Dictionary<string, IGameEvent>();
    
    /// <summary>
    /// 注册事件的方法
    /// </summary>
    /// <param name="name">需要注册的事件名</param>
    /// <param name="gameEvent">需要注册的事件类型</param>
    public void Rigester(string name, IGameEvent gameEvent)
    {
        if (string.IsNullOrEmpty(name) || gameEvent == null)
            return;
        if(!eventGroup.TryGetValue(name,out gameEvent))
        { Debug.Log("字典已经包含该事件！");return; }    
        eventGroup[name] = gameEvent;
    }
    /// <summary>
    /// 注销事件的方法
    /// </summary>
    /// <param name="name">需要注销的事件名</param>
    public void UnRigester(string name)
    {
        if (string.IsNullOrEmpty(name))
            return;
        eventGroup.Remove(name);
    }
    /// <summary>
    /// 广播事件的方法
    /// </summary>
    /// <param name="name">需要广播的事件名</param>
    /// <param name="param">需要传入的参数类型</param>
    public void Broadcast(string name, IGameEventParameter param)
    {
        if (string.IsNullOrEmpty(name) || param == null)
            return;
        eventGroup[name].invoke(param);
    }
}
