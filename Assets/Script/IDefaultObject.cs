using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDefaultObject : MonoBehaviour
{
    //? 該物件被開啟時會等待事情執行完畢或是提前被關閉
    Coroutine C;
    protected abstract IEnumerator Doing();
    IEnumerator Do()
    {
        yield return StartCoroutine(Doing());
        gameObject.SetActive(false);
    }

    protected void OnEnable()
    {
        C = StartCoroutine(Do());
    }
    protected void OnDisable()
    {
        if (C != null)
            StopCoroutine(C);
    }
}
