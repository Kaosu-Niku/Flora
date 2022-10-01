using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TalkPoint : MonoBehaviour
{
    [SerializeField] List<string> TalkString = new List<string>();//* 對話內容
    [SerializeField] List<float> TalkTime = new List<float>();//* 對話秒數
    int TalkNum;//* 對話次數
    MyInput GetInput;
    private void Awake()
    {
        GetInput = new MyInput();
    }
    private void OnEnable()
    {
        GetInput.Enable();
    }
    private void OnDisable()
    {
        GetInput.Disable();
        GetInput.Player.Action.started -= OnTalk;
    }

    private void OnTalk(InputAction.CallbackContext context)
    {
        if (TalkNum < TalkString.Count)
        {
            UiSystemSO.TalkPanelInvoke(null, 1);//? 開始播放第二句以上的對話時，清除前一個對話的進度
            UiSystemSO.TalkPanelInvoke(TalkString[TalkNum], TalkTime[TalkNum]);//? 在指定秒數內逐漸一字一字顯示對話內容
            TalkNum++;
        }
        else
        {
            UiSystemSO.TalkPanelInvoke(null, 1);
            TalkNum = 0;
            PlayerSystemSO.GetPlayerInvoke().SetCanControl(true);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//? 玩家進入範圍
        {
            PlayerSystemSO.GetPlayerInvoke().SetCanControl(false);
            UiSystemSO.TalkPanelInvoke(null, 1);//? 開始播放第二句以上的對話時，清除前一個對話的進度
            UiSystemSO.TalkPanelInvoke(TalkString[TalkNum], TalkTime[TalkNum]);//? 在指定秒數內逐漸一字一字顯示對話內容
            TalkNum++;
            GetInput.Player.Action.started += OnTalk;
        }
    }
}
