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
    [SerializeField] Transform TargetCameraLookTrans;
    CameraController GetCameraControl;

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
            GetPlayerRigid.Sleep();
            GetPlayerRigid.AddForce(transform.up * Power);
        }
        if (GetAnimator != null)
        {
            GetAnimator.SetTrigger("Jump");
        }
        if(GetCameraControl != null)
        {
            GetCameraControl.AddTarget(GetPlayer.transform, 1, 20, 1);
            GetCameraControl.AddTarget(TargetCameraLookTrans, 1, 25, 1);
        }
        yield return new WaitForSeconds(0.25f);
        CanUse = true;
    }
    private void Start()
    {
        GetPlayer = GameObject.FindGameObjectWithTag("Player");
        GetPlayerRigid = GetPlayer.GetComponent<Rigidbody2D>();
        GetCameraControl = GameObject.Find("CameraControl").GetComponent<CameraController>();
    }
}
