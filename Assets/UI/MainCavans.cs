using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCavans : MonoSingleton<MainCavans>
{
    //这是在MainCanvas上的遮罩层，用来渐出UI或场景
    public CanvasGroup blackPanel;
    //这是渐出UI或场景的总时长
    private float fadeTime = 2f;
    //这是判断是否需要渐出UI或场景的Bool变量
    public bool startFade;
    //这是MainCanvas下的Panel需要找到的父节点位置，需要手动进行初始化
    public Transform panelsParent;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        //这个if语句用来判断是否需要进行渐出处理
        if (startFade && blackPanel.alpha > 0)
        {
            //渐出过程
            blackPanel.alpha -= Time.unscaledDeltaTime / fadeTime;
            //渐出过程防止激光触发
            blackPanel.blocksRaycasts = true;
            if (blackPanel.alpha <= 0)
            {
                //结束渐出
                startFade = false;

            }
        }
    }
    //需要渐出的提前准备
    public void StartFade() => startFade = true;
    //需要渐出的提前准备
    public void PrepareFade() => blackPanel.alpha = 1f;
}
