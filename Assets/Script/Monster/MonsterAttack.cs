using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] int damage;
    private void Awake()
    {
        tag = "MonsterAttack";
        gameObject.SetActive(false);
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
            PlayerSystemSO.GetPlayerInvoke().Hurt(damage);
    }
}
