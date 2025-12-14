using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    // 存储相机缓存的字典
    private Dictionary<string, CinemachineVirtualCameraBase> virtualCamera
        = new Dictionary<string, CinemachineVirtualCameraBase>();
    //相机可用优先级参数
    private const int enabledPriority = 10;
    //相机不可用优先级参数
    private const int disabledPriority = 0;

    protected override void Awake()
    {
        base.Awake();
        UpdateCamera();
    }
    /// <summary>
    /// 更新缓存字典的方法
    /// </summary>
    public void UpdateCamera()
    {
        virtualCamera.Clear();
        var cameras = FindObjectsOfType<CinemachineVirtualCameraBase>(true);
        foreach (var cam in cameras)
        {
            if (!virtualCamera.ContainsKey(cam.gameObject.name))
            {
                virtualCamera[cam.gameObject.name] = cam;
            }
            else
                Debug.Log("有重复命名的camera");
        }
    }
    /// <summary>
    /// 获取相机的方法
    /// </summary>
    /// <param name="name">需要获取相机的名字</param>
    /// <returns>需要获取的相机</returns>
    public CinemachineVirtualCameraBase GetCamera(string name)
    {
        if (virtualCamera == null)
            UpdateCamera();
        virtualCamera.TryGetValue(name, out CinemachineVirtualCameraBase cam);
        if (cam == null)
            Debug.Log("没有该名字的相机！");
        return cam;
    }
    /// <summary>
    /// 使相机可用的方法
    /// </summary>
    /// <param name="name">可用相机的名字</param>
    /// <param name="enabledInput">是否需要输出同时可用</param>
    public void EnabledCamera(string name, bool enabledInput = false)
    {
        var camera = GetCamera(name);
        if (camera != null)
            camera.Priority = enabledPriority;
        else
            Debug.Log("该相机不存在！");
        if (enabledInput)
        {
            var InputProvider = camera.GetComponent<CinemachineInputProvider>();
            if (InputProvider == null)
                Debug.Log("输入组件不存在！");
            else
                InputProvider.enabled = true;
        }
    }
    /// <summary>
    /// 使相机不可用的方法
    /// </summary>
    /// <param name="name">相机不可用的名字</param>
    /// <param name="disabledInput">是否需要同时让输入也不可用</param>
    public void DisabledCamera(string name, bool disabledInput = false)
    {
        var camera = GetCamera(name);
        if (camera != null)
            camera.Priority = disabledPriority;
        else
            Debug.Log("该相机不存在！");
        if (disabledInput)
        {
            var InputProvider = camera.GetComponent<CinemachineInputProvider>();
            if (InputProvider == null)
                Debug.Log("输入组件不存在！");
            else
                InputProvider.enabled = false;
        }
    }
    /// <summary>
    /// 使相机震动的方法
    /// </summary>
    /// <param name="impulseSource">让哪个相机源进行震动</param>
    /// <param name="force">震动的大小数值</param>
    public void CameraShake(CinemachineImpulseSource impulseSource,float force)
    {
        if (impulseSource != null)
            impulseSource.GenerateImpulseWithForce(force);
    }
}
