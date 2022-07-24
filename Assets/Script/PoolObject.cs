using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    //? 該物件屬於和物件池共存的，必須在物件池創建時才能被生成
    //? 該物件被開啟時會等待事情執行完畢或是提前被關閉時自動回歸物件池
    [HideInInspector] public string MyTag;
    Coroutine C;
    protected abstract IEnumerator Doing();
    IEnumerator Do()
    {
        yield return StartCoroutine(Doing());
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        C = StartCoroutine(Do());
    }
    private void OnDisable()
    {
        if (C != null)
            StopCoroutine(C);
        GameObjectPoolSO.BackObject(MyTag, this);
    }
}
