using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum ActionType
{
    Started,
    Performed,
    Canceled,
    ExceptStarted,
    ExceptPerformed,
    ExceptCanceled,
    ALL
}
public static class InputTools
{
    [Flags]
    public enum ActionPhaseFlags
    {
        None = 0,
        Started = 1 << 0,
        Performed = 1 << 1,
        Canceled = 1 << 2
    }
    public static ActionPhaseFlags GetPhaseFlags(ActionType type)
    {

        switch (type)
        {
            case ActionType.Started:
                return ActionPhaseFlags.Started;
            case ActionType.Performed:
                return ActionPhaseFlags.Performed;
            case ActionType.Canceled:
                return ActionPhaseFlags.Canceled;
            case ActionType.ExceptStarted:
                return ActionPhaseFlags.Performed | ActionPhaseFlags.Canceled;
            case ActionType.ExceptPerformed:
                return ActionPhaseFlags.Started | ActionPhaseFlags.Canceled;
            case ActionType.ExceptCanceled:
                return ActionPhaseFlags.Started | ActionPhaseFlags.Performed;
            case ActionType.ALL:
                return ActionPhaseFlags.Started | ActionPhaseFlags.Performed | ActionPhaseFlags.Canceled;
            default:
                return ActionPhaseFlags.None;
        }
        ;
    }
    public static void RegisterActionCallBack(this InputAction action,
                                               ActionType type,
                                               Action<InputAction.CallbackContext> context)
    {
        if (action == null || context == null)
            return;
        var flags = GetPhaseFlags(type);
        if ((flags & ActionPhaseFlags.Started) != 0)
            action.started += context;
        if ((flags & ActionPhaseFlags.Performed) != 0)
            action.performed += context;
        if ((flags & ActionPhaseFlags.Canceled) != 0)
            action.canceled += context;

    }
    public static void UnRegisterActionCallBack(this InputAction action,
                                           Action<InputAction.CallbackContext> context)
    {
        if (action == null || context == null)
            return;
        action.started -= context;
        action.performed -= context;
        action.canceled -= context;

    }
    public static void SwitchToActionMap(this InputActionAsset asset, string actionMap)
    {
        if (asset == null && string.IsNullOrEmpty(actionMap))
            return;
        asset.Disable();
        var Map = asset.FindActionMap(actionMap);
        if (Map == null)
            Debug.Log("没有找到地图");
        else
            Map.Enable();
    }

}
