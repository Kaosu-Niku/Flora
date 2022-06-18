using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOne : PlayerAttack
{
    new void Start()
    {
        base.Start();
        transform.Translate(5,0,0);
    }
}
