using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class NpcPop : SkeletonAnimationSystem
{
    void Start()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Clear", false);
    }
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "DialoginOut")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "DialogDefault", true);
        }
        if (e.Data.Name == "TaskNewOut")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "TaskDefault", true);
        }
    }
    public void CallDialog()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Dialogin", false);
    }
    public void CallTask()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "TaskNew", false);
    }
    public void CallClear()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Clear", false);
    }

}
