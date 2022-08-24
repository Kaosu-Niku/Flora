using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMpUI : MonoBehaviour
{
    [SerializeField] Image PlayerMpImage;
    private void ChangePlayerMp()
    {
        PlayerMpImage.fillAmount = ((float)(PlayerSystemSO.GetPlayerInvoke().NowMp)) / ((float)(PlayerSystemSO.GetPlayerInvoke().MaxMp));
    }
    private void OnEnable()
    {
        UiSystemSO.ChangePlayerMpAction += ChangePlayerMp;
    }
    private void OnDisable()
    {
        UiSystemSO.ChangePlayerMpAction -= ChangePlayerMp;
    }
}
