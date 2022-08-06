using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class FloraArmor : Monster
{
    [SerializeField] float Speed;
    bool Attack2Check = false;
    Rigidbody2D Rigid;
    Coroutine C;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        base.AnimationEventCallBack(trackEntry, e);
    }
    protected override void CustomAnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "Attack1Out")
        {
            Attack[0].SetActive(true);
            Attack2Check = true;
            C = StartCoroutine(Attack2IEnum());
            skeletonAnimation.AnimationState.SetAnimation(0, "Attack2", true);
            return;
        }
        if (e.Data.Name == "Attack3Out")
        {
            OnAction();
            return;
        }
    }
    protected override IEnumerator CustomAction()
    {
        if (GetPlayerDistance() < 10)
        {
            LookPlayer();
            skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false);
        }
        else
        {
            //? 左右隨機移動
            skeletonAnimation.AnimationState.SetAnimation(0, "Walk", true);
            transform.Rotate(0, 180, 0);
            int moveTime = Random.Range(1, 4);
            for (float a = 0; a < moveTime; a += Time.deltaTime)
            {
                transform.Translate(Speed * Time.deltaTime, 0, 0);
                yield return 0;
            }

            if (GetPlayerDistance() < 10)
            {
                LookPlayer();
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false);
            }
            else
            {
                //? 停在原地
                skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
                int waitTime = Random.Range(1, 4);
                yield return new WaitForSeconds(waitTime);
                OnAction();
            }
        }
        yield break;
    }
    protected override void CustomHurt()
    {
        if (C != null)
            StopCoroutine(C);
        LookPlayer();
        Rigid.gravityScale = 1;
        Attack[0].SetActive(false);
    }
    IEnumerator Attack2IEnum()
    {
        Vector3 dir = (PlayerSystemSO.GetPlayerInvoke().transform.position + Vector3.up * 2) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Rigid.gravityScale = 0;
        for (float x = 0; x < 1; x += Time.deltaTime)
        {
            if (Attack2Check == false)
                break;
            transform.Translate(30 * Time.deltaTime, 0, 0);
            yield return 0;
        }
        LookPlayer();
        Rigid.gravityScale = 1;
        Attack[0].SetActive(false);
        skeletonAnimation.AnimationState.SetAnimation(0, "Attack3", false);
    }
    new void Start()
    {
        base.Start();
        Rigid = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Attack2Check = false;
    }
}
