using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "MyCustomAsset/GameRun")]
public class GameRunSO : ScriptableObject
{
    public static void ChangeTime(float timeSpeed)//? 改變運行時間
    {
        Time.timeScale = timeSpeed;
    }
    public static UnityAction<int> PlayerHurtEvent;//? 玩家受傷事件
    public static void PlayerHurtTrigger(int damage)//? 玩家受傷事件觸發
    {
        if (PlayerHurtEvent != null)
        {
            PlayerHurtEvent.Invoke(damage);
        }
    }
    public static UnityAction<string, float> NpcTalkEvent;//? NPC對話事件
    public static void NpcTalkTrigger(string what, float time)
    {
        if (NpcTalkEvent != null)
            NpcTalkEvent.Invoke(what, time);
    }
    public static UnityAction ChangePlayerHpEvent;//? 玩家血量UI變更事件
    public static void ChangePlayerHpTrigger()
    {
        if (ChangePlayerHpEvent != null)
            ChangePlayerHpEvent.Invoke();
    }
    public static UnityAction ChangePlayerMpEvent;//? 玩家魔力UI變更事件
    public static void ChangePlayerMpTrigger()
    {
        if (ChangePlayerMpEvent != null)
            ChangePlayerMpEvent.Invoke();
    }
}
