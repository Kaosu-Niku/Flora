using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] GameObject Skill7Attack;
    [SerializeField] GameObject Skill9Attack;
    bool FirstDieCheck = false;
    void OnEnable()
    {
        StartCoroutine(LateTrigger());
    }
    IEnumerator LateTrigger()
    {
        yield return 0;
        {//? (吸引1) 吸取掉落物範圍增加(初始為3，增加後為10)
            if (PlayerSkillSO.AllSkill[0] == true)
                PlayerSystemSO.GetPlayerInvoke().SuckAwardCol.radius = 10;
        }
        {//? (閃避1) 閃避時間增加0.25秒
            if (PlayerSkillSO.AllSkill[1] == true)
                PlayerSystemSO.GetPlayerInvoke().DashTime += 0.25f;
        }
        {//? (減傷2) 所受傷害皆減少1點
            if (PlayerSkillSO.AllSkill[2] == true)
                PlayerSystemSO.GetPlayerInvoke().HurtEvent += Skill2;
        }
        {//? (增傷2) 提高傷害(1.2倍) & 所受傷害皆增加1點
            if (PlayerSkillSO.AllSkill[3] == true)
            {
                PlayerSystemSO.GetPlayerInvoke().AttackEvent += Skill3;
                PlayerSystemSO.GetPlayerInvoke().HurtEvent += Skill3_1;
            }
        }
        {//? (魔力成長2) 魔力容量增加500(數值暫定的)
            if (PlayerSkillSO.AllSkill[4] == true)
                PlayerSystemSO.GetPlayerInvoke().AddMaxMp(500);
        }
        {//? (拜金2) 金錢獲取量上升為1.5倍 & 血量上限砍半
            if (PlayerSkillSO.AllSkill[5] == true)
            {
                PlayerSystemSO.GetPlayerInvoke().AddMoneyEvent += Skill5;
                PlayerDataSO.MaxHp /= 2;
            }
        }
        {//? (無形攻擊2) 使用閃避可對閃避期間接觸的怪物造成10點傷害 & 閃避時間減少0.25秒
            if (PlayerSkillSO.AllSkill[6] == true)
            {
                PlayerSystemSO.GetPlayerInvoke().DashTime -= 0.25f;
                PlayerSystemSO.GetPlayerInvoke().DashEvent += Skill6;
            }
        }
        {//? (光華刀刃3) 每次攻擊有10%機率提高傷害(1.5倍)
            if (PlayerSkillSO.AllSkill[7] == true)
                PlayerSystemSO.GetPlayerInvoke().AttackEvent += Skill7;
        }
        {//? (傷害反彈3) 受到傷害，攻擊對象會受到20點傷害
            if (PlayerSkillSO.AllSkill[8] == true)
                PlayerSystemSO.GetPlayerInvoke().HurtEvent += Skill8;
        }
        {//? (魔力吸取3) 魔力吸取量上升為1.5倍
            if (PlayerSkillSO.AllSkill[9] == true)
                PlayerSystemSO.GetPlayerInvoke().AddMpEvent += Skill9;
        }
        {//? (回血速度增加3) 恢復動作加快(初始1秒，加快後0.5秒)
            if (PlayerSkillSO.AllSkill[10] == true)
                PlayerSystemSO.GetPlayerInvoke().FastRestore = true;
        }
        {//? (魔彼岸花3) 普通攻擊有5%機率吸取攻擊力10%的魔力
            if (PlayerSkillSO.AllSkill[11] == true)
                PlayerSystemSO.GetPlayerInvoke().AttackHurtEnemyEvent += Skill11;
        }
        {//? (根性4) 承受致命傷害，鎖血一滴
            if (PlayerSkillSO.AllSkill[12] == true)
                PlayerSystemSO.GetPlayerInvoke().HurtEvent += Skill12;
        }
    }

    private void Skill2()//? 減傷效果(其實是透過加一滴血來達成減傷效果)
    {
        PlayerSystemSO.GetPlayerInvoke().AddNowHp(1);
    }
    private void Skill3(PlayerAttack p)//? 增傷效果
    {
        p.TureDamage /= 10 * 12;
    }
    private void Skill3_1()//? 增傷負面效果
    {
        PlayerSystemSO.GetPlayerInvoke().AddNowHp(-1);
    }
    private void Skill5(int much)//? 拜金效果
    {
        PlayerDataSO.Money += much / 2;
    }
    private void Skill6()//? 無形攻擊效果
    {
        Instantiate(Skill7Attack, transform.position, transform.rotation, transform);
    }
    private void Skill7(PlayerAttack p)//? 光華刀刃效果
    {
        int r = Random.Range(0, 10);
        if (r > 8)
            p.TureDamage /= 10 * 15;
    }
    private void Skill8()//? 傷害反彈效果
    {
        Instantiate(Skill9Attack, transform.position, transform.rotation, transform);
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
    private void Skill11(PlayerAttack p)//? 魔彼岸花效果
    {
        int r = Random.Range(0, 20);
        if (r == 10)
        {
            PlayerSystemSO.GetPlayerInvoke().AddNowMp(p.TureDamage / 10);
        }

    }
    private void Skill12()//? 根性效果
    {
        if (FirstDieCheck == false)
        {
            if (PlayerSystemSO.GetPlayerInvoke().NowHp < 1)
            {
                PlayerSystemSO.GetPlayerInvoke().AddNowHp(-PlayerSystemSO.GetPlayerInvoke().NowHp + 1);
                FirstDieCheck = true;
            }
        }
    }
}
