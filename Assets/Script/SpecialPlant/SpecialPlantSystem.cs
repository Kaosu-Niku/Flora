using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine.Unity;

public abstract class SpecialPlantSystem : SkeletonAnimationSystem
{
    MyInput myInput;
    protected MyInput GetInput { get => myInput; }
    private void Doing(InputAction.CallbackContext context)
    {
        DoSomething(context);
        myInput.Player.Action.started -= Doing;
    }
    protected abstract void DoSomething(InputAction.CallbackContext context);//? 要做的事
    new void Awake()
    {
        base.Awake();
        myInput = new MyInput();
    }
    private void OnEnable()
    {
        myInput.Enable();
    }
    private void OnDisable()
    {
        myInput.Disable();
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//? 玩家進入範圍
            myInput.Player.Action.started += Doing;
    }

    protected void OnTriggerExit2D(Collider2D other)//? 玩家離開範圍
    {
        if (other.gameObject.CompareTag("Player"))
            myInput.Player.Action.started -= Doing;
    }
}
