using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpAward : DropAward
{
    protected override void CustomGet()
    {
        PlayerSystemSO.GetPlayerInvoke().AddNowMp(10);
    }
}
