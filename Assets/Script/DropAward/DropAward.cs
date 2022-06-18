using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAward : MonoBehaviour
{
    IEnumerator Go()
    {
        Collider2D c = GetComponent<Collider2D>();
        c.enabled = false;
        yield return new WaitForSeconds(2);
        c.enabled = true;
        yield break;
    }
    protected void Get()
    {
        StartCoroutine(GetIEnum());
    }
    protected virtual IEnumerator GetIEnum()
    {
        yield break;
    }
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(16, 21) * 5);
        transform.Translate(1, 0, 0);
        GetComponent<Rigidbody2D>().AddRelativeForce(transform.right * Random.Range(16, 21) * 50);
        StartCoroutine(Go());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Suck"))//? 玩家進入範圍
        {
            Get();
        }
    }
}
