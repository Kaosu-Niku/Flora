using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : Monster
{
    [SerializeField] float Speed;
    bool MoveDir = true;
    [SerializeField] GameObject Big;
    protected override IEnumerator OnActionIEnum()
    {
        if (Mathf.Abs(transform.position.x - PlayerDataSO.PlayerTrans.position.x) > 10)
            yield return StartCoroutine(MoveIEnum());
        else
            yield return StartCoroutine(ChangeIEnum());
    }
    private IEnumerator MoveIEnum()//? 左右隨機移動
    {
        int moveTime = Random.Range(1, 4);
        if (MoveDir == false)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.identity;
        Anima.SetInteger("Move", 1);
        for (float a = 0; a < moveTime; a += Time.deltaTime)
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
            yield return 0;
        }
        MoveDir = !MoveDir;
        yield return StartCoroutine(WaitIEnum());
    }
    private IEnumerator WaitIEnum()//? 停在原地並持續偵測玩家是否進入攻擊範圍
    {
        int moveTime = Random.Range(1, 4);
        Anima.SetInteger("Move", 0);
        for (float a = 0; a < moveTime; a += Time.deltaTime)
        {
            if (Mathf.Abs(transform.position.x - PlayerDataSO.PlayerTrans.position.x) <= 10)
                yield return StartCoroutine(ChangeIEnum());
            yield return 0;
        }
        yield return new WaitForSeconds(moveTime);
        OnAction();
        yield break;
    }
    private IEnumerator ChangeIEnum()//? 變成大吱吱
    {
        Anima.SetTrigger("Change");
        yield return new WaitForSeconds(1);
        Instantiate(Big, transform.position, transform.rotation);
        yield return 0;
        Destroy(this.gameObject);
    }
}
