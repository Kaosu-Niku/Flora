using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class Leafworm : Monster
{
    [SerializeField] float Speed;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        base.AnimationEventCallBack(trackEntry, e);
    }
    protected override void CustomAnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "IntimidateTrigger")
        {
            int a = Random.Range(0, 2);
            if (a == 1)
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack", false);
            return;
        }
        if (e.Data.Name == "IntimidateOut")
        {
            OnAction();
            return;
        }
        if (e.Data.Name == "AttackOpen")
        {
            Attack[0].SetActive(true);
            return;
        }
        if (e.Data.Name == "AttackClose")
        {
            Attack[0].SetActive(false);
            return;
        }
        if (e.Data.Name == "AttackOut")
        {
            OnAction();
            return;
        }
    }
    protected override IEnumerator CustomAction()
    {
        if (GetPlayerDistance() < 5)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Intimidate", false);
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
            //? 停在原地
            skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
            int waitTime = Random.Range(1, 4);
            yield return new WaitForSeconds(waitTime);
            OnAction();
        }
        yield break;
    }
    protected override void CustomHurt()
    {
        Attack[0].SetActive(false);
    }
}
