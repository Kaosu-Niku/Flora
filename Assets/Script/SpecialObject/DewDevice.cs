using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;
using UnityEngine.InputSystem;

public class DewDevice : SpecialPlantSystem
{
    [SerializeField] int SkillNumber;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "TriggerOut")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Triggered", true);
            return;
        }
    }

    protected override void DoSomething(InputAction.CallbackContext context)
    {
        if (SkillNumber > -1 && SkillNumber < 15)
        {
            PlayerSystemSO.SkillOpen[SkillNumber] = true;
            skeletonAnimation.AnimationState.SetAnimation(0, "Trigger", false);
        }
    }
    void Start()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "NotTriggered", true);
    }
}
