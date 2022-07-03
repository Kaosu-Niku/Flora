using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject TargetCamera;
    [SerializeField] CinemachineTargetGroup CTG;
    public void AddTarget(Transform t, float w, float r, float time)
    {
        StartCoroutine(AddTargetIEnum(t, w, r, time));
    }
    IEnumerator AddTargetIEnum(Transform t, float w, float r, float time)
    {
        CTG.AddMember(t, w, r);
        TargetCamera.SetActive(true);
        yield return new WaitForSeconds(time);
        CTG.RemoveMember(t);
        TargetCamera.SetActive(false);
    }
    private void Start()
    {
        TargetCamera.SetActive(false);
    }
}
