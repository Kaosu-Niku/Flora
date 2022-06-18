using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyCustomAsset/PlayerSkill")]
public class PlayerSkillSO : ScriptableObject
{
    enum 技能講解
    {
        吸引 = 0,//* (1) 吸取掉落物範圍增加(初始為3，增加後為10)
        閃避 = 1,//* (1) 閃避時間增加0.25秒
        減傷 = 2,//* (2) 所受傷害皆減少1點
        增傷 = 3,//* (2) 提高傷害(1.2倍) & 所受傷害皆增加1點
        魔力成長 = 4,//* (2) 魔力容量增加500(數值暫定的)
        拜金 = 5,//* (2) 金錢獲取量上升為1.5倍 & 血量上限砍半
        無形攻擊 = 6,//* (2) 使用閃避可對閃避期間接觸的敵人造成傷害(攻擊力0.25倍的傷害) & 閃避時間減少0.25秒
        光華刀刃 = 7,//* (3) 每次攻擊有10%機率提高傷害(1.5倍)
        傷害反彈 = 8,//* (3) 受到傷害園地生成一個反彈盾，對接觸的敵人造成傷害(攻擊力0.5倍的傷害)
        魔力吸取 = 9,//* (3) 魔力吸取量上升為1.5倍
        恢復動作加快 = 10,//* (3) 恢復動作加快(初始1秒，加快後0.5秒)
        魔彼岸花 = 11,//* (3) 普通攻擊有5%機率吸取攻擊力10%的魔力
        根性 = 12,//* (4) 承受致命傷害，鎖血一滴
    }
    [SerializeField] List<bool> _allSkill = new List<bool>(15);//* 各技能是否開啟
    public List<bool> AllSkill { get => _allSkill; private set { _allSkill = value; } }
    List<int> _skillDepletePoint = new List<int>(15) { 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4 };//* 各技能的點數消耗
    public List<int> SkillDepletePoint { get => _skillDepletePoint; private set { _skillDepletePoint = value; } }
    public void UseSkill(int whichSkill)
    {
        if (whichSkill < 15)
        {
            if (AllSkill[whichSkill] == false)//? 技能未開啟
            {
                if (PlayerDataSO.SkillNowPoint + SkillDepletePoint[whichSkill] <= PlayerDataSO.SkillMaxPoint)//? 當前技能點數超過最大數將無法安裝
                {
                    PlayerDataSO.SkillNowPoint += SkillDepletePoint[whichSkill];
                    AllSkill[whichSkill] = true;
                }
            }
            else//? 技能已開啟
            {
                PlayerDataSO.SkillNowPoint -= SkillDepletePoint[whichSkill];
                AllSkill[whichSkill] = false;
            }
        }
    }
}
