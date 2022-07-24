using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : DefaultObject
{
    [SerializeField] int damage;
    [SerializeField] float StartTime;//* 攻擊判定開啟
    [SerializeField] float OverTime;//* 攻擊判定結束
    [SerializeField] float CloseTime;//* 關閉時間
    Collider2D Col;
    private void Awake()
    {
        Col = GetComponent<Collider2D>();
        gameObject.SetActive(false);
    }
    protected override IEnumerator Doing()
    {
        Col.enabled = false;
        yield return new WaitForSeconds(StartTime);//? 開啟攻擊判定
        Col.enabled = true;
        yield return new WaitForSeconds(OverTime - StartTime);//? 關閉攻擊判定
        Col.enabled = false;
        yield return new WaitForSeconds(CloseTime - OverTime);//? 關閉此物件
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
            PlayerSystemSO.GetPlayerInvoke().Hurt(damage);
    }
}
