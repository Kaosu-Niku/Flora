using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAlert : MonoBehaviour
{
    CircleCollider2D Mycol;
    [HideInInspector] public Monster GetMonster;
    [HideInInspector] public float AlertDis;
    [HideInInspector] public GameObject GetHpSlider;
    bool CanCloseHate = true;
    void Start()
    {
        Mycol = GetComponent<CircleCollider2D>();
        Mycol.radius = AlertDis;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //GetMonster.AttackMode = true;
            CanCloseHate = false;
            GetHpSlider.SetActive(true);
            StartCoroutine(Hate());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CanCloseHate == true)
            {
                //GetMonster.AttackMode = false;
                GetHpSlider.SetActive(false);
            }
        }
    }
    IEnumerator Hate()
    {
        yield return new WaitForSeconds(5);
        CanCloseHate = true;
    }
}
