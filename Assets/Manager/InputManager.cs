using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    private InputStateData _state;
    public InputStateData stateData => _state;
    private GameInput gameInput;

    protected override void Awake()
    {
        _state = new InputStateData();
        gameInput = new GameInput();
        gameInput.Player.Move.RegisterActionCallBack(ActionType.ALL,
            (cb) =>
            {
                stateData.dirKeyAxis = cb.ReadValue<Vector2>();
            });
        gameInput.Player.Run.RegisterActionCallBack(ActionType.ALL,
            (cb) =>
            {
                stateData.speedUp = cb.ReadValue<float>() != 0;
            });
        gameInput.Player.MainWeapon.RegisterActionCallBack(ActionType.Started,
            (cb) =>
            {
                stateData.switchBattle = true;
            });
        gameInput.Player.RollorDodge.RegisterActionCallBack(ActionType.Started,
            (cb) => { stateData.rollorDodge = true; });
        gameInput.Player.LookTarget.RegisterActionCallBack(ActionType.Started,
            (cb) => { stateData.switchLookTarget = true; });
        gameInput.Player.Attack.RegisterActionCallBack(ActionType.Started,
            (cb) => { stateData.attack = true; });
        gameInput.Player.OppenInventory.RegisterActionCallBack(ActionType.Started,
          (cb) => { stateData.switchInventory = true; });
        gameInput.Player.Pause.RegisterActionCallBack(ActionType.Started,
          (cb) => { stateData.switchPause = true; });
        gameInput.Player.Food1.RegisterActionCallBack(ActionType.Started,
            (cb) => { stateData.food1 = true; });
        gameInput.Player.Food2.RegisterActionCallBack(ActionType.Started,
            (cb) => { stateData.food2 = true; });
        gameInput.Player.Drug1.RegisterActionCallBack(ActionType.Started,
            (cb) => { stateData.drug1 = true; });
        gameInput.Player.Drug2.RegisterActionCallBack(ActionType.Started,
             (cb) => { stateData.drug2 = true; });
        gameInput.Player.Interaction.RegisterActionCallBack(ActionType.Started,
             (cb) => { stateData.interaction = true; });

        //下面是UI的部分
        gameInput.UI.Cancel.RegisterActionCallBack(ActionType.Started,
            (cb) => { stateData.cancel = true; });
        gameInput.UI.CloseInventory.RegisterActionCallBack(ActionType.Started,
            (cb) => { stateData.switchInventory = true; });
        base.Awake();
    }
    // 进行状态每帧重置的方法
    public void ResetInputState()
    {
        stateData.attack = false;
        stateData.cancel = false;
        stateData.drug1 = false;
        stateData.drug2 = false;
        stateData.food1 = false;
        stateData.food2 = false;
        stateData.interaction = false;
        stateData.rollorDodge = false;
        stateData.switchBattle = false;
        stateData.switchInventory = false;
        stateData.switchLookTarget = false;
        stateData.switchPause = false;
        
    }
 
}
