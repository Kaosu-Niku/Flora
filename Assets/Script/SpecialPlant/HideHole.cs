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
            PlayerDataSO.CantFindPlayer = true;
            PlayerDataSO.CanControl = false;
            Doing = StartCoroutine(DoingIEnum());
        }
        else
        {
            CanUse = true;
            PlayerDataSO.CantFindPlayer = false;
            PlayerDataSO.CanControl = true;
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
