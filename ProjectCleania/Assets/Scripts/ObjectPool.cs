using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool Instance;

    public enum enumPoolObject
    {
        Pollution,
        Stain
    }

    public GameObject pollutionPrefab;
    public GameObject stainPrefab;

    Dictionary<enumPoolObject, Queue<GameObject>> poolableDict = new Dictionary<enumPoolObject, Queue<GameObject>>();

    Queue<GameObject> pollutionQueue = new Queue<GameObject>();
    Queue<GameObject> StainQueue = new Queue<GameObject>();


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        AddQueueToDict();
    }

    void AddQueueToDict()
    {
        poolableDict.Add(enumPoolObject.Pollution, pollutionQueue);
        poolableDict.Add(enumPoolObject.Stain, StainQueue);
    }

    public static GameObject GetObject(enumPoolObject objType, Vector3 pos, Quaternion rot)
    {
        if (Instance.poolableDict[objType].Count <= 0)
        {
            GameObject obj = Instance.CreateNewObject(objType, pos, rot);   // Queue에 추가되서 나옴
            if (obj == null)
                throw new System.Exception("objType is wrong!");
        }

        GameObject objToSpawn = Instance.poolableDict[objType].Dequeue();

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
            case enumPoolObject.Stain:
                obj = Instantiate(stainPrefab);
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
