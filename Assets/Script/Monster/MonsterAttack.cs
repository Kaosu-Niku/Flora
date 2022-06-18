using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float StartTime;//* 攻擊判定開啟
    [SerializeField] float OverTime;//* 攻擊判定結束
    [SerializeField] bool IsDestroy;//* 是否要刪除
    [SerializeField] float DesTime;//* 刪除時間
    [SerializeField] Vector2 MovePos;//* 初始位置設定
    Collider2D Col;
    protected void Start()
    {
        Col = GetComponent<Collider2D>();
        Col.enabled = false;
        transform.Translate(MovePos.x, MovePos.y, 0);
        UseAttack();
    }
    public void UseAttack()
    {
        StartCoroutine(UseAttackIEnum());
    }

    private IEnumerator UseAttackIEnum()
    {
        yield return new WaitForSeconds(StartTime);//? 開啟攻擊判定
        Col.enabled = true;
        yield return new WaitForSeconds(OverTime - StartTime);//? 關閉攻擊判定
        Col.enabled = false;
        yield return new WaitForSeconds(DesTime - OverTime);//? 刪除此物件
        if (IsDestroy == true)
            Destroy(this.gameObject);
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
            GameRunSO.PlayerHurtTrigger(damage);
    }
}
