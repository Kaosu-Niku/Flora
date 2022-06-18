using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoad
{
    static int _test;
    public static void _save(){
        FileStream _data = new FileStream(Application.dataPath + "/GameData.txt" , FileMode.Create);
        StreamWriter _writer = new StreamWriter(_data);
        _writer.WriteLine(_test);
        _writer.Close();
        _data.Close();
    }
    public static void _load(){
        FileStream _data = new FileStream(Application.dataPath + "/GameData.txt" , FileMode.Open);
        StreamReader _read = new StreamReader(_data);
        _test = int.Parse(_read.ReadLine());
    }
}
