using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "MyCustomAsset/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    static int _playerMaxHp;//* 最大血量
    public static int PlayerMaxHp { get => _playerMaxHp; set { if (value > 40) _playerMaxHp = 40; else _playerMaxHp = value; } }
    static int _playerMaxMp;//* 最大魔力
    public static int PlayerMaxMp { get => _playerMaxMp; set { _playerMaxMp = value; } }
    static int _playerMaxAtk;//* 最大攻擊力
    public static int PlayerMaxAtk { get => _playerMaxAtk; set { _playerMaxAtk = value; } }
    static int _playerMaxHit;//* 最大硬直力
    public static int PlayerMaxHit { get => _playerMaxHit; set { _playerMaxHit = value; } }
    static int _playerMaxSpeed = 15;//* 最大速度
    public static int PlayerMaxSpeed { get => _playerMaxSpeed; set { _playerMaxSpeed = value; } }
    static int _playerNowHp;//* 當前血量
    public static int PlayerNowHp { get => _playerNowHp; set { if (_playerNowHp > _playerMaxHp) _playerNowHp = _playerMaxHp; else _playerNowHp = value; GameRunSO.ChangePlayerHpTrigger(); } }
    static int _playerNowMp;//* 當前魔力
    public static int PlayerNowMp { get => _playerNowMp; set { if (_playerNowMp > _playerMaxMp) _playerNowMp = _playerMaxMp; else _playerNowMp = value; GameRunSO.ChangePlayerMpTrigger(); } }
    static int _playerNowAtk;//* 當前攻擊力
    public static int PlayerNowAtk { get => _playerNowAtk; set { if (_playerNowAtk > _playerMaxAtk) _playerNowAtk = _playerMaxAtk; _playerNowAtk = value; } }
    static int _playerNowHit;//* 當前硬直力
    public static int PlayerNowHit { get => _playerNowHit; set { if (_playerNowHit > _playerMaxHit) _playerNowHit = _playerMaxHit; _playerNowHit = value; } }
    static float _playerMoney;//* 金錢數量
    public static float PlayerMoney { get => _playerMoney; set { _playerMoney = value; if (_playerMoney < 0) _playerMoney = 0; } }
    static int _skillMaxPoint;//* 最大技能點數
    public static int SkillMaxPoint { get => _skillMaxPoint; set { if (value > 10) _skillMaxPoint = 10; else _skillMaxPoint = value; } }
    static int _skillNowPoint;//* 已消耗的技能點數
    public static int SkillNowPoint { get => _skillNowPoint; set { _skillNowPoint = value; } }
    static bool _super = false;//* 無敵狀態
    public static bool Super { get => _super; set { _super = value; } }
    static Transform _playerTrans;//* 玩家的位置
    public static Transform PlayerTrans { get => _playerTrans; set { _playerTrans = value; } }
    static bool _canControl;//* 玩家是否能控制
    public static bool CanControl { get => _canControl; set { _canControl = value; } }
    static bool _cantFindPlayer = false;//* 怪物是否能找到玩家
    public static bool CantFindPlayer { get => _cantFindPlayer; set { _cantFindPlayer = value; } }

    public static UnityAction<int> AddMpEvent;//? 增加魔力事件 (魔力吸取技能訂閱)
    public static void AddMpTrigger(int much)//? 增加魔力事件觸發(任何會增加魔力的東西都需要觸發該方法，否則不會觸發魔力吸取技能效果)
    {
        if (AddMpEvent != null)
            AddMpEvent.Invoke(much);
    }
    public static UnityAction<int> AddMoneyEvent;//? 增加金錢事件 (拜金技能訂閱)
    public static void AddMoneyTrigger(int much)//? 增加金錢事件觸發(任何會增加金錢的東西都需要觸發該方法，否則不會觸發拜金技能效果)
    {
        if (AddMoneyEvent != null)
            AddMoneyEvent.Invoke(much);
    }
}
