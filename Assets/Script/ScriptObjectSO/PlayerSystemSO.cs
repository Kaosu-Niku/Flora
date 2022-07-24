using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "MyCustomAsset/PlayerSystem")]
public class PlayerSystemSO : ScriptableObject
{
    public static Func<PlayerSystem> GetPlayerFunc;
    public static PlayerSystem GetPlayerInvoke()
    {
        return GetPlayerFunc.Invoke();
    }
}
