using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3 : Monster
{
    [SerializeField] float Speed;
    [SerializeField] MonsterAttack CollideAttack;

    protected override IEnumerator CustomDefaultAction()
    {
        yield return StartCoroutine(Move());
    }

    protected override IEnumerator CustomDie()
    {
        yield break;
    }

    protected override IEnumerator CustomHitRecover()
    {
        yield break;
    }

    protected override IEnumerator CustomHurt()
    {
        yield break;
    }

    protected override IEnumerator CustomIdleAction()
    {
        IsFight = true;
        yield break;
    }
    private IEnumerator Move()//? 持續往玩家移動,接近玩家後使出衝撞攻擊
    {
        if (PlayerSystemSO.GetPlayerInvoke().transform.position.x > transform.position.x)
            transform.rotation = Quaternion.identity;
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
        Anima.SetInteger("Move", 1);
        while (Mathf.Abs(transform.position.x - PlayerSystemSO.GetPlayerInvoke().transform.position.x) > 5)
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
            yield return 0;
        }
        yield return StartCoroutine(ColliderAttack());
    }
    private IEnumerator ColliderAttack()//? 衝撞攻擊
    {
        if (PlayerSystemSO.GetPlayerInvoke().transform.position.x > transform.position.x)
            transform.rotation = Quaternion.identity;
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
        CollideAttack.UseAttack();
        Rigid.AddRelativeForce(transform.right * 1000);
        Rigid.AddRelativeForce(Vector2.up * 500);
        Anima.SetTrigger("Attack");
        yield return new WaitForSeconds(2);
    }
}
