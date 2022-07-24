using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMpUI : MonoBehaviour
{
    [SerializeField] Image PlayerMpImage;
    private void ChangePlayerMp()
    {
        PlayerMpImage.fillAmount = PlayerSystemSO.GetPlayerInvoke().NowMp / PlayerDataSO.MaxMp;
    }
    private void OnEnable()
    {
        UiSystem.ChangePlayerMpAction += ChangePlayerMp;
    }
    private void OnDisable()
    {
        UiSystem.ChangePlayerMpAction -= ChangePlayerMp;
    }
}
