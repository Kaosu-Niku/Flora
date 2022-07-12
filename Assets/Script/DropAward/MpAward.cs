using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpAward : DropAward
{
    protected override IEnumerator GetIEnum()
    {
        PlayerSystemSO.GetPlayerInvoke().AddNowMp(10);
        Destroy(this.gameObject);
        yield break;
    }
}
