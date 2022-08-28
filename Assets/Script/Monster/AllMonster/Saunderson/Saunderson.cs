using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class Saunderson : Monster
{
    Rigidbody2D Rigid;
    [SerializeField] GameObjectPool pool;
    AreaEffector2D Attack2;
    AreaEffector2D Fly_1;
    AreaEffector2D Fly_2;
    int WhichFlyAttack;//? 使出哪一個空中攻擊(不同的空中攻擊，飛行高度有差) 1.下墜戳擊。2.空中射羽毛。3.抓技。
    bool FlyCheck = false;//? 使出空中攻擊後的落地判定(落地後才接OnAction重置行動)
    [SerializeField] Transform Attack5ShootTrans;
    bool Attack6Check;//? 抓技是否有抓到玩家
    bool IsAngry = false;//? 當此值為True時必定使出抓技，施放完畢就False
    bool HpLost50Check = false;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        base.AnimationEventCallBack(trackEntry, e);
    }
    protected override void CustomAnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "FlyTrigger")
        {
            Rigid.gravityScale = 0;
            Fly_1.gameObject.transform.position = transform.position;
            Fly_2.gameObject.transform.position = transform.position;
            Fly_1.gameObject.SetActive(true);
            Fly_2.gameObject.SetActive(true);
            return;
        }
        if (e.Data.Name == "FlyOut")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "FlyLoop", true);
            LookPlayer();
            if (C != null)
                StopCoroutine(C);
            C = StartCoroutine(FlyAttackIEnum());
            return;
        }
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
            Attack2.transform.position = transform.position;
            if (transform.eulerAngles.y < 90)
            {
                Attack2.gameObject.transform.rotation = Quaternion.identity;
                Attack2.forceAngle = 25;
            }
            else
            {
                Attack2.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                Attack2.forceAngle = 155;
            }
            Attack2.gameObject.SetActive(true);
            return;
        }
        if (e.Data.Name == "Attack2Out")
        {
            OnAction();
            return;
        }
        if (e.Data.Name == "Attack4Open")
        {
            Attack[1].SetActive(true);
            return;
        }
        if (e.Data.Name == "Attack4Close")
        {
            Attack[1].SetActive(false); GameManagerSO.ImpluseInvoke(1);
            return;
        }
        if (e.Data.Name == "Attack4Out")
        {
            Rigid.gravityScale = 1;
            skeletonRootMotion.rootMotionScaleY = 1;
            OnAction();
            return;
        }
        if (e.Data.Name == "Attack5Trigger")
        {
            for (int x = 0; x < 18; x++)
                pool.GetObject("Attack5", Attack5ShootTrans.position, Quaternion.Euler(0, transform.eulerAngles.y, 180 + x * 10));
            return;
        }
        if (e.Data.Name == "Attack5Out")
        {
            FlyCheck = true;
            Rigid.gravityScale = 1;
            skeletonAnimation.AnimationState.SetAnimation(0, "FlyLoop", true);
            return;
        }
        if (e.Data.Name == "Attack6Open")
        {
            if (C != null)
                StopCoroutine(C);
            C = StartCoroutine(Attack6IEnum());
            Attack[2].SetActive(true);
            return;
        }
        if (e.Data.Name == "Attack6Out")
        {
            Attack[2].SetActive(false);
            if (C != null)
                StopCoroutine(C);
            if (Attack6Check == true)
            {
                Attack6Check = false;
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack7", false);
            }
            else
            {
                Rigid.gravityScale = 1;
                Attack[2].SetActive(false);
                OnAction();
            }

            return;
        }
        if (e.Data.Name == "Attack7Out")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Attack8", false);
            PlayerSystemSO.GetPlayerInvoke().UntieBondage();
            return;
        }
        if (e.Data.Name == "Attack8Trigger")
        {
            PlayerSystemSO.GetPlayerInvoke().ForDamage(2);
            return;
        }
        if (e.Data.Name == "Attack8Free")
        {
            PlayerSystemSO.GetPlayerInvoke().UntieBondage();
            return;
        }
        if (e.Data.Name == "Attack8Out")
        {
            OnAction();
            return;
        }
    }
    protected override IEnumerator CustomAction()
    {
        switch (2)
        {
            case 1: skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false); break;
            case 2: WhichFlyAttack = 0; skeletonRootMotion.rootMotionScaleY = 2; skeletonAnimation.AnimationState.SetAnimation(0, "Fly", false); break;
            case 3: WhichFlyAttack = 1; skeletonAnimation.AnimationState.SetAnimation(0, "Fly", false); break;
            case 4: WhichFlyAttack = 2; skeletonAnimation.AnimationState.SetAnimation(0, "Fly", false); break;
        }
        yield break;
        if (Hp < MaxHp / 2 && HpLost50Check == false)//? 血量低於50%必定施放一次抓技
        {
            IsAngry = true;
            HpLost50Check = true;
        }
        if (IsAngry == false)
        {
            //? 每次攻擊有20%機率飛上天
            if (Random.Range(1, 11) > 0)// 8 
            {
                WhichFlyAttack = Random.Range(0, 2);
                if (WhichFlyAttack == 0)
                    skeletonRootMotion.rootMotionScaleY = 2;
                skeletonAnimation.AnimationState.SetAnimation(0, "Fly", false);
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
                    if (Random.Range(0, 5) > 10)
                    {
                        yield break;
                    }
                    else
                    {
                        skeletonAnimation.AnimationState.SetAnimation(0, "Walk", true);
                        while (GetPlayerDistance() > 5)
                        {
                            transform.Translate(5 * Time.deltaTime, 0, 0);
                            yield return 0;
                        }
                    }
                }
                //? 甩腳爪
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false);
            }
        }
        else
        {
            //? 抓技
            WhichFlyAttack = 2;
            skeletonAnimation.AnimationState.SetAnimation(0, "Fly", false);
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
        Fly_1.gameObject.SetActive(false);
        Fly_2.gameObject.SetActive(false);
        IsAngry = true;
        skeletonRootMotion.rootMotionScaleY = 1;
        Rigid.gravityScale = 1;
    }
    Coroutine C;
    IEnumerator FlyAttackIEnum()//? 所有空中攻擊皆使用該協程
    {
        switch (WhichFlyAttack)
        {
            case 0:
                //? 墜落戳擊
                for (float x = 0; x < 3; x += Time.deltaTime)
                {
                    LookPlayer();
                    transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerSystemSO.GetPlayerInvoke().transform.position.x, transform.position.y, 0), 2 * Time.deltaTime);
                    yield return 0;
                }
                transform.Translate(-1, 0, 0);
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack4", false);
                break;
            case 1:
                //? 空中射羽毛
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack5", false);
                break;
            case 2:
                //? 抓技
                yield return new WaitForSeconds(1);
                PlayerSystemSO.GetPlayerInvoke().BondageEvent += PlayerBondageCheck;
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack6", false);
                break;
        }
    }
    IEnumerator Attack6IEnum()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerSystemSO.GetPlayerInvoke().transform.position.x, transform.position.y, 0), 5 * Time.deltaTime);
            yield return 0;
        }
    }
    void PlayerBondageCheck()
    {
        PlayerSystemSO.GetPlayerInvoke().BondageEvent -= PlayerBondageCheck;
        Attack6Check = true;
    }
    new void Start()
    {
        base.Start();
        Rigid = GetComponent<Rigidbody2D>();
        Attack2 = pool.GetObject("Attack2", transform.position, Quaternion.identity).GetComponent<AreaEffector2D>();
        Fly_1 = pool.GetObject("Fly-1", transform.position, Quaternion.identity).GetComponent<AreaEffector2D>();
        Fly_2 = pool.GetObject("Fly-2", transform.position, Quaternion.identity).GetComponent<AreaEffector2D>();
        Attack2.gameObject.SetActive(false);
        Fly_1.gameObject.SetActive(false);
        Fly_2.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        PlayerSystemSO.GetPlayerInvoke().BondageEvent -= PlayerBondageCheck;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (FlyCheck == true)
        {
            Vector2 contactsNormal = other.contacts[0].normal;
            float colAngle = (Mathf.Atan(contactsNormal.y / contactsNormal.x)) * 180 / Mathf.PI;
            if (colAngle < 120 && colAngle > 60 || colAngle > -120 && colAngle < -60)
            {
                FlyCheck = false;
                OnAction();
            }
        }

    }
}
