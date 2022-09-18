using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "MyCustomAsset/PlayerSystem")]
public class PlayerSystemSO : ScriptableObject
{
    public static Func<PlayerSystem> GetPlayerFunc;
    public static PlayerSystem GetPlayerInvoke()
    {
        return GetPlayerFunc.Invoke();
    }
    public static UnityAction UpdateSkillAction;
    public static void UpdateSkillInvoke()
    {
        UpdateSkillAction?.Invoke();
    }

    enum 技能講解
    {
        吸引 = 0,//* (1) 吸取掉落物範圍增加(初始為3，增加後為10)
        閃避 = 1,//* (2) 閃避無敵時間增加
        減傷 = 2,//* (1) 所受傷害皆減少1點
        增傷 = 3,//* (2) 提高傷害(1.2倍) & 所受傷害皆增加1點
        魔力成長 = 4,//* (1) 魔力容量增加20
        拜金 = 5,//* (2) 金錢獲取量上升為1.5倍 & 血量上限砍半
        無形攻擊 = 6,//* (2) 使用閃避可對閃避期間接觸的敵人造成傷害(攻擊力0.25倍的傷害)
        光華刀刃 = 7,//* (2) 每次攻擊有10%機率提高傷害(1.5倍)
        荊棘之身 = 8,//* (3) 受到傷害園地生成一個反彈盾，對接觸的敵人造成傷害(攻擊力2倍的傷害)
        魔力增加 = 9,//* (3) 魔力吸取量上升為1.5倍    
        回血加快 = 10,//* (3) 恢復動作加快
        彼岸花 = 11,//* (3) 施放技能消耗的魔力量減少
        根性 = 12,//* (4) 承受致命傷害，鎖血一滴
        生命成長 = 13//* (1) 生命增加20%
    }
    static int _skillMaxPoint;//* 最大技能點數
    public static int SkillMaxPoint { get => _skillMaxPoint; set { if (value > 10) _skillMaxPoint = 10; else _skillMaxPoint = value; } }
    static int _skillNowPoint;//* 已消耗的技能點數
    public static int SkillNowPoint { get => _skillNowPoint; set { _skillNowPoint = value; } }
    static bool[] _SkillOpen = new bool[15];//* 各技能是否解鎖
    public static bool[] SkillOpen { get => _SkillUse; set { _SkillUse = value; } }
    static bool[] _SkillUse = new bool[15];//* 各技能是否使用
    public static bool[] SkillUse { get => _SkillUse; set { _SkillUse = value; } }
    static int[] _SkillDeletePoint = new int[] { 1, 2, 1, 2, 1, 2, 2, 2, 3, 3, 3, 3, 4, 1 };//* 各技能的點數消耗
    public static int[] SkillDeletePoint { get => _SkillDeletePoint; private set { _SkillDeletePoint = value; } }
    public static void UseSkill(int whichSkill)
    {
        if (whichSkill < 15)
        {
            if (SkillOpen[whichSkill] == true)//? 技能已解鎖
            {
                if (SkillUse[whichSkill] == false)//? 技能未使用
                {
                    if (SkillNowPoint + SkillDeletePoint[whichSkill] <= SkillMaxPoint)//? 當前技能點數超過最大數將無法安裝
                    {
                        SkillNowPoint += SkillDeletePoint[whichSkill];
                        SkillUse[whichSkill] = true;
                    }
                }
                else//? 技能已使用
                {
                    SkillNowPoint -= SkillDeletePoint[whichSkill];
                    SkillUse[whichSkill] = false;
                }
            }
            else
            {

            }
        }
    }
}
