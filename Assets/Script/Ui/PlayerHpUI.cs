using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpUI : MonoBehaviour
{
    [SerializeField] AllEventSO GetAllEvent;
    [SerializeField] PlayerDataSO GetPlayerData;
    [SerializeField] Transform FirstPos;//* 初始圖片位置
    [SerializeField] List<GameObject> NullHpImage = new List<GameObject>();//* 空血量圖片
    [SerializeField] List<GameObject> HalfHpImage = new List<GameObject>();//* 半血量圖片
    [SerializeField] List<GameObject> MaxHpImage = new List<GameObject>();//* 滿血量圖片
    private void ChangePlayerHp()
    {
        StartCoroutine(ChangePlayerHpIEnum());
    }
    private IEnumerator ChangePlayerHpIEnum()
    {
        yield return 0;
        for (int x = 0; x < 20; x++)
        {
            if (NullHpImage[x].activeSelf == true)
                NullHpImage[x].SetActive(false);
            if (MaxHpImage[x].activeSelf == true)
                MaxHpImage[x].SetActive(false);
            if (HalfHpImage[x].activeSelf == true)
                HalfHpImage[x].SetActive(false);
        }
        for (int x = 0; x < PlayerSystemSO.GetPlayerInvoke().MaxHp / 2; x++)
            NullHpImage[x].SetActive(true);
        for (int x = 0; x < PlayerSystemSO.GetPlayerInvoke().NowHp / 2; x++)
            MaxHpImage[x].SetActive(true);
        if (PlayerSystemSO.GetPlayerInvoke().NowHp % 2 == 1)
        {
            NullHpImage[PlayerSystemSO.GetPlayerInvoke().NowHp / 2].SetActive(true);
            HalfHpImage[PlayerSystemSO.GetPlayerInvoke().NowHp / 2].SetActive(true);
        }

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
