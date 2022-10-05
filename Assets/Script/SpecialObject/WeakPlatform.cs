using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPlatform : MonoBehaviour
{
    [SerializeField] float WeakTime;
    [SerializeField] GameObject g;
    bool CanGo = true;
    Collider2D MyCol;
    private void Start()
    {
        MyCol = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CanGo == true)
                StartCoroutine(Go());
        }
    }
    IEnumerator Go()
    {
        CanGo = false;
        yield return new WaitForSeconds(WeakTime);
        MyCol.enabled = false;
        g.SetActive(false);
        yield return new WaitForSeconds(3);
        MyCol.enabled = true;
        g.SetActive(true);
        CanGo = true;
    }
}
