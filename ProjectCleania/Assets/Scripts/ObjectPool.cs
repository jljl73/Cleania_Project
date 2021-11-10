using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    public enum enumPoolObject
    {
        Pollution
    }

    public GameObject pollutionPrefab;




    Dictionary<enumPoolObject, Queue<GameObject>> poolableDict = new Dictionary<enumPoolObject, Queue<GameObject>>();

    Queue<GameObject> pollutionQueue = new Queue<GameObject>();


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        AddQueueToDict();
    }

    void AddQueueToDict()
    {
        poolableDict.Add(enumPoolObject.Pollution, pollutionQueue);
    }

    public static GameObject GetObject(enumPoolObject objType, Vector3 pos, Quaternion rot)
    {
        //if (Instance.poolableDict[objType].Count > 0)
        //{
        //    print("pooled object!");
        //    GameObject obj = Instance.poolableDict[objType].Dequeue();

        //    // 전처리
        //    obj.transform.position = pos;
        //    obj.transform.rotation = rot;
        //    obj.SetActive(true);
        //    return obj;
        //}
        //else
        //{
        //    print("created object!");
        //    GameObject obj = Instance.CreateNewObject(objType, pos, rot);
        //    if (obj == null)
        //        throw new System.Exception("objType is wrong!");
        //    obj.SetActive(true);
        //    return obj;
        //}

        if (Instance.poolableDict[objType].Count <= 0)
        {
            GameObject obj = Instance.CreateNewObject(objType, pos, rot);   // 오브젝트 풀에 삽입되고 비활성화된 상태
            if (obj == null)
                throw new System.Exception("objType is wrong!");
        }

        GameObject objToSpawn = Instance.poolableDict[objType].Dequeue();

        // 전처리
        objToSpawn.transform.position = pos;
        objToSpawn.transform.rotation = rot;
        objToSpawn.SetActive(true);
        return objToSpawn;
    }

    public static T SpawnFromPool<T>(enumPoolObject objType, Vector3 pos, Quaternion rot) where T : Component
    {
        GameObject obj = GetObject(objType, pos, rot);
        
        if (obj.TryGetComponent(out T component))
        {
            return component;
        }
        else
        {
            obj.SetActive(false);
            throw new System.Exception("component not found!");
        }
    }

    GameObject CreateNewObject(enumPoolObject objType, Vector3 pos, Quaternion rot)
    {
        GameObject obj = null;
        switch (objType)
        {
            case enumPoolObject.Pollution:
                obj = Instantiate(pollutionPrefab);
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(false);
                break;
            default:
                break;
        }

        return obj;
    }

    public static void ReturnObject(enumPoolObject objType, GameObject obj)
    {
        obj.SetActive(false);
        Instance.poolableDict[objType].Enqueue(obj);
    }
}
