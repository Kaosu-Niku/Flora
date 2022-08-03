using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public abstract class SkeletonAnimationSystem : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation { get; private set; }
    protected void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        if (skeletonAnimation != null)
        {
            //? 所有動畫結束時皆會調用
            skeletonAnimation.AnimationState.Complete += AnimationCompleteCallBack;
            //? Spine動畫發出事件時調用
            skeletonAnimation.AnimationState.Event += AnimationEventCallBack;
        }
    }
    private void AnimationCompleteCallBack(TrackEntry trackEntry)
    {
        if (trackEntry.Next == null)
        {
            //? 第0通道始終會保持著有動畫接續的狀態(無動作就切換回預設動作並循環)
            //? 非第0通道的其他通道都是為了做混合動畫，其他通道不需要預設動作，直接清除軌道就好。
            if (trackEntry.TrackIndex != 0)
                skeletonAnimation.AnimationState.ClearTrack(trackEntry.TrackIndex);
        }
    }
    protected abstract void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e);
}
