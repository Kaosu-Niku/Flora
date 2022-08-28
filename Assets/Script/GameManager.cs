using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObjectPool pool;
    private GameObjectPool GetPool()
    {
        return pool;
    }
    [SerializeField] CinemachineImpulseSource impluseSource;
    private void ImpluseTrigger(float power)
    {
        impluseSource.GenerateImpulse(power);
    }
    private void OnEnable()
    {
        GameManagerSO.GetPoolFunc += GetPool;
        GameManagerSO.ImpluseAction += ImpluseTrigger;
    }
    private void OnDisable()
    {
        GameManagerSO.GetPoolFunc -= GetPool;
        GameManagerSO.ImpluseAction -= ImpluseTrigger;
    }
}
