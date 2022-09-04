using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstUI : MonoBehaviour
{
    [SerializeField] AllEventSO eventSO;
    [SerializeField] Slider MasterVolume;
    [SerializeField] Slider BgmVolume;
    [SerializeField] Slider SeVolume;
    [SerializeField] GameObject FullModeOption;
    [SerializeField] GameObject BorderModeOption;
    [SerializeField] GameObject WindowsModeOption;
    void Awake()
    {

        MasterVolume.value = GameDataSO.MasterVolume;
        BgmVolume.value = GameDataSO.BgmVolume;
        SeVolume.value = GameDataSO.SeVolume;
        FullModeOption.SetActive(false);
        BorderModeOption.SetActive(false);
        WindowsModeOption.SetActive(false);
        eventSO.ChangeWindowMode(GameDataSO.WindowsMode);
        switch (GameDataSO.WindowsMode)
        {
            case 0: FullModeOption.SetActive(true); break;
            case 1: BorderModeOption.SetActive(true); break;
            case 2: WindowsModeOption.SetActive(true); break;
        }
    }
}
