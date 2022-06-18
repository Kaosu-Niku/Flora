using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpAward : DropAward
{
    protected override IEnumerator GetIEnum()
    {
        PlayerDataSO.PlayerNowMp += 10;
        PlayerDataSO.AddMpTrigger(10);
        Destroy(this.gameObject);
        yield break;
    }
}
