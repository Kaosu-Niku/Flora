using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;

public class NPC : MonoBehaviour
{
    NpcPop MyPop;//* 提示泡泡
    IPoolObject NpcTalk;//* Npc對話面板
    Text TalkText;
    [SerializeField] Vector3 TalkMove;//* 對話面板位置微調 
    [SerializeField] List<string> TalkString = new List<string>();//* 對話內容
    [SerializeField] List<float> TalkTime = new List<float>();//* 對話秒數
    int TalkNum;//* 對話次數
    bool CanTalk = false;
    MyInput GetInput;
    Sequence Seq;
    private void Awake()
    {
        GetInput = new MyInput();
        Seq = DOTween.Sequence();
    }
    private void OnEnable()
    {
        GetInput.Enable();
        GetInput.Player.Action.started += OnTalk;
    }
    private void OnDisable()
    {
        GetInput.Disable();
        GetInput.Player.Action.started -= OnTalk;
    }
    private void Start()
    {
        MyPop = transform.GetComponentInChildren<NpcPop>();
        NpcTalk = GameManagerSO.GetPoolInvoke().GetObject("NpcTalk", transform.position + TalkMove, transform.rotation);
        NpcTalk.gameObject.SetActive(false);
        TalkText = NpcTalk.transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    private void OnTalk(InputAction.CallbackContext context)
    {
        if (CanTalk == true)
        {
            if (TalkNum < TalkString.Count)
            {
                NpcTalk.gameObject.SetActive(true);
                Seq.Append(TalkText.DOText("", 1));//? 開始播放第二句以上的對話時，清除前一個對話的進度
                Seq.Append(TalkText.DOText(TalkString[TalkNum], TalkTime[TalkNum]));//? 在指定秒數內逐漸一字一字顯示對話內容
                TalkNum++;
                //skeletonAnimationSystem.GetSkeletonAnimation.AnimationState.SetAnimation(0, "Talk", true);
            }
            else
            {
                NpcTalk.gameObject.SetActive(false);
                Seq.Append(TalkText.DOText("", 1));
                TalkNum = 0;
                //skeletonAnimationSystem.GetSkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//? 玩家進入範圍
        {
            CanTalk = true;
            MyPop.CallDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D other)//? 玩家離開範圍
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CanTalk = false;
            TalkNum = 0;
            MyPop.CallClear();
        }
    }

}
