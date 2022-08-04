using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class Saunderson : Monster
{
    [SerializeField] float Speed;
    [SerializeField] AreaEffector2D GetArea;
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
        GetArea.gameObject.SetActive(false);
        while (true)
        {
            int r = Random.Range(0, 5);
            if (r == 0)
            {
                LookPlayer();
                if (transform.eulerAngles.y < 90)
                    GetArea.forceAngle = 25;
                else
                    GetArea.forceAngle = 155;
                GetArea.gameObject.SetActive(true);
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack2", false);
                yield return new WaitForSeconds(1);
                GetArea.gameObject.SetActive(false);
                yield return new WaitForSeconds(4);
            }
            else
            {
                Attack[0].SetActive(true);
                skeletonAnimation.AnimationState.SetAnimation(0, "Attack1", false);
                yield return new WaitForSeconds(1);
                Attack[0].SetActive(false);
                yield return new WaitForSeconds(1);
            }

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

    }
}
