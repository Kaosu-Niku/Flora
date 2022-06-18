using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AddPlayerSpeed : SpecialPlantSystem
{
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        PlayerDataSO.PlayerMaxSpeed = 15;
        Destroy(this.gameObject);
    }
    private void Start()
    {
        PlayerDataSO.PlayerMaxSpeed = 10;
    }
}
