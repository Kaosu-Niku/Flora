using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine;

public class HideHole : SpecialPlantSystem
{

    bool CanUse = true;
    Coroutine Doing;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        if (CanUse == true)
        {
            CanUse = false;
            PlayerSystemSO.GetPlayerInvoke().SetCanFind(false);
            PlayerSystemSO.GetPlayerInvoke().SetCanControl(false);
            Doing = StartCoroutine(DoingIEnum());
        }
        else
        {
            CanUse = true;
            PlayerSystemSO.GetPlayerInvoke().SetCanFind(true);
            PlayerSystemSO.GetPlayerInvoke().SetCanControl(true);
            StopCoroutine(Doing);
        }

    }
    private IEnumerator DoingIEnum()
    {
        while (true)
        {
            PlayerSystemSO.GetPlayerInvoke().transform.position = transform.position;
            yield return 0;
        }
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
