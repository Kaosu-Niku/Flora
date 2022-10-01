using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine;

public class BigJumpPlant : SpecialPlantSystem
{
    bool CanUse = true;
    [SerializeField] int Power;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "JumpTrigger")
        {
            PlayerSystemSO.GetPlayerInvoke().CallJump(Power);
            return;
        }
        if (e.Data.Name == "JumpOut")
        {
            CanUse = true;
            skeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
            return;
        }
    }
    protected override void DoSomething(InputAction.CallbackContext context)
    {

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CanUse == true)
            {
                CanUse = false;
                skeletonAnimation.AnimationState.SetAnimation(0, "Jump", false);
            }
        }
    }
}
