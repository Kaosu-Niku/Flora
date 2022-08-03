using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine;

public class AddPlayerSpeed : SpecialPlantSystem
{
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        PlayerDataSO.MaxSpeed = 15;
        Destroy(this.gameObject);
    }
    private void Start()
    {
        PlayerDataSO.MaxSpeed = 10;
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
