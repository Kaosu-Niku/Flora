using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : DefaultObject
{
    [SerializeField] float DamageMagn;
    int tureDamage;
    public int TureDamage { get => tureDamage; set { tureDamage = value; } }
    [SerializeField] float HitMagn;
    int hitDamage;
    public int HitDamage { get => hitDamage; set { hitDamage = value; } }
    [SerializeField] float StartTime;//* 攻擊判定開啟
    [SerializeField] float OverTime;//* 攻擊判定結束
    [SerializeField] float CloseTime;
    Collider2D Col;
    private void Awake()
    {
        Col = GetComponent<Collider2D>();
        gameObject.SetActive(false);
    }
    protected override IEnumerator Doing()
    {
        yield return 0;
        Debug.Log(PlayerSystemSO.GetPlayerInvoke().NowAtk); Debug.Log(DamageMagn); Debug.Log(PlayerSystemSO.GetPlayerInvoke().NowAtk * DamageMagn);
        TureDamage = ((int)(PlayerSystemSO.GetPlayerInvoke().NowAtk * DamageMagn));
        HitDamage = ((int)(PlayerSystemSO.GetPlayerInvoke().NowHit * HitMagn));
        Col.enabled = false;
        PlayerSystemSO.GetPlayerInvoke().AttackTrigger(this);//? (增傷效果)(光華刀刃效果)
        yield return new WaitForSeconds(StartTime);//? 開啟攻擊判定
        Col.enabled = true;
        yield return new WaitForSeconds(OverTime - StartTime);//? 關閉攻擊判定
        Col.enabled = false;
        yield return new WaitForSeconds(CloseTime - OverTime);//? 關閉此物件
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Hurt") && other.transform.root.transform.CompareTag("Monster"))
        {
            PlayerSystemSO.GetPlayerInvoke().AttackHurtEnemyTrigger(this); //? (彼岸花效果)
        }
    }
}
