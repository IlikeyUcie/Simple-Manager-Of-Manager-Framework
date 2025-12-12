using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptObject/GameSoundGroupData",fileName = "New GameSoundGroup")]
public class GameSoundGroupDataSO : ScriptableObject
{
    //声明音效组名字
    public string groupName;
    //声明音效List
    public List<GameSound> gameSounds;
    //声明音效Dictionary
    public Dictionary<string, AudioClip> gameSound;

    /// <summary>
    ///初始化方法 
    /// </summary>
    public void Init()
    {
        //列表初始化
        gameSounds = new List<GameSound>();
        //将列表转化为Dictionary
        gameSound = gameSounds.GroupBy(s => s.soundName).ToDictionary(
            g => g.Key,
            g => g.First().audioClip
            );
    }
    /// <summary>
    /// 这是获取音效的方法
    /// </summary>
    /// <param name="soundName">需要获取音效的音效名</param>
    /// <returns>音效名对应的音效</returns>
    public AudioClip Get(string soundName)
    {
        if (gameSound.ContainsKey(soundName))
            return gameSound[soundName];
        Debug.Log("该群组未找到该音效名");
        return null;
    }
}
[Serializable]
/// <summary>
/// 这是音效的最小单位，总共包含音效的名字以及音效本身
/// </summary>
public class GameSound
{
   public string soundName;
    public AudioClip audioClip;
}
