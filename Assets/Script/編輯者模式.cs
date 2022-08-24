using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 編輯者模式 : MonoBehaviour
{
    [SerializeField] float 運行時間;
    [SerializeField] int 玩家最大血量;
    [SerializeField] int 玩家最大魔力;
    [SerializeField] int 玩家最大攻擊力;
    [SerializeField] int 玩家最大硬直力;
    [SerializeField] int 玩家最大速度;
    [SerializeField] int 玩家最大槽數;

    [SerializeField] bool 吸引;
    [SerializeField] bool 閃避;
    [SerializeField] bool 減傷;
    [SerializeField] bool 增傷;
    [SerializeField] bool 魔力成長;
    [SerializeField] bool 拜金;
    [SerializeField] bool 無形攻擊;
    [SerializeField] bool 光華刀刃;
    [SerializeField] bool 荊棘之身;
    [SerializeField] bool 魔力增加;
    [SerializeField] bool 回血加快;
    [SerializeField] bool 彼岸花;
    [SerializeField] bool 根性;
    [SerializeField] bool 生命成長;
    void Awake()
    {
        Time.timeScale = 運行時間;
        PlayerDataSO.MaxHp = 玩家最大血量;
        PlayerDataSO.MaxMp = 玩家最大魔力;
        PlayerDataSO.MaxAtk = 玩家最大攻擊力;
        PlayerDataSO.MaxHit = 玩家最大硬直力;
        PlayerDataSO.MaxSpeed = 玩家最大速度;
        PlayerSystemSO.SkillMaxPoint = 玩家最大槽數;
        PlayerSystemSO.AllSkill[0] = 吸引;
        PlayerSystemSO.AllSkill[1] = 閃避;
        PlayerSystemSO.AllSkill[2] = 減傷;
        PlayerSystemSO.AllSkill[3] = 增傷;
        PlayerSystemSO.AllSkill[4] = 魔力成長;
        PlayerSystemSO.AllSkill[5] = 拜金;
        PlayerSystemSO.AllSkill[6] = 無形攻擊;
        PlayerSystemSO.AllSkill[7] = 光華刀刃;
        PlayerSystemSO.AllSkill[8] = 荊棘之身;
        PlayerSystemSO.AllSkill[9] = 魔力增加;
        PlayerSystemSO.AllSkill[10] = 回血加快;
        PlayerSystemSO.AllSkill[11] = 彼岸花;
        PlayerSystemSO.AllSkill[12] = 根性;
        PlayerSystemSO.AllSkill[13] = 生命成長;
    }

}
