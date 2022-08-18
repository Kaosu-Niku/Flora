using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBoneFollow : MonoBehaviour
{
    //? 請將該腳本和碰撞體都放在原骨頭上，BneTrans指定Override的骨頭
    Vector3 FirstPos;
    [SerializeField] Transform BoneTrans;
    Transform PlayerTrans;
    Coroutine C;
    void Awake()
    {
        FirstPos = transform.position;
    }
    void Start()
    {
        PlayerTrans = PlayerSystemSO.GetPlayerInvoke().transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (C != null)
                StopCoroutine(C);
            C = StartCoroutine(FollowPlayerIEnum());
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (C != null)
                StopCoroutine(C);
            BoneTrans.position = FirstPos;
        }
    }
    IEnumerator FollowPlayerIEnum()
    {
        while (true)
        {
            BoneTrans.position = PlayerTrans.position;
            yield return 0;
        }
    }
}
