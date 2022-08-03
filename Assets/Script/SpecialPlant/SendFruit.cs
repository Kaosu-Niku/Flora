using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine;

public class SendFruit : SpecialPlantSystem
{
    bool CanUse = true;
    Vector3 newPos;
    PlayerSystem GetPlayer;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        if (CanUse == true)
        {
            CanUse = false;
            if (GetPlayer != null)
            {
                GetPlayer.transform.position = newPos;
                GetPlayer.CallWallJump();
            }
        }
    }
    private void Start()
    {
        GetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSystem>();
        newPos = new Vector3(transform.position.x, transform.position.y - 2, 0);
    }
    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player"))
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
