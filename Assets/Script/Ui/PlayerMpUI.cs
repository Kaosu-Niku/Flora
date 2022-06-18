using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMpUI : MonoBehaviour
{
    [SerializeField] Image PlayerMpImage;
    private void ChangePlayerMp()
    {
        PlayerMpImage.fillAmount = PlayerDataSO.PlayerNowMp / PlayerDataSO.PlayerMaxMp;
    }
    private void OnEnable()
    {
        GameRunSO.ChangePlayerMpEvent += ChangePlayerMp;
    }
    private void OnDisable()
    {
        GameRunSO.ChangePlayerMpEvent -= ChangePlayerMp;
    }
}
