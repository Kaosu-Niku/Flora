using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrap : MonoBehaviour
{
    [SerializeField] Rigidbody2D Rigid;
    [SerializeField] Collider2D TriggerCol;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigid.gravityScale = 10;
            Destroy(TriggerCol);
        }
    }
}
