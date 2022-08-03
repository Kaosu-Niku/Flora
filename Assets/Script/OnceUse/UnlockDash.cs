using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine;

public class UnlockDash : SpecialPlantSystem
{
    PlayerSystem GetPS;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        GetPS.SetCanFlash(true);
        Destroy(gameObject);
    }
    private void Start()
    {
        GetPS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSystem>();
        GetPS.SetCanFlash(false);
    }
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "JumpOut")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "a", true);
            return;
        }
    }
}
