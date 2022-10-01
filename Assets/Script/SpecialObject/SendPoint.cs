using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine;

public class SendPoint : SpecialPlantSystem
{
    [SerializeField] Transform SendTrans;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        StartCoroutine(Doing());
    }
    private IEnumerator Doing()
    {
        if (SendTrans != null)
            PlayerSystemSO.GetPlayerInvoke().transform.position = SendTrans.position;
        yield break;
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
