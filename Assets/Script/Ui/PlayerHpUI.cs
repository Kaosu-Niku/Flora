using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    [SerializeField] AllEventSO GetAllEvent;
    [SerializeField] PlayerDataSO GetPlayerData;
    [SerializeField] Image HpImage;
    private void ChangePlayerHp()
    {
        StartCoroutine(ChangePlayerHpIEnum());
    }
    private IEnumerator ChangePlayerHpIEnum()
    {
        yield return 0;
        HpImage.fillAmount = ((float)PlayerSystemSO.GetPlayerInvoke().NowHp) / ((float)PlayerSystemSO.GetPlayerInvoke().MaxHp);
    }
    private void OnEnable()
    {
        UiSystemSO.ChangePlayerHpAction += ChangePlayerHp;
    }
    private void OnDisable()
    {
        UiSystemSO.ChangePlayerHpAction -= ChangePlayerHp;
    }
}
