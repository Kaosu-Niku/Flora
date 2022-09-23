using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "MyCustomAsset/UiSystem")]
public class UiSystemSO : ScriptableObject
{
    public static UnityAction ChangePlayerHpAction;//? 玩家血量UI變更事件
    public static void ChangePlayerHpInvoke()
    {
        ChangePlayerHpAction?.Invoke();
    }
    public static UnityAction ChangePlayerMpAction;//? 玩家魔力UI變更事件
    public static void ChangePlayerMpInvoke()
    {
        ChangePlayerMpAction?.Invoke();
    }
    public static UnityAction<string, float> TalkPanelAction;//? 顯示對話框事件
    public static void TalkPanelInvoke(string what, float time)
    {
        TalkPanelAction?.Invoke(what, time);
    }
    public static UnityAction<bool> SaveImageAction;//? 存檔圖片事件
    public static void SaveImageInvoke(bool b)
    {
        SaveImageAction?.Invoke(b);
    }
}
