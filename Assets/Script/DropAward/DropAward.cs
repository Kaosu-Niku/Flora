using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropAward : PoolObject
{
    [SerializeField] float CloseTime;
    protected override IEnumerator Doing()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(16, 21) * 5);
        transform.Translate(1, 0, 0);
        GetComponent<Rigidbody2D>().AddRelativeForce(transform.right * Random.Range(16, 21) * 50);
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
