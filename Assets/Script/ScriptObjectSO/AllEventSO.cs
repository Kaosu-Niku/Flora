using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "MyCustomAsset/AllEvent")]
public class AllEventSO : ScriptableObject
{
    [SerializeField] AudioMixer GetAudioMixer;
    public static UnityAction<string> LoadSceneEvent;//? 場景轉換事件(無讀取)
    public static void LoadSceneTrigger(string sceneName)
    {
        if (LoadSceneEvent != null)
            LoadSceneEvent.Invoke(sceneName);
    }
    public static UnityAction<string> LoadSceneAsyncEvent;//? 場景轉換事件(有讀取)
    public static void LoadSceneAsyncTrigger(string sceneName)
    {
        if (LoadSceneAsyncEvent != null)
            LoadSceneAsyncEvent.Invoke(sceneName);
    }
    public static UnityAction SaveEvent;//? 存檔事件
    public static void SaveTrigger()
    {
        if (SaveEvent != null)
            SaveEvent.Invoke();
    }
    public static UnityAction LoadEvent;//? 讀檔事件
    public static void LoadTrigger()
    {
        if (LoadEvent != null)
            LoadEvent.Invoke();
    }
    
    public void ChangeWindowMode(Dropdown getDropDown)//? 更改視窗模式
    {

        switch (getDropDown.value)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case 1: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case 2: Screen.fullScreenMode = FullScreenMode.MaximizedWindow; break;
            case 3: Screen.fullScreenMode = FullScreenMode.Windowed; break;
        }
    }

//? 更改音量
    public void SetMasterVolume(Slider getSlider)
    {
        GetAudioMixer.SetFloat("MasterVolume", getSlider.value);
    }
    public void SetBGMVolume(Slider getSlider)
    {
        GetAudioMixer.SetFloat("BGMVolume", getSlider.value);
    }
    public void SetSEVolume(Slider getSlider)
    {
        GetAudioMixer.SetFloat("SEVolume", getSlider.value);
    }
}
