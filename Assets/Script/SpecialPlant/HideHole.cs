using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HideHole : SpecialPlantSystem
{

    bool CanUse = true;
    GameObject GetPlayer;
    PlayerSystem GetPlayerSystem;
    Coroutine Doing;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        if (CanUse == true)
        {
            CanUse = false;
            PlayerSystemSO.GetPlayerInvoke().SetCanFind(false);
            PlayerSystemSO.GetPlayerInvoke().SetCanControl(false);
            Doing = StartCoroutine(DoingIEnum());
        }
        else
        {
            CanUse = true;
            PlayerSystemSO.GetPlayerInvoke().SetCanFind(true);
            PlayerSystemSO.GetPlayerInvoke().SetCanControl(true);
            StopCoroutine(Doing);
        }

    }
    private IEnumerator DoingIEnum()
    {
        while (true)
        {
            GetPlayer.transform.position = transform.position;
            yield return 0;
        }
    }

    private void Start()
    {
        GetPlayer = GameObject.FindGameObjectWithTag("Player");
        if (GetPlayer != null)
            GetPlayerSystem = GetPlayer.GetComponent<PlayerSystem>();
    }
}
