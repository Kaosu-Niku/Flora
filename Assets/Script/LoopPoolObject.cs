using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPoolObject : PoolObject
{
    protected override IEnumerator Doing()
    {
        while (true)
        {
            yield return 0;
        }
    }

}
