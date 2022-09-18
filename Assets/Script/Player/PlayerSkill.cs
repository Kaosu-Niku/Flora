using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    int FirstPlayerMaxHp;
    bool Skill5Check;
    bool Skill12Check = false;
    bool Skill13Check;
    void Awake()
    {
        PlayerSystemSO.UpdateSkillAction += UpDateSkill;
    }
    void OnDestroy()
    {
        PlayerSystemSO.UpdateSkillAction -= UpDateSkill;
    }
    private void Start()
    {
        FirstPlayerMaxHp = PlayerDataSO.MaxHp;
        UpDateSkill();
    }
    void UpDateSkill()
    {
        StartCoroutine(UpdateSkillIEnum());
    }
    IEnumerator UpdateSkillIEnum()
    {
        yield return 0;
        {//? (吸引) 吸取掉落物範圍增加(初始為3，增加後為10)
            if (PlayerSystemSO.SkillUse[0] == true)
                PlayerSystemSO.GetPlayerInvoke().SuckAwardCol.radius = 10;
            else
                PlayerSystemSO.GetPlayerInvoke().SuckAwardCol.radius = 3;
        }
        {//? (閃避) 閃避時間增加
            PlayerSystemSO.GetPlayerInvoke().FastFlash = PlayerSystemSO.SkillUse[1];
        }
        {//? (減傷) 所受傷害皆減少1點
            if (PlayerSystemSO.SkillUse[2] == true)
                PlayerSystemSO.GetPlayerInvoke().HurtEvent += Skill2;
            else
                PlayerSystemSO.GetPlayerInvoke().HurtEvent -= Skill2;
        }
        {//? (增傷) 提高傷害(1.2倍) & 所受傷害皆增加1點
            if (PlayerSystemSO.SkillUse[3] == true)
            {
                PlayerSystemSO.GetPlayerInvoke().AttackEvent += Skill3;
                PlayerSystemSO.GetPlayerInvoke().HurtEvent += Skill3_1;
            }
            else
            {
                PlayerSystemSO.GetPlayerInvoke().AttackEvent -= Skill3;
                PlayerSystemSO.GetPlayerInvoke().HurtEvent -= Skill3_1;
            }
        }
        {//? (魔力成長) 魔力容量增加20
            if (PlayerSystemSO.SkillUse[4] == true)
                PlayerSystemSO.GetPlayerInvoke().SetMaxMp(120);
            else
                PlayerSystemSO.GetPlayerInvoke().SetMaxMp(100);
        }
        {//? (拜金) 金錢獲取量上升為1.5倍 & 血量上限砍半
            if (PlayerSystemSO.SkillUse[5] == true)
            {
                PlayerSystemSO.GetPlayerInvoke().AddMoneyEvent += Skill5;
                if (Skill5Check == false)
                {
                    PlayerSystemSO.GetPlayerInvoke().AddMaxHp(-FirstPlayerMaxHp / 2);
                    Skill5Check = true;
                }
            }
            else
            {
                PlayerSystemSO.GetPlayerInvoke().AddMoneyEvent -= Skill5;
                if (Skill5Check == true)
                {
                    PlayerSystemSO.GetPlayerInvoke().AddMaxHp(FirstPlayerMaxHp / 2);
                    Skill5Check = false;
                }
            }
        }
        {//? (無形攻擊) 使用閃避可對閃避期間接觸的怪物造成傷害
            PlayerSystemSO.GetPlayerInvoke().Skill6Check = PlayerSystemSO.SkillUse[6];
        }
        {//? (光華刀刃) 每次攻擊有10%機率提高傷害(1.5倍)
            if (PlayerSystemSO.SkillUse[7] == true)
                PlayerSystemSO.GetPlayerInvoke().AttackEvent += Skill7;
            else
                PlayerSystemSO.GetPlayerInvoke().AttackEvent -= Skill7;
        }
        {//? (傷害反彈) 受到傷害，攻擊對象會受到傷害
            PlayerSystemSO.GetPlayerInvoke().Skill8Check = PlayerSystemSO.SkillUse[8];
        }
        {//? (魔力吸取) 魔力吸取量上升為1.5倍
            if (PlayerSystemSO.SkillUse[9] == true)
                PlayerSystemSO.GetPlayerInvoke().AddMpEvent += Skill9;
            else
                PlayerSystemSO.GetPlayerInvoke().AddMpEvent -= Skill9;
        }
        {//? (回血速度增加) 恢復動作加快
            PlayerSystemSO.GetPlayerInvoke().FastRestore = PlayerSystemSO.SkillUse[10];
        }
        {//! (彼岸花) 減少施放技能所消耗的魔力
            if (PlayerSystemSO.SkillUse[11] == true)
            {

            }
            else
            {

            }
        }
        {//? (根性) 承受致命傷害，鎖血一滴
            if (PlayerSystemSO.SkillUse[12] == true)
                PlayerSystemSO.GetPlayerInvoke().HurtEvent += Skill12;
            else
                PlayerSystemSO.GetPlayerInvoke().HurtEvent -= Skill12;
        }
        {//? (生命成長) 額外增加20%血量
            if (PlayerSystemSO.SkillUse[13] == true)
            {
                if (Skill13Check == false)
                {
                    PlayerSystemSO.GetPlayerInvoke().AddMaxHp(FirstPlayerMaxHp / 5);
                    Skill13Check = true;
                }
            }
            else
            {
                if (Skill13Check == true)
                {
                    PlayerSystemSO.GetPlayerInvoke().AddMaxHp(-FirstPlayerMaxHp / 5);
                    Skill13Check = false;
                }
            }
        }
    }

    private void Skill2()//? 減傷效果(其實是透過減一滴血來達成減傷效果)
    {
        PlayerSystemSO.GetPlayerInvoke().AddNowHp(-1);
    }
    private void Skill3(PlayerAttack p)//? 增傷效果
    {
        p.TureDamage /= 10 * 12;
    }
    private void Skill3_1()//? 增傷負面效果
    {
        PlayerSystemSO.GetPlayerInvoke().AddNowHp(1);
    }
    private void Skill5(int much)//? 拜金效果
    {
        PlayerDataSO.Money += much / 2;
    }
    private void Skill7(PlayerAttack p)//? 光華刀刃效果
    {
        int r = Random.Range(0, 10);
        if (r > 8)
            p.TureDamage /= 10 * 15;
    }
    private void Skill9(int much)//? 魔力吸取效果
    {
        if (much > 0)
        {
            PlayerSystemSO.GetPlayerInvoke().AddMpEvent -= Skill9;//? 防止重複增加陷入死循環
            PlayerSystemSO.GetPlayerInvoke().AddNowMp(much / 2);
            PlayerSystemSO.GetPlayerInvoke().AddMpEvent += Skill9;
        }
    }
    private void Skill11()//? 魔彼岸花效果
    {

    }
    private void Skill12()//? 根性效果
    {
        if (Skill12Check == false)
        {
            if (PlayerSystemSO.GetPlayerInvoke().NowHp >= PlayerSystemSO.GetPlayerInvoke().MaxHp)
            {
                PlayerSystemSO.GetPlayerInvoke().AddNowHp(PlayerSystemSO.GetPlayerInvoke().NowHp - 1);
                Skill12Check = true;
            }
        }
    }
}
