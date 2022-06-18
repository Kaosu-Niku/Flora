using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnlockDash : SpecialPlantSystem
{
    PlayerSystem GetPS;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        GetPS.CanDash = true;
        Destroy(this.gameObject);
    }
    private void Start()
    {
        GetPS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSystem>();
        GetPS.CanDash = false;
    }
}
