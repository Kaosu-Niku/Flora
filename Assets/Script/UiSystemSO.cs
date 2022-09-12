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
        if (ChangePlayerHpAction != null)
            ChangePlayerHpAction.Invoke();
    }
    public static UnityAction ChangePlayerMpAction;//? 玩家魔力UI變更事件
    public static void ChangePlayerMpInvoke()
    {
        if (ChangePlayerMpAction != null)
            ChangePlayerMpAction.Invoke();
    }
    public static UnityAction<Text, string, float> TalkPanelAction;//? 顯示對話框事件
    public static void TalkPanelInvoke(Text text, string what, float time)
    {
        if (TalkPanelAction != null)
            TalkPanelAction.Invoke(text, what, time);
    }
}
