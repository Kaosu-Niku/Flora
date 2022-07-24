using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class SpecialPlantSystem : MonoBehaviour
{
    MyInput GetInput;
    protected abstract void DoSomething(InputAction.CallbackContext context);//? 要做的事
    private void Awake()
    {
        GetInput = new MyInput();
    }
    private void OnEnable()
    {
        GetInput.Enable();
    }
    private void OnDisable()
    {
        GetInput.Player.Action.started -= DoSomething;
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//? 玩家進入範圍
            GetInput.Player.Action.started += DoSomething;
    }

    protected void OnTriggerExit2D(Collider2D other)//? 玩家離開範圍
    {
        if (other.gameObject.CompareTag("Player"))
            GetInput.Player.Action.started -= DoSomething;
    }
}
