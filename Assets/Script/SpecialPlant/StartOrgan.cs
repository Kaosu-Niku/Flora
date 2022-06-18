using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartOrgan : SpecialPlantSystem
{
    bool CanUse = false;
    [SerializeField] List<GameObject> StartObject = new List<GameObject>();
    [SerializeField] GameObject Close;
    [SerializeField] GameObject Open;
    protected override void DoSomething(InputAction.CallbackContext context)
    {
        StartCoroutine(Doing());
    }
    private void Start()
    {
        if (StartObject.Count != 0)
        {
            for (int x = 0; x < StartObject.Count; x++)
                StartObject[x].SetActive(false);
        }
        Close.SetActive(true);
        Open.SetActive(false);
    }
    private IEnumerator Doing()
    {
        if (CanUse == false)
        {
            CanUse = true;
            if (StartObject.Count != 0)
            {
                for (int x = 0; x < StartObject.Count; x++)
                    StartObject[x].SetActive(true);
            }
            Close.SetActive(false);
            Open.SetActive(true);
        }
        yield break;
    }
}
