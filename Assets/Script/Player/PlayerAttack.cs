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
    [SerializeField] float StartTime;//* 攻擊判定開啟
    [SerializeField] float OverTime;//* 攻擊判定結束
    [SerializeField] float DestrotTime;
    [SerializeField] Vector2 MovePos;//* 初始位置設定
    Collider2D Col;
    protected void Awake()
    {
        Col = GetComponent<Collider2D>();
    }
    protected void Start()
    {
        TureDamage = ((int)(PlayerDataSO.PlayerNowAtk * DamageMagn));
        HitDamage = ((int)(PlayerDataSO.PlayerNowHit * HitMagn));
        Col.enabled = false;
        transform.Translate(MovePos.x, MovePos.y, 0);
        PlayerSystem.AttackTrigger(this);//? (增傷效果)(光華刀刃效果)
        UseAttack();
    }
    private void UseAttack()
    {
        StartCoroutine(UseAttackIEnum()); Debug.Log(TureDamage);
    }

    private IEnumerator UseAttackIEnum()
    {
        yield return new WaitForSeconds(StartTime);//? 開啟攻擊判定
        Col.enabled = true;
        yield return new WaitForSeconds(OverTime - StartTime);//? 關閉攻擊判定
        Col.enabled = false;
        yield return new WaitForSeconds(DestrotTime - OverTime);//? 刪除此物件
        Destroy(this.gameObject);
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Hurt") && other.transform.root.transform.CompareTag("Monster"))
        {
            PlayerSystem.AttackHurtEnemyTrigger(this); //? (彼岸花效果)
        }
    }
}
