using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine;

public class RockFlower : SpecialPlantSystem
{
    bool CanUse = true;
    [SerializeField] Vector2 MoveDis;
    [SerializeField] float ResetTime;
    Vector3 PlayerFirstPos;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        if (CanUse == true)
            StartCoroutine(Doing());
    }
    private IEnumerator Doing()
    {
        CanUse = false;
        PlayerFirstPos = PlayerSystemSO.GetPlayerInvoke().transform.position;
        if (MoveDis.x < 0)
            PlayerSystemSO.GetPlayerInvoke().transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            PlayerSystemSO.GetPlayerInvoke().transform.rotation = Quaternion.identity;
        PlayerSystemSO.GetPlayerInvoke().transform.Translate(MoveDis.x, MoveDis.y, 0);
        yield return new WaitForSeconds(ResetTime);
        PlayerSystemSO.GetPlayerInvoke().transform.position = PlayerFirstPos;
        CanUse = true;
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
