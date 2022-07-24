using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObject : MonoBehaviour
{
    [SerializeField] GameObject obj;
    Collider2D GetCol;
    void Start()
    {
        GetCol = GetComponent<Collider2D>();
        obj.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//? 玩家進入範圍
            obj.SetActive(true);
    }

    protected void OnTriggerExit2D(Collider2D other)//? 玩家離開範圍
    {
        if (other.gameObject.CompareTag("Player"))
            obj.SetActive(false);
    }
}
