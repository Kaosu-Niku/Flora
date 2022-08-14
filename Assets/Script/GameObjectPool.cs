using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class NewObject
    {
        public string Tag;//* 該物品的名稱
        public IPoolObject Obj;
        public int SetCount;//* 該物品的預熱數量
    }
    [SerializeField] List<NewObject> GetAllObject = new List<NewObject>();
    public Dictionary<string, Queue<IPoolObject>> AllObject;
    private void Awake()
    {
        AllObject = new Dictionary<string, Queue<IPoolObject>>();
        for (int x = 0; x < GetAllObject.Count; x++)
        {
            Queue<IPoolObject> ThisObj = new Queue<IPoolObject>();
            for (int y = 0; y < GetAllObject[x].SetCount; y++)
            {
                IPoolObject g = Instantiate(GetAllObject[x].Obj);
                g.MyPool = this;
                g.MyTag = GetAllObject[x].Tag;
                ThisObj.Enqueue(g);
                g.gameObject.SetActive(false);
            }
            AllObject.Add(GetAllObject[x].Tag, ThisObj);
        }
    }
    public IPoolObject GetObject(string tag, Vector3 pos, Quaternion rot)
    {
        if (AllObject.ContainsKey(tag) == true)
        {
            if (AllObject[tag].Count > 0)
            {
                IPoolObject g = AllObject[tag].Dequeue();
                g.transform.position = pos; Debug.Log("數量還夠");
                g.transform.rotation = rot;
                g.gameObject.SetActive(true);
                return g;
            }
            else
            {
                for (int x = 0; x < GetAllObject.Count; x++)
                {
                    if (GetAllObject[x].Tag == tag)
                    {
                        IPoolObject g = Instantiate(GetAllObject[x].Obj);
                        g.MyPool = this;
                        g.MyTag = GetAllObject[x].Tag; Debug.Log("數量不夠");
                        g.transform.position = pos;
                        g.transform.rotation = rot;
                        g.gameObject.SetActive(true);
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
    public void BackObject(string tag, IPoolObject obj)
    {
        if (AllObject.ContainsKey(tag) != false)
        {
            AllObject[tag].Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
    }
}
