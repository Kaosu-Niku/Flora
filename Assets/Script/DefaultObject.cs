using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaultObject : MonoBehaviour
{
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
    }
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}