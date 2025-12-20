using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoSingleton<TimeScaleManager>
{
    //这是防止重复改变时间的bool变量，true代表已经改变过且未恢复，false代表正常scale
    private bool alreadyScale;
    /// <summary>
    /// 改变Scale的第一个异步方法，此方法是判断是否需要永久改变TimeScale
    /// </summary>
    /// <param name="timeSacle">需要改变的TimeScale大小</param>
    /// <param name="duration">需要改变TimeScale的时间，当它为-1时则是永久改变</param>
    public async void ScaleTime(float timeSacle, float duration)
    {
        //判断是否已经改变Scale
        if (alreadyScale == true)
            return;
        //判断是否是永久改变
        if (duration == -1)
        {
            Time.timeScale = timeSacle;
            alreadyScale = true;
            return;
        }
        //如果不是永久改变，则进入异步，具体根据时间延迟改变Scale
        await UniTask.Create(async () => await ScaleTimeAsync(timeSacle, duration));
    }
    /// <summary>
    /// 根据延迟改变timeScale的异步方法
    /// </summary>
    /// <param name="timeScale">需要改变的TimeScale大小</param>
    /// <param name="duration">需要改变TimeScale的时间</param>
    /// <returns></returns>
    private async UniTask ScaleTimeAsync(float timeScale, float duration)
    {
        Time.timeScale = timeScale;
        alreadyScale = true;
        //设立延迟
        await UniTask.Delay((int)duration * 1000, ignoreTimeScale: true);
        ResetAlreadyScale();
    }
    //重置Scale的方法
    public void ResetAlreadyScale()
    {
        Time.timeScale = 1;
        alreadyScale = false;
    }
}
