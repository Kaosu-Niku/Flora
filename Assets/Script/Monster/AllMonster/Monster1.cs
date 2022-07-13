using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : Monster
{
    [SerializeField] float Speed;
    bool MoveDir = true;
    protected override IEnumerator CustomIdle()
    {
        //? 左右隨機移動
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
        //? 停在原地
        int waitTime = Random.Range(1, 4);
        Anima.SetInteger("Move", 0);
        yield return new WaitForSeconds(waitTime);
        yield break;
    }
    [SerializeField] GameObject Big;
    protected override IEnumerator CustomDefault()
    {
        yield break;
    }

    protected override IEnumerator CustomHurt()
    {
        yield break;
    }

    protected override IEnumerator CustomHitRecover()
    {
        yield break;
    }

    protected override IEnumerator CustomDie()
    {
        //? 變成大吱吱
        Anima.SetTrigger("Change");
        yield return new WaitForSeconds(1);
        Instantiate(Big, transform.position, transform.rotation);
        yield return 0;
        Destroy(this.gameObject);
    }
}
