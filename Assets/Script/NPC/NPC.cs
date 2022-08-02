using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine.Unity;

public class NPC : MonoBehaviour
{
    Transform PlayerTransform;
    PoolObject NpcHintButton;//* 提示按鈕
    [SerializeField] Vector3 ButtonMove;//* 按鈕位置微調 
    [SerializeField] List<string> TalkString = new List<string>();//* 對話內容
    [SerializeField] List<float> TalkTime = new List<float>();//* 對話秒數
    int TalkNum;//* 對話次數
    bool CanTalk = false;
    MyInput GetInput;
    private void Awake()
    {
        GetInput = new MyInput();
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

    private void OnTalk(InputAction.CallbackContext context)
    {
        if (CanTalk == true)
        {
            if (TalkNum >= TalkString.Count)
            {
                UiSystem.TalkPanelInvoke(null, 1);
                TalkNum = 0;
                //skeletonAnimationSystem.GetSkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
            }
            else
            {
                UiSystem.TalkPanelInvoke(TalkString[TalkNum], TalkTime[TalkNum]);
                TalkNum++;
                //skeletonAnimationSystem.GetSkeletonAnimation.AnimationState.SetAnimation(0, "Talk", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//? 玩家進入範圍
        {
            CanTalk = true;
            NpcHintButton = GameObjectPoolSO.GetObject("NpcHintButton", transform.position + ButtonMove, transform.rotation);
            NpcHintButton.gameObject.SetActive(true);
            PlayerTransform = other.transform.root.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)//? 玩家離開範圍
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CanTalk = false;
            TalkNum = 0;
            if (NpcHintButton != null)
                NpcHintButton.gameObject.SetActive(false);
        }
    }

}
