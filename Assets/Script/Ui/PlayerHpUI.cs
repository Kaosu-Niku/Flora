using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    [SerializeField] AllEventSO GetAllEvent;
    [SerializeField] PlayerDataSO GetPlayerData;
    [SerializeField] Image HpImage;
    [SerializeField] GameObject Hp30Image;
    [SerializeField] GameObject Hp20Image;
    private void ChangePlayerHp()
    {
        StartCoroutine(ChangePlayerHpIEnum());
    }
    private IEnumerator ChangePlayerHpIEnum()
    {
        yield return 0;
        float a = ((float)PlayerSystemSO.GetPlayerInvoke().NowHp) / ((float)PlayerSystemSO.GetPlayerInvoke().MaxHp);
        HpImage.fillAmount = a;
        if (a < 0.7f)
        {
            if (Hp30Image.activeInHierarchy == true)
                Hp30Image.SetActive(false);
            if (Hp20Image.activeInHierarchy == true)
                Hp20Image.SetActive(false);
        }
        else if (a < 0.9f)
        {
            if (a < 0.8f)
            {
                if (Hp30Image.activeInHierarchy == false)
                    Hp30Image.SetActive(true);
                if (Hp20Image.activeInHierarchy == true)
                    Hp20Image.SetActive(false);
            }
            else
            {
                if (Hp30Image.activeInHierarchy == true)
                    Hp30Image.SetActive(false);
                if (Hp20Image.activeInHierarchy == false)
                    Hp20Image.SetActive(true);
            }
        }
        else
        {
            if (Hp30Image.activeInHierarchy == false)
                Hp30Image.SetActive(true);
            if (Hp20Image.activeInHierarchy == false)
                Hp20Image.SetActive(true);
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
    void Start()
    {
        HpImage.fillAmount = 0;
        Hp30Image.SetActive(false);
        Hp20Image.SetActive(false);
    }
}
