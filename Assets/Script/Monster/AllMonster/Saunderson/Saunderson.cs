using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class Saunderson : Monster
{
    Rigidbody2D Rigid;
    [SerializeField] GameObjectPool pool;
    AreaEffector2D Attack2;
    AreaEffector2D Attack3_1;
    AreaEffector2D Attack3_2;
    [SerializeField] Transform Attack5ShootTrans;
    bool IsAngry = false;//? 當此值為True時必定使出抓技，施放完畢就False
    bool HpLost50Check = false;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        base.AnimationEventCallBack(trackEntry, e);
    }
    protected override void CustomAnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "Attack1Open")
        {
            Attack[0].SetActive(true);
            return;
        }
        if (e.Data.Name == "Attack1Close")
        {
            Attack[0].SetActive(false);
            return;
        }
        if (e.Data.Name == "Attack1Out")
        {
            OnAction();
            return;
        }
        if (e.Data.Name == "Attack2Trigger")
        {
            // Attack2.transform.position = transform.position;
            // if (transform.eulerAngles.y < 90)
            // {
            //     Attack2.gameObject.transform.rotation = Quaternion.identity;
            //     Attack2.forceAngle = 25;
            // }
            // else
            // {
            //     Attack2.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            //     Attack2.forceAngle = 155;
            // }
            //Attack2.gameObject.SetActive(true);
            //! 發射羽毛
            // transform.Translate(0, 50, 0);
            // if (C != null)
            //     StopCoroutine(C);
            // C = StartCoroutine(Attack5IEnum());
            return;
        }
        if (e.Data.Name == "Attack2Out")
        {
            OnAction();
            return;
        }
        if (e.Data.Name == "Attack3Trigger")
        {
            Rigid.gravityScale = 0;
            Attack3_1.gameObject.transform.position = transform.position;
            Attack3_2.gameObject.transform.position = transform.position;
            Attack3_1.gameObject.SetActive(true);
            Attack3_2.gameObject.SetActive(true);
            return;
        }
        if (e.Data.Name == "Attack3Out")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "FlyLoop", true);
            if (C != null)
                StopCoroutine(C);
            C = StartCoroutine(Attack3_1IEnum());
            return;
        }
        if (e.Data.Name == "Attack4Open")
        {
            Rigid.gravityScale = 1;
            Attack[1].SetActive(true);
            return;
        }
        if (e.Data.Name == "Attack4Close")
        {
            Attack[1].SetActive(false);
            return;
        }
        if (e.Data.Name == "Attack4Out")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
            OnAction();
            return;
        }
    }
    protected override IEnumerator CustomAction()
    {
        // int r = Random.Range(1, 4);
        // switch (r)
        // {
        //     case 1: skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false); break;
        //     case 2: skeletonAnimation.AnimationState.SetAnimation(0, "Attack2", false); break;
        //     case 3: skeletonAnimation.AnimationState.SetAnimation(0, "Attack3", false); break;
        // }
        if (Hp < MaxHp / 2 && HpLost50Check == false)//? 血量低於50%必定施放一次抓技
        {
            IsAngry = true;
            HpLost50Check = true;
        }
        if (IsAngry == false)
        {
            //? 每次攻擊有20%機率飛上天
            if (Random.Range(1, 11) > 8)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack3", false);
                //! 等空中射羽毛攻擊動作完成才再修改
            }
            else
            {
                //? 原地休息一秒
                skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
                yield return new WaitForSeconds(1);
                LookPlayer();
                //? 距離大於5射羽毛或持續接近玩家
                if (GetPlayerDistance() > 5)
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, "Walk", true);
                    while (GetPlayerDistance() > 5)
                    {
                        transform.Translate(5 * Time.deltaTime, 0, 0);
                        yield return 0;
                    }
                    //! 等地面射羽毛攻擊動作完成才再修改
                }
                //? 甩腳爪
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false);
            }
        }
        else
        {
            //! 等抓技攻擊動作完成才再修改
        }
        yield break;
    }
    protected override void CustomHurt()
    {
        if (C != null)
            StopCoroutine(C);
        Attack[0].SetActive(false);
        Attack[1].SetActive(false);
        Attack2.gameObject.SetActive(false);
        Attack3_1.gameObject.SetActive(false);
        Attack3_2.gameObject.SetActive(false);
        IsAngry = true;
        skeletonRootMotion.rootMotionScaleY = 1;
        Rigid.gravityScale = 1;
    }
    Coroutine C;
    IEnumerator Attack3_1IEnum()//? 墜地鳥嘴戳
    {
        for (float x = 0; x < 3; x += Time.deltaTime)
        {
            LookPlayer();
            transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerSystemSO.GetPlayerInvoke().transform.position.x, transform.position.y, 0), 2 * Time.deltaTime);
            yield return 0;
        }
        transform.Translate(-1, 0, 0);
        skeletonAnimation.AnimationState.SetAnimation(0, "Attack4", false);
    }
    IEnumerator Attack5IEnum()//? 發射羽毛
    {
        for (int x = 0; x < 20; x++)
        {
            pool.GetObject("Attack5", Attack5ShootTrans.position, Quaternion.Euler(0, transform.eulerAngles.y, Attack5ShootTrans.eulerAngles.z));
            yield return new WaitForSeconds(0.1f);
        }
    }
    new void Start()
    {
        base.Start();
        Rigid = GetComponent<Rigidbody2D>();
        Attack2 = pool.GetObject("Attack2", transform.position, Quaternion.identity).GetComponent<AreaEffector2D>();
        Attack3_1 = pool.GetObject("Attack3-1", transform.position, Quaternion.identity).GetComponent<AreaEffector2D>();
        Attack3_2 = pool.GetObject("Attack3-2", transform.position, Quaternion.identity).GetComponent<AreaEffector2D>();
        Attack2.gameObject.SetActive(false);
        Attack3_1.gameObject.SetActive(false);
        Attack3_2.gameObject.SetActive(false);

    }
}
