using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BigJumpPlant : SpecialPlantSystem
{
    bool CanUse = true;
    [SerializeField] int Power;
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
    new void OnTriggerExit2D(Collider2D other)
    {

    }
    private IEnumerator Doing()
    {
        CanUse = false;
        if (GetPlayerRigid != null)
        {
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
