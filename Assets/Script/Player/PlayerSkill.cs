using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] PlayerSkillSO GetPlayerSkill;
    PlayerSystem GetPlayerSystem;
    [SerializeField] GameObject Skill7Attack;
    [SerializeField] GameObject Skill9Attack;
    bool FirstDieCheck = false;
    void Awake()
    {
        GetPlayerSystem = GetComponent<PlayerSystem>();
    }
    void OnEnable()
    {
        {//? (吸引1) 吸取掉落物範圍增加(初始為3，增加後為10)
            if (GetPlayerSkill.AllSkill[0] == false)
                GetPlayerSystem.SuckAwardCol.radius = 3;
            else
                GetPlayerSystem.SuckAwardCol.radius = 10;
        }
        {//? (閃避1) 閃避時間增加0.25秒
            if (GetPlayerSkill.AllSkill[1] == false)
                GetPlayerSystem.DashTime -= 0.25f;
            else
                GetPlayerSystem.DashTime += 0.25f;
        }
        {//? (減傷2) 所受傷害皆減少1點
            if (GetPlayerSkill.AllSkill[2] == false)
                PlayerSystem.HurtEvent -= Skill2;
            else
                PlayerSystem.HurtEvent += Skill2;
        }
        {//? (增傷2) 提高傷害(1.2倍) & 所受傷害皆增加1點
            if (GetPlayerSkill.AllSkill[3] == false)
            {
                PlayerSystem.AttackEvent -= Skill3;
                PlayerSystem.HurtEvent -= Skill3_1;
            }
            else
            {
                PlayerSystem.AttackEvent += Skill3;
                PlayerSystem.HurtEvent += Skill3_1;
            }
        }
        {//? (魔力成長2) 魔力容量增加500(數值暫定的)
            if (GetPlayerSkill.AllSkill[4] == false)
                PlayerDataSO.PlayerMaxMp -= 500;
            else
                PlayerDataSO.PlayerMaxMp += 500;
        }
        {//? (拜金2) 金錢獲取量上升為1.5倍 & 血量上限砍半
            if (GetPlayerSkill.AllSkill[5] == false)
            {
                PlayerDataSO.AddMoneyEvent -= Skill5;
                PlayerDataSO.PlayerMaxHp *= 2;
            }
            else
            {
                PlayerDataSO.AddMoneyEvent += Skill5;
                PlayerDataSO.PlayerMaxHp /= 2;
            }
        }
        {//? (無形攻擊2) 使用閃避可對閃避期間接觸的怪物造成10點傷害 & 閃避時間減少0.25秒
            if (GetPlayerSkill.AllSkill[6] == false)
            {
                GetPlayerSystem.DashTime += 0.25f;
                PlayerSystem.DashEvent -= Skill6;
            }
            else
            {
                GetPlayerSystem.DashTime -= 0.25f;
                PlayerSystem.DashEvent += Skill6;
            }
        }
        {//? (光華刀刃3) 每次攻擊有10%機率提高傷害(1.5倍)
            if (GetPlayerSkill.AllSkill[7] == false)
                PlayerSystem.AttackEvent -= Skill7;
            else
                PlayerSystem.AttackEvent += Skill7;
        }
        {//? (傷害反彈3) 受到傷害，攻擊對象會受到20點傷害
            if (GetPlayerSkill.AllSkill[8] == false)
                PlayerSystem.HurtEvent -= Skill8;
            else
                PlayerSystem.HurtEvent += Skill8;
        }
        {//? (魔力吸取3) 魔力吸取量上升為1.5倍
            if (GetPlayerSkill.AllSkill[9] == false)
                PlayerDataSO.AddMpEvent -= Skill9;
            else
                PlayerDataSO.AddMpEvent += Skill9;
        }
        {//? (回血速度增加3) 恢復動作加快(初始1秒，加快後0.5秒)
            if (GetPlayerSkill.AllSkill[10] == false)
                GetPlayerSystem.FastRestore = false;
            else
                GetPlayerSystem.FastRestore = true;
        }
        {//? (魔彼岸花3) 普通攻擊有5%機率吸取攻擊力10%的魔力
            if (GetPlayerSkill.AllSkill[11] == false)
                PlayerSystem.AttackHurtEnemyEvent -= Skill11;
            else
                PlayerSystem.AttackHurtEnemyEvent += Skill11;
        }
        {//? (根性4) 承受致命傷害，鎖血一滴
            if (GetPlayerSkill.AllSkill[12] == false)
                PlayerSystem.HurtEvent -= Skill12;
            else
                PlayerSystem.HurtEvent += Skill12;
        }
    }

    public void Skill2()//? 減傷效果(其實是透過加一滴血來達成減傷效果)
    {
        PlayerDataSO.PlayerNowHp += 1;
    }
    public void Skill3(PlayerAttack p)//? 增傷效果
    {
        p.TureDamage /= 10 * 12;
    }
    public void Skill3_1()//? 增傷負面效果
    {
        PlayerDataSO.PlayerNowHp -= 1;
    }
    public void Skill5(int much)//? 拜金效果
    {
        PlayerDataSO.PlayerMoney += much / 2;
    }
    public void Skill6()//? 無形攻擊效果
    {
        Instantiate(Skill7Attack, transform.position, transform.rotation, transform);
    }
    public void Skill7(PlayerAttack p)//? 光華刀刃效果
    {
        int r = Random.Range(0, 10);
        if (r > 8)
            p.TureDamage /= 10 * 15;
    }
    public void Skill8()//? 傷害反彈效果
    {
        Instantiate(Skill9Attack, transform.position, transform.rotation, transform);
    }
    public void Skill9(int much)//? 魔力吸取效果
    {
        PlayerDataSO.PlayerNowMp += much / 2;
    }
    public void Skill11(PlayerAttack p)//? 魔彼岸花效果
    {
        int r = Random.Range(0, 20);
        if (r > 18)
        {
            PlayerDataSO.PlayerNowMp += p.TureDamage / 10;
            PlayerDataSO.AddMpTrigger(p.TureDamage / 10);
        }

    }
    public void Skill12()//? 根性效果
    {
        if (FirstDieCheck == false)
        {
            if (PlayerDataSO.PlayerNowHp < 1)
            {
                PlayerDataSO.PlayerNowHp = 1;
                FirstDieCheck = true;
            }
        }
    }
}
