using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class BoomFish : Monster
{
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        base.AnimationEventCallBack(trackEntry, e);
    }
    protected override void CustomAnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "AttackTrigger")
        {
            if (GetPlayerDistance() > 5)
            {
                OnAction();
            }
            else
            {
                Hurt(Hp + 1, 0);
            }
            return;
        }
        if (e.Data.Name == "DieTrigger")
        {
            StartCoroutine(DieIEnum());
            return;
        }
    }
    IEnumerator DieIEnum()
    {
        Attack[0].SetActive(true);
        Attack[1].SetActive(true);
        yield return new WaitForFixedUpdate();
        yield return 0;
        Attack[0].SetActive(false);
        Attack[1].SetActive(false);
    }
    protected override IEnumerator CustomAction()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name != "Idle")
            skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
        if (GetPlayerDistance() > 15)
        {
            yield return new WaitForSeconds(1);
            OnAction();
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Walk", true);
            while (GetPlayerDistance() > 3)
            {
                LookPlayer();
                for (float t = 0; t < 0.5f; t += Time.deltaTime)
                {
                    transform.Translate(10 * Time.deltaTime, 0, 0);
                    yield return 0;
                }
            }
            skeletonAnimation.AnimationState.SetAnimation(0, "Attack", false);
        }
        yield break;
    }
    protected override void CustomHurt()
    {

    }
}
