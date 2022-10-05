using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPoolObject : IDefaultObject
{
    //? 該物件屬於和物件池共存的，必須在物件池創建時才能被生成
    [HideInInspector] public GameObjectPool MyPool;
    [HideInInspector] public string MyTag;
    Coroutine C;
    protected override IEnumerator Doing()
    {
        yield return StartCoroutine(Doing2());
    }
    protected abstract IEnumerator Doing2();
    new void OnDisable()
    {
        base.OnDisable();
        if (MyPool != null)
            MyPool.BackObject(MyTag, this);
    }
}
