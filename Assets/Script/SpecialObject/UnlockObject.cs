using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine;

public class UnlockObject : SpecialPlantSystem
{
    bool Do = false;
    [SerializeField] List<GameObject> Obj = new List<GameObject>();
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        if (Do == false)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "TurnOn", false);
            Do = true;
        }
    }
    void Start()
    {
        for (int x = 0; x < Obj.Count; x++)
            Obj[x].SetActive(false);
    }
    protected override void AnimationEventCallBack(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "TurnOnOut")
        {
            for (int x = 0; x < Obj.Count; x++)
                Obj[x].SetActive(true);
            skeletonAnimation.AnimationState.SetAnimation(0, "Open", true);
            return;
        }
    }
}
