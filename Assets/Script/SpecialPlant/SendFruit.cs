using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SendFruit : SpecialPlantSystem
{
    bool CanUse = true;
    Vector3 newPos;
    GameObject GetPlayer;
    PlayerSystem GetPlayerSystem;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        if (CanUse == true)
        {
            CanUse = false;
            if (GetPlayerSystem != null)
            {
                GetPlayer.transform.position = newPos;
                GetPlayerSystem.CallWallJump(true);
            }
        }
    }
    private void Start()
    {
        GetPlayer = GameObject.FindGameObjectWithTag("Player");
        if (GetPlayer != null)
            GetPlayerSystem = GetPlayer.GetComponent<PlayerSystem>();
        newPos = new Vector3(transform.position.x - 1.5f, transform.position.y - 2, 0);
    }
    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player"))
            CanUse = true;
    }
}
