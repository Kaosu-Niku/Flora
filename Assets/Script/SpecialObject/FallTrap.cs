using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrap : MonoBehaviour
{
    [SerializeField] Rigidbody2D Rigid;
    [SerializeField] Collider2D CollisionCol;
    [SerializeField] Collider2D TriggerCol;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSystemSO.GetPlayerInvoke().ForDamage(1);
            PlayerSystemSO.GetPlayerInvoke().HitFly(1000);
            CollisionCol.enabled = false;
            StartCoroutine(Late());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigid.gravityScale = 10;
            TriggerCol.enabled = false;
        }
    }
    IEnumerator Late()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
