using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class Saunderson : Monster
{
    [SerializeField] float Speed;
    Rigidbody2D Rigid;
    [SerializeField] GameObjectPool pool;
    AreaEffector2D Attack2;
    AreaEffector2D Attack3_1;
    AreaEffector2D Attack3_2;
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
            transform.Translate(0, 50, 0);
            for (int x = 18; x < 37; x++)
            {
                pool.GetObject("Attack5", transform.position, Quaternion.Euler(0, transform.eulerAngles.y, 0 + x * 10));
            }
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
            StartCoroutine(Attack3_1Ienum());
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
        LookPlayer();
        while (true)
        {
            int r = 2; Random.Range(1, 4);
            switch (r)
            {
                case 1: skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false); break;
                case 2: skeletonAnimation.AnimationState.SetAnimation(0, "Attack2", false); break;
                case 3: skeletonAnimation.AnimationState.SetAnimation(0, "Attack3", false); break;
            }
            yield return new WaitForSeconds(10);

        }
        // if (GetPlayerDistance() < 5)
        // {

        // }
        // else
        // {
        //     //? 左右隨機移動
        //     skeletonAnimation.AnimationState.SetAnimation(0, "Walk", true);
        //     transform.Rotate(0, 180, 0);
        //     int moveTime = Random.Range(1, 4);
        //     for (float a = 0; a < moveTime; a += Time.deltaTime)
        //     {
        //         transform.Translate(Speed * Time.deltaTime, 0, 0);
        //         yield return 0;
        //     }
        //     //? 停在原地
        //     skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
        //     int waitTime = Random.Range(1, 4);
        //     yield return new WaitForSeconds(waitTime);
        //     OnAction();
        // }
        yield break;
    }
    protected override void CustomHurt()
    {
        Attack[0].SetActive(false);
        Attack[1].SetActive(false);
        Attack2.gameObject.SetActive(false);
        Attack3_1.gameObject.SetActive(false);
        Attack3_2.gameObject.SetActive(false);
        Rigid.gravityScale = 1;
    }
    IEnumerator Attack3_1Ienum()
    {
        yield return new WaitForSeconds(2);
        skeletonAnimation.AnimationState.SetAnimation(0, "Attack4", false);
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
