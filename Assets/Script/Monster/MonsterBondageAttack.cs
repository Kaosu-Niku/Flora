using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBondageAttack : MonoBehaviour
{
    bool b;
    [SerializeField] Transform FollowTrans;
    Coroutine C;
    private void Awake()
    {
        tag = "MonsterAttack";
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        b = false;
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (b == false)
        {
            if (other.transform.CompareTag("Player"))
            {
                b = true;
                PlayerSystemSO.GetPlayerInvoke().Bondage(FollowTrans);
            }
        }
    }
}
