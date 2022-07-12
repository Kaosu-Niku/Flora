using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigJumpPlant : SpecialPlantSystem
{
    bool CanUse = true;
    [SerializeField] int Power;
    [SerializeField] Animator GetAnimator;
    GameObject GetPlayer;
    Rigidbody2D GetPlayerRigid;

    new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CanUse == true)
                StartCoroutine(Doing());
        }
    }
    private IEnumerator Doing()
    {
        CanUse = false;
        if (GetAnimator != null)
        {
            GetAnimator.SetTrigger("Jump");
        }
        yield return new WaitForSeconds(0.25f);
        if (GetPlayerRigid != null)
        {
            GetPlayerRigid.Sleep();
            GetPlayerRigid.AddForce(transform.up * Power);
        }
        yield return new WaitForSeconds(0.25f);
        CanUse = true;
    }
    private void Start()
    {
        GetPlayer = GameObject.FindGameObjectWithTag("Player");
        GetPlayerRigid = GetPlayer.GetComponent<Rigidbody2D>();
    }
}
