using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "MyCustomAsset/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    static int _MaxHp;//* 最大血量
    public static int MaxHp { get => _MaxHp; set { if (value > 40) _MaxHp = 40; else _MaxHp = value; } }
    static int _MaxMp;//* 最大魔力
    public static int MaxMp { get => _MaxMp; set { _MaxMp = value; } }
    static int _MaxAtk;//* 最大攻擊力
    public static int MaxAtk { get => _MaxAtk; set { _MaxAtk = value; } }
    static int _MaxHit;//* 最大硬直力
    public static int MaxHit { get => _MaxHit; set { _MaxHit = value; } }
    static float _MaxSpeed;//* 最大速度
    public static float MaxSpeed { get => _MaxSpeed; set { _MaxSpeed = value; } }
    static float _Money;//* 金錢數量
    public static float Money { get => _Money; set { _Money = value; if (_Money < 0) _Money = 0; } }
}
