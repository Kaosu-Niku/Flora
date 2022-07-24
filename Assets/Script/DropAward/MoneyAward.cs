using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAward : DropAward
{
    [SerializeField] int Much;
    protected override void CustomGet()
    {
        PlayerSystemSO.GetPlayerInvoke().AddMoney(Much);
    }
}
