using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropAward : PoolObject
{
    [SerializeField] float CloseTime;
    Vector3 MovePos;
    protected override IEnumerator Doing()
    {
        yield return 0;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(6, 31) * 5);
        transform.Translate(1.5f, 0, 0);
        GetComponent<Rigidbody2D>().AddForce(transform.right * Random.Range(30, 50));
        yield return new WaitForSeconds(CloseTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Suck"))//? 玩家進入範圍
        {
            Get();
        }
    }
    void Get()
    {
        CustomGet();
        gameObject.SetActive(false);
    }
    protected abstract void CustomGet();

}
