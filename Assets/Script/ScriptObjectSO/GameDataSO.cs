using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyCustomAsset/GameData")]
public class GameDataSO : ScriptableObject
{
    public static string LastScene;//* 上一個保存的場景名稱
    public static float[] ResetPoint = new float[2];//* 最後接觸的休息點位置
    public static float MasterVolume = -20;
    public static float BgmVolume = -15;
    public static float SeVolume = -10;
    public static int WindowsMode;
}
