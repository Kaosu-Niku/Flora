using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine;

public class HangingBug : SpecialPlantSystem
{
    bool CanUse = true;
    [SerializeField] Transform SetTrans;
    [SerializeField] SpriteRenderer Hint;
    bool ani;
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "IntoRangeOut")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "InRange", true);
            return;
        }
        if (e.Data.Name == "LeaveRangeOut")
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "Outside", true);
            return;
        }
        if (e.Data.Name == "TriggerOut")
        {
            ani = false;
            if (CanUse == true)
                skeletonAnimation.AnimationState.SetAnimation(0, "IntoRange", false);
            else
                skeletonAnimation.AnimationState.SetAnimation(0, "LeaveRange", false);
            return;
        }
    }
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        if (CanUse == true)
        {
            CanUse = false;
            StartCoroutine(DoingIEnum());
        }
    }
    IEnumerator DoingIEnum()
    {
        PlayerSystemSO.GetPlayerInvoke().SetCanControl(false);
        PlayerSystemSO.GetPlayerInvoke().Rigid.gravityScale = 0;
        ani = true;
        Hint.enabled = true;
        for (float t = 0; t < 5; t += Time.deltaTime)
        {
            PlayerSystemSO.GetPlayerInvoke().transform.position = SetTrans.position;
            SetTrans.Rotate(0, 0, GetInput.Player.Move.ReadValue<float>() * 2);
            if (GetInput.Player.Jump.triggered)
            {
                PlayerSystemSO.GetPlayerInvoke().SetCanControl(true);
                PlayerSystemSO.GetPlayerInvoke().CallJump(0);
                PlayerSystemSO.GetPlayerInvoke().Rigid.AddRelativeForce(SetTrans.right * 2000);
                skeletonAnimation.AnimationState.SetAnimation(0, "Trigger", false);
                Hint.enabled = false;
                yield break;
            }
            yield return 0;
        }
        PlayerSystemSO.GetPlayerInvoke().SetCanControl(true);
        PlayerSystemSO.GetPlayerInvoke().Rigid.gravityScale = 10;
        ani = false;
        Hint.enabled = false;
    }
    private void Start()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Outside", true);
        Hint.enabled = false;
    }
    new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player"))
        {
            CanUse = true;
            if (ani == false)
                skeletonAnimation.AnimationState.SetAnimation(0, "IntoRange", false);
        }
    }
    new void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player"))
        {
            CanUse = false;
            if (ani == false)
                skeletonAnimation.AnimationState.SetAnimation(0, "LeaveRange", false);
        }
    }
}
