using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaundersonAttack5 : IPoolObject
{
    bool IsCol;
    Collider2D Col;
    protected override IEnumerator Doing2()
    {
        IsCol = false;
        Col.enabled = true;
        for (float x = 0; x < 5; x += Time.deltaTime)
        {
            if (IsCol == true)
            {
                break;
            }
            transform.Translate(40 * Time.deltaTime, 0, 0);
            yield return 0;
        }
        yield return new WaitForSeconds(5);
        yield break;
    }
    private void Awake()
    {
        Col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.gameObject.CompareTag("Floor") == true)
        {
            IsCol = true;
            Col.enabled = false;
        }
    }
}
