using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 編輯者模式 : MonoBehaviour
{
    [SerializeField] int 玩家最大血量;
    [SerializeField] int 玩家最大魔力;
    [SerializeField] int 玩家最大攻擊力;
    [SerializeField] int 玩家最大硬直力;
    [SerializeField] int 玩家最大速度;

    [SerializeField] bool 吸引;
    [SerializeField] bool 閃避;
    [SerializeField] bool 減傷;
    [SerializeField] bool 增傷;
    [SerializeField] bool 魔力成長;
    [SerializeField] bool 拜金;
    [SerializeField] bool 無形攻擊;
    [SerializeField] bool 光華刀刃;
    [SerializeField] bool 傷害反彈;
    [SerializeField] bool 魔力吸取;
    [SerializeField] bool 回血速度增加;
    [SerializeField] bool 魔彼岸花;
    [SerializeField] bool 根性;
    void Awake()
    {
        PlayerDataSO.MaxHp = 玩家最大血量;
        PlayerDataSO.MaxMp = 玩家最大魔力;
        PlayerDataSO.MaxAtk = 玩家最大攻擊力;
        PlayerDataSO.MaxHit = 玩家最大硬直力;
        PlayerDataSO.MaxSpeed = 玩家最大速度;
        PlayerSkillSO.AllSkill[0] = 吸引;
        PlayerSkillSO.AllSkill[1] = 閃避;
        PlayerSkillSO.AllSkill[2] = 減傷;
        PlayerSkillSO.AllSkill[3] = 增傷;
        PlayerSkillSO.AllSkill[4] = 魔力成長;
        PlayerSkillSO.AllSkill[5] = 拜金;
        PlayerSkillSO.AllSkill[6] = 無形攻擊;
        PlayerSkillSO.AllSkill[7] = 光華刀刃;
        PlayerSkillSO.AllSkill[8] = 傷害反彈;
        PlayerSkillSO.AllSkill[9] = 魔力吸取;
        PlayerSkillSO.AllSkill[10] = 回血速度增加;
        PlayerSkillSO.AllSkill[11] = 魔彼岸花;
        PlayerSkillSO.AllSkill[12] = 根性;
    }

}
