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
    [HideInInspector] public int WhichWindowsMode;
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

    public void OnClick_ChangeWindowMode(bool b)//? 滑鼠點擊選項箭頭觸發的更改視窗模式和UI
    {
        FullModeOption.SetActive(false);
        BorderModeOption.SetActive(false);
        WindowsModeOption.SetActive(false);
        if (b == false)
        {
            WhichWindowsMode--;
            if (WhichWindowsMode < 0)
                WhichWindowsMode = 2;
        }
        else
        {
            WhichWindowsMode++;
            if (WhichWindowsMode > 2)
                WhichWindowsMode = 0;
        }
        switch (WhichWindowsMode)
        {
            case 0: FullModeOption.SetActive(true); Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case 1: BorderModeOption.SetActive(true); Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case 2: WindowsModeOption.SetActive(true); Screen.fullScreenMode = FullScreenMode.MaximizedWindow; break;
        }
    }
    public void OnClick_ChangeWindowMode(int which)//? 鍵盤AD切換更改視窗模式和UI
    {
        FullModeOption.SetActive(false);
        BorderModeOption.SetActive(false);
        WindowsModeOption.SetActive(false);
        switch (which)
        {
            case 0: FullModeOption.SetActive(true); Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case 1: BorderModeOption.SetActive(true); Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case 2: WindowsModeOption.SetActive(true); Screen.fullScreenMode = FullScreenMode.MaximizedWindow; break;
        }
    }
}
