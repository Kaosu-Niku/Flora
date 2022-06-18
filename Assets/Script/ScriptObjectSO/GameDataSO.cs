using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyCustomAsset/GameData")]
public class GameDataSO : ScriptableObject
{
    public static string LastScene;//* 上一個保存的場景名稱
    public static float[] ResetPoint = new float[2];//* 最後接觸的休息點位置
}
