using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class Monster3 : Monster
{
    [SerializeField] float Speed;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        base.AnimationEventCallBack(trackEntry, e);
    }
    protected override void CustomAnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        throw new System.NotImplementedException();
    }
    protected override IEnumerator CustomAction()
    {
        if (GetPlayerDistance() < 5)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Attack", false);
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

    }
}
