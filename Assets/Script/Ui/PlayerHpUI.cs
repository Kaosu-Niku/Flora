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
        for (int x = 0; x < PlayerDataSO.PlayerMaxHp / 2; x++)
        {
            if (MaxHpImage[x].activeInHierarchy == true)
                MaxHpImage[x].SetActive(false);
            if (HalfHpImage[x].activeInHierarchy == true)
                HalfHpImage[x].SetActive(false);
        }
        for (int x = 0; x < PlayerDataSO.PlayerNowHp / 2; x++)
            MaxHpImage[x].SetActive(true);
        if (PlayerDataSO.PlayerNowHp % 2 == 1)
        {
            MaxHpImage[((int)PlayerDataSO.PlayerNowHp / 2)].SetActive(false);
            HalfHpImage[((int)PlayerDataSO.PlayerNowHp / 2)].SetActive(true);
        }
    }
    private void OnEnable()
    {
        GameRunSO.ChangePlayerHpEvent += ChangePlayerHp;
    }
    private void OnDisable()
    {
        GameRunSO.ChangePlayerHpEvent -= ChangePlayerHp;
    }
    private void Start()
    {
        for (int x = 0; x < 20; x++)
        {
            NullHpImage[x].SetActive(false);
            MaxHpImage[x].SetActive(false);
            HalfHpImage[x].SetActive(false);
        }
        for (int x = 0; x < PlayerDataSO.PlayerMaxHp / 2; x++)
        {
            NullHpImage[x].SetActive(true);
            MaxHpImage[x].SetActive(true);
            if (PlayerDataSO.PlayerMaxHp % 2 == 1)
            {
                MaxHpImage[((int)PlayerDataSO.PlayerMaxHp / 2)].SetActive(false);
                HalfHpImage[((int)PlayerDataSO.PlayerMaxHp / 2)].SetActive(true);
            }
        }
    }
}
