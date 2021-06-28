using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    GameObject targetObj;
    List<GameObject> pool = new List<GameObject>();

    public ObjectPool(GameObject objToPool)
    {
        targetObj = objToPool;
        GameObject obj = Object.Instantiate(objToPool);
        obj.SetActive(false);
        pool.Add(obj);
    }

    public GameObject GetObjectFromPool()
    {
        foreach(var item in pool)
        {
            if (!item.activeInHierarchy) 
            {
                return item;
            } 
        }
        GameObject obj = Object.Instantiate(targetObj);
        pool.Add(obj);
        return obj;
    }
}
