using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(menuName = "MyCustomAsset/SaveLoad")]
public class SaveLoadSO : ScriptableObject
{
    public static void Save()
    {
        FileStream data = new FileStream(Application.dataPath + "/GameData.txt", FileMode.Create);
        StreamWriter writer = new StreamWriter(data);
        //writer.WriteLine(GameDataSO.LastScene);
        writer.Close();
        data.Close();
    }
    public static void Load()
    {
        FileStream data = new FileStream(Application.dataPath + "/GameData.txt", FileMode.Open);
        StreamReader read = new StreamReader(data);
        //GameData.LastScene = read.ReadLine();
        //int.Parse(_read.ReadLine());
    }
    public static void QuitGame(bool needSave)
    {
        if (needSave == true)
            Save();
        Application.Quit();
    }
}
