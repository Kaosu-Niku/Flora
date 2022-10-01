using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueMovePlatform : MonoBehaviour
{
    Vector3 FirstPos;
    [SerializeField] Vector3 MoveDis;
    Vector3 EndPos;
    [SerializeField] float Speed;
    private void Awake()
    {
        FirstPos = transform.position;
        transform.Translate(MoveDis.x, MoveDis.y, 0);
        EndPos = transform.position;
        transform.position = FirstPos;
    }
    private void OnEnable()
    {
        StartCoroutine(Go1());
    }

    IEnumerator Go1()
    {
        while (transform.position != EndPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPos, Speed * Time.deltaTime);
            yield return 0;
        }
        yield return StartCoroutine(Go2());
    }
    IEnumerator Go2()
    {
        while (transform.position != FirstPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, FirstPos, Speed * Time.deltaTime);
            yield return 0;
        }
        yield return StartCoroutine(Go1());
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            PlayerSystemSO.GetPlayerInvoke().transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            PlayerSystemSO.GetPlayerInvoke().transform.parent = null;
    }
}
