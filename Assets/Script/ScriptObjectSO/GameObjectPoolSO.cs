using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyCustomAsset/GameObjectPool")]
public class GameObjectPoolSO : ScriptableObject
{
    [SerializeField] List<GameObject> AllGameObject = new List<GameObject>();//? 分配所有物件
    public GameObject GetGameObject(int which, Vector3 pos, Quaternion rot)//? 生出指定物件
    {
        if (AllGameObject[which] != null)
            return Instantiate(AllGameObject[which], pos, rot);
        else
            return null;
    }
}
