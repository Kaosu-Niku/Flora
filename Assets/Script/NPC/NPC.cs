using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    NpcPop MyPop;//* 提示泡泡
    [SerializeField] List<string> TalkString = new List<string>();//* 對話內容
    [SerializeField] List<float> TalkTime = new List<float>();//* 對話秒數
    int TalkNum;//* 對話次數
    MyInput GetInput;
    Transform PlayerTrans;
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
    private void Start()
    {
        MyPop = transform.GetComponentInChildren<NpcPop>();
        PlayerTrans = PlayerSystemSO.GetPlayerInvoke().transform;
    }

    private void OnTalk(InputAction.CallbackContext context)
    {
        if (TalkNum < TalkString.Count)
        {
            PlayerSystemSO.GetPlayerInvoke().SetCanControl(false);
            UiSystemSO.TalkPanelInvoke(null, 1);//? 開始播放第二句以上的對話時，清除前一個對話的進度
            UiSystemSO.TalkPanelInvoke(TalkString[TalkNum], TalkTime[TalkNum]);//? 在指定秒數內逐漸一字一字顯示對話內容
            TalkNum++;
            //skeletonAnimationSystem.GetSkeletonAnimation.AnimationState.SetAnimation(0, "Talk", true);
        }
        else
        {
            PlayerSystemSO.GetPlayerInvoke().SetCanControl(true);
            UiSystemSO.TalkPanelInvoke(null, 1);
            TalkNum = 0;
            //skeletonAnimationSystem.GetSkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//? 玩家進入範圍
        {
            GetInput.Player.Action.started += OnTalk;
            MyPop.CallDialog();
            C = StartCoroutine(FollowPlayerIEnum());
        }
    }

    private void OnTriggerExit2D(Collider2D other)//? 玩家離開範圍
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetInput.Player.Action.started -= OnTalk;
            TalkNum = 0;
            MyPop.CallClear();
            if (C != null)
                StopCoroutine(C);
        }
    }
    Coroutine C;
    IEnumerator FollowPlayerIEnum()
    {
        while (true)
        {
            if (PlayerTrans.position.x > transform.position.x)
            {
                if (transform.eulerAngles.y > 90)
                    transform.Rotate(0, 180, 0);
            }
            else
            {
                if (transform.eulerAngles.y < 90)
                    transform.Rotate(0, 180, 0);
            }
            yield return 0;
        }
    }
}
