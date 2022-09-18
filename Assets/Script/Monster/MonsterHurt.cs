using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHurt : MonoBehaviour
{
    Monster GetMonster;
    private void Awake()
    {
        GetMonster = transform.parent.GetComponent<Monster>();
        tag = "Hurt";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerAttack"))//? 被玩家攻擊
        {
            PlayerAttack a = other.gameObject.GetComponent<PlayerAttack>();
            if (a)
            {
                Debug.Log(a.TureDamage); Debug.Log(a.HitDamage);
                GetMonster.Hurt(a.TureDamage, a.HitDamage);
            }
        }
    }
}
