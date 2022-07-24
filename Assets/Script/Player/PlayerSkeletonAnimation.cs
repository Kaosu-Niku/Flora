using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;

public class PlayerSkeletonAnimation : SkeletonAnimationSystem
{
    protected override string DefaultAnimation => "Idle";

    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {

    }
}
