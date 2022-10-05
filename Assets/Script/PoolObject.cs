using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : IPoolObject
{
    //?  預設物件池物件，給定的時間自動開關，如果時間為0則不會關閉
    [SerializeField] float FalseTime;
    protected override IEnumerator Doing2()
    {
        if (FalseTime != 0)
        {
            yield return new WaitForSeconds(FalseTime);
        }
        else
        {
            while (true)
            {
                yield return 0;
            }
        }
    }

}
