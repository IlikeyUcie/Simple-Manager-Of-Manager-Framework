using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //判断面板是否出现的bool变量
    public bool isShow;
    //面板渐进或者渐出的速度
    private float fadeSpeed = 10f;
    //本面板的CanvasGroup组件
    public CanvasGroup canvasGroup;
    //在执行Hide后传入的回调函数
    public Action hideCallBack;
    protected virtual void Awake()
    {
        //绑定CanvasGroup组件，如果没有则创建并绑定
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        //面板初始化
        Init();
    }
    //可重写的初始化方法
    protected abstract void Init();
    //显示面板的方法，将面板状态变量置为true，并将CanvasGroup的alpha值置为0
    public void Show()
    {
        isShow = true;
        canvasGroup.alpha = 0f;

    }
    /// <summary>
    /// 隐藏面板的方法
    /// </summary>
    /// <param name="CallBack">隐藏后需要回调的函数</param>
    public void Hide(Action CallBack)
    {
        //将面板状态变量置为False
        isShow = false;
        //将CanvasGroup的alpha值置为1
        canvasGroup.alpha = 1f;
        //将回调函数传入类中可供类内使用
        hideCallBack = CallBack;
    }
    protected void Update()
    {
        //渐进效果的实现
        if (isShow && canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Mathf.Clamp01(fadeSpeed * Time.unscaledDeltaTime);
        }
        //渐出效果的实现
        else if (!isShow && canvasGroup.alpha == 1)
        {
            canvasGroup.alpha -= Mathf.Clamp01(fadeSpeed * Time.unscaledDeltaTime);
            if(canvasGroup.alpha == 0)
            hideCallBack?.Invoke();
        }
    }
}
