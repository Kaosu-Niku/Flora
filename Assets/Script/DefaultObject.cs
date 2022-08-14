using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObject : IDefaultObject
{
    [SerializeField] float FalseTime;
    protected override IEnumerator Doing()
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
