using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TalkUI : MonoBehaviour
{
    [SerializeField] GameObject TalkPanel;//* 對話介面
    [SerializeField] Text TalkText;//* 對話文字
    Sequence Seq;
    private void Awake()
    {
        Seq = DOTween.Sequence();
    }
    private void OnEnable()
    {
        UiSystemSO.TalkPanelAction += ToTalk;
    }
    private void OnDisable()
    {
        UiSystemSO.TalkPanelAction -= ToTalk;
    }
    private void Start()
    {
        TalkPanel.SetActive(false);
    }
    private void ToTalk(Text text, string what, float time)
    {
        if (TalkPanel != null)
        {
            TalkPanel.SetActive(true);
            if (what != null)
            {
                Seq.Append(text.DOText("", 1));//? 開始播放第二句以上的對話時，清除前一個對話的進度
                Seq.Append(text.DOText(what, time));//? 在指定秒數內逐漸一字一字顯示對話內容
            }
            else
            {
                TalkPanel.SetActive(false);
                Seq.Append(text.DOText("", 1));
            }
        }
    }
}
