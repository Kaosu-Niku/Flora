using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAward : DropAward
{
    [SerializeField] int Much;
    protected override IEnumerator GetIEnum()
    {
        PlayerSystemSO.GetPlayerInvoke().AddMoney(Much);
        Destroy(this.gameObject);
        yield break;
    }
}
