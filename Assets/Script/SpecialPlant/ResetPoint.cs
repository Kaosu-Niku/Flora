using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Spine;

public class ResetPoint : SpecialPlantSystem
{
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        StartCoroutine(Doing());
    }
    private IEnumerator Doing()
    {
        GameDataSO.LastScene = SceneManager.GetActiveScene().name;
        GameDataSO.ResetPoint[0] = transform.position.x;
        GameDataSO.ResetPoint[1] = transform.position.y;
        AllEventSO.LoadSceneAsyncTrigger(GameDataSO.LastScene);
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

