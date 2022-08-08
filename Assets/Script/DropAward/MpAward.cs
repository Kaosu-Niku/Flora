using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpAward : DropAward
{
    [SerializeField] int Much;
    protected override void CustomGet()
    {
        PlayerSystemSO.GetPlayerInvoke().AddNowMp(Much);
    }
}
