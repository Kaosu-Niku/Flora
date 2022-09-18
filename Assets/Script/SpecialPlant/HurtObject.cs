using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtObject : MonoBehaviour
{
    PlayerSystem GetPlayer;
    bool CanHurt = true;
    bool s;
    private void Start()
    {
        GetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSystem>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CanHurt = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CanHurt == true)
            {
                StartCoroutine(UseIEnum());
            }
        }
    }
    IEnumerator UseIEnum()
    {
        GetPlayer.Hurt(1);
        CanHurt = false;
        yield return new WaitForSeconds(2);
        CanHurt = true;
        transform.Translate(0, -0.0001f, 0);
        yield return new WaitForFixedUpdate();
        yield return 0;
        transform.Translate(0, 0.0001f, 0);
    }
}