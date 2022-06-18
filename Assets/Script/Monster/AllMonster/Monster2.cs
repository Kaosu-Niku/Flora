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
    protected override IEnumerator OnActionIEnum()
    {
        if (CallSmallTime > 10)//? 每超過20秒都召喚一隻小吱吱
            yield return StartCoroutine(OnCallSmallMonster());
        else
            yield return StartCoroutine(OnMove());
    }
    protected override IEnumerator CustomHitRecoverIEnum()
    {
        Anima.SetTrigger("Hurt");
        yield break;
    }
    protected override IEnumerator CustomDieIEnum()
    {
        Anima.SetTrigger("Die");
        yield break;
    }
    private IEnumerator OnMove()//? 招式起手式
    {
        Anima.SetInteger("Move", 1);
        if (transform.position.x <= PlayerDataSO.PlayerTrans.position.x)
            transform.rotation = Quaternion.Euler(transform.position.x, 0, 0);
        else
            transform.rotation = Quaternion.Euler(transform.position.x, 180, 0);
        if (Mathf.Abs(transform.position.x - PlayerDataSO.PlayerTrans.position.x) > 4)//? 距離大於4，持續接近玩家
        {
            while (Mathf.Abs(transform.position.x - PlayerDataSO.PlayerTrans.position.x) > 4)
            {
                transform.Translate(Speed * Time.deltaTime, 0, 0);
                yield return 0;
            }
            int r = Random.Range(0, 2);//? 接近玩家後，各50%使出快速或慢速刺擊
            if (r == 0)
                yield return StartCoroutine(OnFastPrick());
            else
                yield return StartCoroutine(OnSlowPrick());
        }
        else//? 距離小於4，直接使出慢速刺擊
        {
            yield return StartCoroutine(OnSlowPrick());
        }
        // if (Hp > MaxHp * 0.7f)//? 第一階段
        // {
        //     if (Mathf.Abs(transform.position.x - GetPlayer.transform.position.x) > 4)//? 距離大於4，持續接近玩家
        //     {
        //         while (Mathf.Abs(transform.position.x - GetPlayer.transform.position.x) > 4)
        //         {
        //             transform.Translate(Speed * Time.deltaTime, 0, 0);
        //             CallSmallTime += Time.deltaTime;
        //             yield return 0;
        //         }
        //         int r = Random.Range(0, 2);//? 接近玩家後，各50%使出快速或慢速刺擊
        //         if (r == 0)
        //             yield return StartCoroutine(OnFastPrick());
        //         else
        //             yield return StartCoroutine(OnSlowPrick());
        //     }
        //     else//? 距離小於4，直接使出慢速刺擊
        //     {
        //         yield return StartCoroutine(OnSlowPrick());
        //     }
        // }
        // else if (Hp > MaxHp * 0.4f)//? 第二階段
        // {
        //     if (TwoStage == false)//? 轉二階段必放一次
        //         yield return StartCoroutine(OnTwoStageAttack());
        //     else
        //     {
        //         int r = Random.Range(0, 5);
        //         if (r == 3)//? 20%使出大跳高
        //             yield return StartCoroutine(OnBigJump());
        //         else
        //         {
        //             if (Mathf.Abs(transform.position.x - GetPlayer.transform.position.x) > 4)//? 距離大於4，持續接近玩家
        //             {
        //                 while (Mathf.Abs(transform.position.x - GetPlayer.transform.position.x) > 4)
        //                 {
        //                     transform.Translate(Speed * Time.deltaTime, 0, 0);
        //                     CallSmallTime += Time.deltaTime;
        //                     yield return 0;
        //                 }
        //                 int r2 = Random.Range(0, 2);//? 接近玩家後，各50%使出快速或慢速刺擊
        //                 if (r2 == 0)
        //                     yield return StartCoroutine(OnFastPrick());
        //                 else
        //                     yield return StartCoroutine(OnSlowPrick());
        //             }
        //             else//? 距離小於4，各50%使出慢速刺擊或大跳高
        //             {
        //                 int r3 = Random.Range(0, 2);
        //                 if (r3 == 0)
        //                     yield return StartCoroutine(OnSlowPrick());
        //                 else
        //                     yield return StartCoroutine(OnBigJump());
        //             }
        //         }
        //     }
        // }
        // else//? 第三階段
        // {
        //     if (ThreeStageTime > 49)//? 轉三階段必放一次，之後每隔50秒施放一次
        //         yield return StartCoroutine(OnTwoStageAttack());
        //     else
        //     {
        //         int r = Random.Range(0, 5);
        //         if (r == 3)//? 20%使出大跳高
        //         {
        //             ThreeStageTime += 4;
        //             yield return StartCoroutine(OnBigJump());
        //         }
        //         else
        //         {
        //             if (Mathf.Abs(transform.position.x - GetPlayer.transform.position.x) > 4)//? 距離大於4，持續接近玩家
        //             {
        //                 while (Mathf.Abs(transform.position.x - GetPlayer.transform.position.x) > 4)
        //                 {
        //                     transform.Translate(Speed * Time.deltaTime, 0, 0);
        //                     CallSmallTime += Time.deltaTime;
        //                     ThreeStageTime += Time.deltaTime;
        //                     yield return 0;
        //                 }
        //                 int r2 = Random.Range(0, 2);//? 接近玩家後，各50%使出快速或慢速刺擊
        //                 if (r2 == 0)
        //                 {
        //                     ThreeStageTime += 2;
        //                     yield return StartCoroutine(OnFastPrick());
        //                 }
        //                 else
        //                 {
        //                     ThreeStageTime += 3;
        //                     yield return StartCoroutine(OnSlowPrick());
        //                 }
        //             }
        //             else//? 距離小於4，各40%使出慢速刺擊或大跳高，20%使出二階段大招
        //             {
        //                 int r3 = Random.Range(0, 5);
        //                 if (r3 > 2)
        //                 {
        //                     ThreeStageTime += 3;
        //                     yield return StartCoroutine(OnSlowPrick());
        //                 }
        //                 else if (r3 > 0)
        //                 {
        //                     ThreeStageTime += 4;
        //                     yield return StartCoroutine(OnBigJump());
        //                 }
        //                 else
        //                 {
        //                     ThreeStageTime += 5;
        //                     yield return StartCoroutine(OnTwoStageAttack());
        //                 }
        //             }
        //         }
        //     }
        // }
    }
    private IEnumerator OnCallSmallMonster()//? 召喚小吱吱
    {
        Instantiate(SmallMonster, transform.position + Vector3.up * 20, Quaternion.identity);
        yield return new WaitForSeconds(3);
        CallSmallTime = 0;
        OnAction();
        yield break;
    }
    private IEnumerator OnFastPrick()//? 快速刺擊
    {
        FastPrickAttack.UseAttack();
        Anima.SetInteger("Move", 0);
        Anima.SetTrigger("Attack");
        Anima.SetInteger("WhichAttack", 0);
        yield return new WaitForSeconds(2);
        CallSmallTime += 2;
        OnAction();
        yield break;
    }
    private IEnumerator OnSlowPrick()//? 慢速刺擊
    {
        SlowPrickAttack.UseAttack();
        SlowPrickFloorAttack.UseAttack();
        Anima.SetInteger("Move", 0);
        Anima.SetTrigger("Attack");
        Anima.SetInteger("WhichAttack", 1);
        yield return new WaitForSeconds(3);
        CallSmallTime += 3;
        OnAction();
        yield break;
    }
    private IEnumerator OnTwoStageAttack()//? 二階段絕招
    {
        TwoStage = true;
        yield return new WaitForSeconds(5);
        CallSmallTime += 5;
        OnAction();
        yield break;
    }
    private IEnumerator OnBigJump()//? 大跳躍，附帶落石
    {
        yield return new WaitForSeconds(4);
        CallSmallTime += 4;
        OnAction();
        yield break;
    }
    private IEnumerator OnThreeStageAttack()//? 三階段絕招
    {
        yield return new WaitForSeconds(5);
        CallSmallTime += 5;
        ThreeStageTime = 0;
        OnAction();
        yield break;
    }
    private void Start()
    {
        HpObject.SetActive(true);
    }
}
