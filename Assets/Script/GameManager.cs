using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObjectPool pool;
    private GameObjectPool GetPool()
    {
        return pool;
    }
    private void OnEnable()
    {
        GameManagerSO.GetPoolFunc += GetPool;
    }
    private void OnDisable()
    {
        GameManagerSO.GetPoolFunc -= GetPool;
    }
}
