using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class FloraArmor : Monster
{
    [SerializeField] float Speed;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
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
    protected override void CustomAnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        throw new System.NotImplementedException();
    }
    protected override IEnumerator CustomAction()
    {
        if (GetPlayerDistance() < 10)
        {
            LookPlayer();
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

            if (GetPlayerDistance() < 10)
            {
                LookPlayer();
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack", false);
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
        Attack[0].SetActive(false);
    }
}
