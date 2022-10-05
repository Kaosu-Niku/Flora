using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropAward : PoolObject
{
    [SerializeField] float CloseTime;
    Vector3 MovePos;
    protected override IEnumerator Doing2()
    {
        yield return 0;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(4, 33) * 5);
        float a = Random.Range(3, 8);
        transform.Translate(a, 0, 0);
        MovePos = transform.position;
        transform.Translate(-a, 0, 0);
        while (Vector3.Distance(transform.position, MovePos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, MovePos, Time.deltaTime);
            yield return 0;
        }
        StartCoroutine(MoveIEnum());
        yield return new WaitForSeconds(CloseTime);
    }
    IEnumerator MoveIEnum()
    {
        bool dir = true;
        float y = Random.Range(0.2f, 0.5f);
        Vector3 high = transform.position + Vector3.up * y;
        Vector3 low = transform.position + Vector3.up * -y;
        while (true)
        {
            transform.Rotate(0, 0, -20 * Time.deltaTime);
            if (dir == true)
            {
                transform.position = Vector3.Lerp(transform.position, high, 2 * Time.deltaTime);
                if (Vector3.Distance(transform.position, high) < 0.1f)
                    dir = false;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, low, 2 * Time.deltaTime);
                if (Vector3.Distance(transform.position, low) < 0.1f)
                    dir = true;
            }
            yield return 0;
        }

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
