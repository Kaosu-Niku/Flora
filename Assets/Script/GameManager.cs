using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObjectPoolSO pool;
    private void Awake()
    {
        pool.FirstSet();
    }
}
