using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : Monster
{
    [SerializeField] float Speed;
    [SerializeField] GameObject SmallMonster;
    [SerializeField] MonsterAttack FastPrickAttack;
    [SerializeField] MonsterAttack SlowPrickAttack;
    [SerializeField] MonsterAttack SlowPrickFloorAttack;
    float CallSmallTime;//* 召喚小吱吱的時間
    bool TwoStage;//* 第二階段
    float ThreeStageTime = 100;//* 第三階段大招間隔時間
    protected override IEnumerator CustomIdle()
    {
        IsFight = true;
        yield break;
    }
    protected override IEnumerator CustomDefault()
    {
        if (CallSmallTime > 20)//? 每超過20秒必定優先召喚一隻小吱吱
            yield return StartCoroutine(CallSmallMonster());
        else
            yield return StartCoroutine(Move());
        yield break;
    }

    protected override IEnumerator CustomHurt()
    {
        yield break;
    }

    protected override IEnumerator CustomHitRecover()
    {
        Anima.SetTrigger("Hurt");
        yield return new WaitForSeconds(2);
    }

    protected override IEnumerator CustomDie()
    {
        Anima.SetTrigger("Die");
        yield return new WaitForSeconds(2);
    }
    private IEnumerator Move()//? 招式起手式
    {
        if (true)//? 第一階段  Hp > MaxHp * 0.7f
        {
            Anima.SetInteger("Move", 1);
            if (transform.position.x <= PlayerSystemSO.GetPlayerInvoke().transform.position.x)
                transform.rotation = Quaternion.Euler(transform.position.x, 0, 0);
            else
                transform.rotation = Quaternion.Euler(transform.position.x, 180, 0);
            if (GetPlayerDistance() > 4)//? 距離大於4，持續接近玩家
            {
                while (GetPlayerDistance() > 4)
                {
                    transform.Translate(Speed * Time.deltaTime, 0, 0);
                    yield return 0;
                }
                int r = Random.Range(0, 2);//? 接近玩家後，各50%使出快速或慢速刺擊
                if (r == 0)
                    yield return StartCoroutine(FastPrick());
                else
                    yield return StartCoroutine(SlowPrick());
            }
            else//? 距離小於4，直接使出慢速刺擊
            {
                yield return StartCoroutine(SlowPrick());
            }
        }
        else if (Hp > MaxHp * 0.4f)//? 第二階段
        {
            if (TwoStage == false)//? 轉二階段必放一次
                yield return StartCoroutine(TwoStageAttack());
            else
            {
                int r = Random.Range(0, 5);
                if (r == 3)//? 20%使出大跳高
                    yield return StartCoroutine(BigJump());
                else
                {
                    if (GetPlayerDistance() > 4)//? 距離大於4，持續接近玩家
                    {
                        while (GetPlayerDistance() > 4)
                        {
                            transform.Translate(Speed * Time.deltaTime, 0, 0);
                            yield return 0;
                        }
                        int r2 = Random.Range(0, 2);//? 接近玩家後，各50%使出快速或慢速刺擊
                        if (r2 == 0)
                            yield return StartCoroutine(FastPrick());
                        else
                            yield return StartCoroutine(SlowPrick());
                    }
                    else//? 距離小於4，各50%使出慢速刺擊或大跳高
                    {
                        int r3 = Random.Range(0, 2);
                        if (r3 == 0)
                            yield return StartCoroutine(SlowPrick());
                        else
                            yield return StartCoroutine(BigJump());
                    }
                }
            }
        }
        else//? 第三階段
        {
            if (ThreeStageTime > 50)//? 轉三階段必放一次，之後每隔50秒施放一次
                yield return StartCoroutine(ThreeStageAttack());
            else
            {
                int r = Random.Range(0, 5);
                if (r == 3)//? 20%使出大跳高
                {
                    ThreeStageTime += 4;
                    yield return StartCoroutine(BigJump());
                }
                else
                {
                    if (GetPlayerDistance() > 4)//? 距離大於4，持續接近玩家
                    {
                        while (GetPlayerDistance() > 4)
                        {
                            transform.Translate(Speed * Time.deltaTime, 0, 0);
                            yield return 0;
                        }
                        int r2 = Random.Range(0, 2);//? 接近玩家後，各50%使出快速或慢速刺擊
                        if (r2 == 0)
                        {
                            yield return StartCoroutine(FastPrick());
                        }
                        else
                        {
                            yield return StartCoroutine(SlowPrick());
                        }
                    }
                    else//? 距離小於4，各40%使出慢速刺擊或大跳高，20%使出二階段大招
                    {
                        int r3 = Random.Range(0, 5);
                        if (r3 > 2)
                        {
                            ThreeStageTime += 3;
                            yield return StartCoroutine(SlowPrick());
                        }
                        else if (r3 > 0)
                        {
                            ThreeStageTime += 4;
                            yield return StartCoroutine(BigJump());
                        }
                        else
                        {
                            ThreeStageTime += 5;
                            yield return StartCoroutine(TwoStageAttack());
                        }
                    }
                }
            }
        }
    }
    private IEnumerator CallSmallMonster()//? 召喚小吱吱
    {
        Instantiate(SmallMonster, transform.position + Vector3.up * 20, Quaternion.identity);
        yield return new WaitForSeconds(3);
        CallSmallTime = 0;
        yield break;
    }
    private IEnumerator FastPrick()//? 快速刺擊
    {
        FastPrickAttack.gameObject.SetActive(true);
        Anima.SetInteger("Move", 0);
        Anima.SetTrigger("Attack");
        Anima.SetInteger("WhichAttack", 0);
        yield return new WaitForSeconds(2);
        yield break;
    }
    private IEnumerator SlowPrick()//? 慢速刺擊
    {
        SlowPrickAttack.gameObject.SetActive(true);
        SlowPrickFloorAttack.gameObject.SetActive(true);
        Anima.SetInteger("Move", 0);
        Anima.SetTrigger("Attack");
        Anima.SetInteger("WhichAttack", 1);
        yield return new WaitForSeconds(3);
        yield break;
    }
    private IEnumerator TwoStageAttack()//? 二階段絕招
    {
        TwoStage = true;
        yield return new WaitForSeconds(5);
        yield break;
    }
    private IEnumerator BigJump()//? 大跳躍，附帶落石
    {
        yield return new WaitForSeconds(4);
        yield break;
    }
    private IEnumerator ThreeStageAttack()//? 三階段絕招
    {
        yield return new WaitForSeconds(5);
        ThreeStageTime = 0;
        yield break;
    }
    IEnumerator CallTime()
    {
        while (true)
        {
            CallSmallTime += Time.deltaTime;
            ThreeStageTime += Time.deltaTime;
            yield return 0;
        }
    }
    new void Start()
    {
        base.Start();
        StartCoroutine(CallTime());
    }
}
