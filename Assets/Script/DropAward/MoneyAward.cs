using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAward : DropAward
{
    [SerializeField] int Much;
    protected override IEnumerator GetIEnum()
    {
        PlayerDataSO.PlayerMoney += Much;
        PlayerDataSO.AddMoneyTrigger(Much);
        Destroy(this.gameObject);
        yield break;
    }
}
