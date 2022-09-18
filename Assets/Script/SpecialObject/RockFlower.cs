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
    GameObject GetPlayer;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        if (CanUse == true)
            StartCoroutine(Doing());
    }
    private IEnumerator Doing()
    {
        CanUse = false;
        if (GetPlayer != null)
        {
            PlayerFirstPos = GetPlayer.transform.position;
            if (MoveDis.x < 0)
                GetPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                GetPlayer.transform.rotation = Quaternion.identity;
            GetPlayer.transform.Translate(MoveDis.x, MoveDis.y, 0);
        }
        yield return new WaitForSeconds(ResetTime);
        if (GetPlayer != null)
            GetPlayer.transform.position = PlayerFirstPos;
        CanUse = true;
    }
    private void Start()
    {
        GetPlayer = GameObject.FindGameObjectWithTag("Player");
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
