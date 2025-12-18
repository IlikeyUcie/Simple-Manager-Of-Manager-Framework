using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStateData 
{
    //移动方向输入数据
    public Vector2 dirKeyAxis = new Vector2();
    //鼠标位置输入数据
    public Vector2 mouseDelta = new Vector2();
    //是否按下暂停输入数据
    public bool switchPause;
    //是否按下取消输入数据
    public bool cancel;
    //是否按下交互输入数据
    public bool interaction;
    //是否按下加速输入数据,这是需要持续检测的，而不是瞬时的
    public bool speedUp;
    //是否进入战斗形态输入数据
    public bool switchBattle;
    //是否按下翻滚躲避输入数据
    public bool rollorDodge;
    //是否按下锁定敌人输入数据
    public bool switchLookTarget;
    //是否按下攻击输入数据
    public bool attack;
    //是否按下背包输入数据
    public bool switchInventory;
    //是否按下第一个快捷道具输入数据
    public bool food1;
    //是否按下第二个快捷道具输入数据
    public bool food2;
    //是否按下第一个快捷药品输入数据
    public bool drug1;
    //是否按下第二个快捷药品输入数据
    public bool drug2;
  
}
