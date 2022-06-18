using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnlockObject : SpecialPlantSystem
{
    [SerializeField] List<GameObject> Obj = new List<GameObject>();
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        for (int x = 0; x < Obj.Count; x++)
        {
            Obj[x].SetActive(true);
        }
    }
    void Start()
    {
        if (Obj.Count > 0)
        {
            for (int x = 0; x < Obj.Count; x++)
                Obj[x].SetActive(false);
        }

    }
}
