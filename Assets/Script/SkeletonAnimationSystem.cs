using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

[RequireComponent(typeof(SkeletonAnimation))]
public abstract class SkeletonAnimationSystem : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    protected SkeletonAnimation GetSkeletonAnimation { get => skeletonAnimation; }
    protected abstract string DefaultAnimation { get; }//? 初始預設動畫(若沒有接續動畫則會自動接續該預設動畫)
    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        GetSkeletonAnimation.AnimationState.Complete += AnimationCompleteCallBack;//? 所有動畫結束時皆會調用
        GetSkeletonAnimation.AnimationState.Event += AnimationEventCallBack;//? Spine動畫發出事件時調用
    }
    private void AnimationCompleteCallBack(TrackEntry trackEntry)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, DefaultAnimation, true);
    }

    public void ChangeSkeletonAnimation(int mix, string animationName, bool loop)//? 更改Spine動畫(1.動畫混合、2.動畫名稱、3.是否循環)
    {
        skeletonAnimation.AnimationState.SetAnimation(mix, animationName, loop);
    }
    protected abstract void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e);
}
