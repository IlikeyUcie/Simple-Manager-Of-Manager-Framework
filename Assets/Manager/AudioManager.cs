using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager :  MonoSingleton<AudioManager>
{
    //声明三个音源，分别代表，背景音乐，UI音效，场景音效
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceUI;
    public AudioSource audioSourceScene;
    //是否需要淡入或淡出的bool
    private bool fadingBGM;
    //初始化方法
    public void Init(AudioSource BGM, AudioSource UI, AudioSource Scene)
    {
        audioSourceBGM = BGM;
        audioSourceUI = UI;
        audioSourceScene = Scene;
    }
    /// <summary>
    /// UI音效的播放方法
    /// </summary>
    /// <param name="soundName">需要播放的UI音效名</param>
    /// <param name="volume">播放时的音量</param>
    public void PlayUIEffect(string soundName, float volume = 1f)
    {
        if (audioSourceUI != null)
        {
            AudioClip clip = GameManager.Instance.gameSoundGroupData["UI"].Get(soundName);
            if (clip != null)
            {
                audioSourceUI.volume = volume;
                audioSourceUI.PlayOneShot(clip);
            }
        }
    }
    /// <summary>
    /// 任意音效的播放方法
    /// </summary>
    /// <param name="audioSource">音源</param>
    /// <param name="groupName">音效组名</param>
    /// <param name="soundName">音效名</param>
    /// <param name="volume">音效音量</param>
    public void PlayEffect(AudioSource audioSource, string groupName, string soundName, float volume = 1f)
    {
        if (audioSource != null)
        {
            //判断GameManager中的音效组内有没有包含输入的音效组
            if (GameManager.Instance.gameSoundGroupData.ContainsKey(groupName))
            {
                AudioClip clip = GameManager.Instance.gameSoundGroupData[groupName].Get(soundName);
                if (clip != null)
                {
                    audioSource.volume = volume;
                    audioSource.PlayOneShot(clip);
                }
            }
        }
    }
    /// <summary>
    /// BGM播放方法
    /// </summary>
    /// <param name="soundName">BGM音乐名</param>
    public void PlayBGMEffect(string soundName)
    {
        //判断音源是否存在
        if (audioSourceBGM == null)
            return;
        AudioClip clip = GameManager.Instance.gameSoundGroupData["BGM"].Get(soundName);
        //判断新的播片是否存在
        if (clip == null)
            return;
        //判断正在进行的播片是否与新输入的播片是否相同
        if (audioSourceBGM.isPlaying && audioSourceBGM.clip == clip)
            return;
        //上面都没问题，下面就安全启动
        audioSourceBGM.Stop();
        audioSourceBGM.volume = 0;
        audioSourceBGM.clip = clip;
        audioSourceBGM.Play();
        //播片需要循环
        audioSourceBGM.loop = true;
        fadingBGM = true;

    }
    /// <summary>
    /// BGM淡入淡出方法
    /// </summary>
    private void ProgressBGM()
    {
        if (fadingBGM)
        {
            audioSourceBGM.volume += Time.deltaTime;
            if (audioSourceBGM.volume >= 1)
            {
                audioSourceBGM.volume = 1f;
                fadingBGM = false;
            }
        }
        else
            // 场景切换背景音乐淡出，现在还没做所以先return
            return;
    }
}
