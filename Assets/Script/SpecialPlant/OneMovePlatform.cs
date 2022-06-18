using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneMovePlatform : MonoBehaviour
{
    [SerializeField] Vector2 MoveDis;
    [SerializeField] float Speed;
    bool OneUse = false;
    Vector3 FirstPos;
    Vector3 EndPos;
    private void OnEnable()
    {
        if (OneUse == true)
        {
            OneUse = true;
            FirstPos = transform.position;
            transform.Translate(MoveDis.x, MoveDis.y, 0);
            EndPos = transform.position;
            transform.position = FirstPos;
            StartCoroutine(Go());
        }
        OneUse = true;
    }
    IEnumerator Go()
    {
        while (transform.position != EndPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPos, Speed * Time.deltaTime);
            yield return 0;
        }
        yield break;
    }

}
