using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "MyCustomAsset/GameManager")]
public class GameManagerSO : ScriptableObject
{
    public static Func<GameObjectPool> GetPoolFunc;
    public static GameObjectPool GetPoolInvoke()
    {
        if (GetPoolFunc != null)
            return GetPoolFunc.Invoke();
        else
            return null;
    }
    public static UnityAction<float> ImpluseAction;
    public static void ImpluseInvoke(float power)
    {
        ImpluseAction?.Invoke(power);
    }
}
