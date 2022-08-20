using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float DamageMagn;
    int tureDamage;
    public int TureDamage { get => tureDamage; set { tureDamage = value; } }
    [SerializeField] float HitMagn;
    int hitDamage;
    public int HitDamage { get => hitDamage; set { hitDamage = value; } }
    private void Awake()
    {
        tag = "PlayerAttack";
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Debug.Log(PlayerSystemSO.GetPlayerInvoke().NowAtk * DamageMagn); Debug.Log(PlayerSystemSO.GetPlayerInvoke().NowHit * HitMagn);
        TureDamage = ((int)(PlayerSystemSO.GetPlayerInvoke().NowAtk * DamageMagn));
        HitDamage = ((int)(PlayerSystemSO.GetPlayerInvoke().NowHit * HitMagn));
        PlayerSystemSO.GetPlayerInvoke().AttackTrigger(this);//? (增傷效果)(光華刀刃效果)
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Hurt") && other.transform.parent.CompareTag("Monster"))
        {
            PlayerSystemSO.GetPlayerInvoke().AttackHurtEnemyTrigger(this); //? (彼岸花效果)
        }
    }
}
