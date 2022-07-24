using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyCustomAsset/GameObjectPool")]
public class GameObjectPoolSO : ScriptableObject
{
    [System.Serializable]
    public class NewObject
    {
        public string Tag;//* 該物品的名稱
        public PoolObject Obj;
        public int SetCount;//* 該物品的預熱數量
    }
    [SerializeField] List<NewObject> SetAllObject = new List<NewObject>();
    static List<NewObject> GetAllObject;
    public static Dictionary<string, Queue<PoolObject>> AllObject;
    public void FirstSet()
    {
        GetAllObject = SetAllObject;
        AllObject = new Dictionary<string, Queue<PoolObject>>();
        for (int x = 0; x < GetAllObject.Count; x++)
        {
            Queue<PoolObject> ThisObj = new Queue<PoolObject>();
            for (int y = 0; y < GetAllObject[x].SetCount; y++)
            {
                PoolObject g = Instantiate(GetAllObject[x].Obj);
                g.MyTag = GetAllObject[x].Tag;
                ThisObj.Enqueue(g);
                g.gameObject.SetActive(false);
            }
            AllObject.Add(GetAllObject[x].Tag, ThisObj);
        }
    }
    public static PoolObject GetObject(string tag, Vector3 pos, Quaternion rot)
    {
        if (AllObject.ContainsKey(tag) == true)
        {
            if (AllObject[tag].Count > 0)
            {
                PoolObject g = AllObject[tag].Dequeue();
                g.gameObject.SetActive(true);
                g.transform.position = pos;
                g.transform.rotation = rot;
                return g;
            }
            else
            {
                for (int x = 0; x < GetAllObject.Count; x++)
                {
                    if (GetAllObject[x].Tag == tag)
                    {
                        PoolObject g = Instantiate(GetAllObject[x].Obj);
                        g.MyTag = GetAllObject[x].Tag;
                        g.transform.position = pos;
                        g.transform.rotation = rot;
                        return g;
                    }
                }
                Debug.Log("不可能找不到");
                return null;
            }
        }
        else
        {
            Debug.Log("找不到");
            return null;
        }
    }
    public static void BackObject(string tag, PoolObject obj)
    {
        if (AllObject.ContainsKey(tag) != false)
        {
            AllObject[tag].Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
    }
}
