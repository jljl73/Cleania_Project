using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool Instance;

    public enum enumPoolObject
    {
        CleaningWind,
        Dusty,
        WildInti,
        HighDusty,
        SummonerDusty,
        TheDusty,
        Toxicity,
        Seal,
        Mine,
        StormWindGroup,
        StormWind,
        Turret,
        TurretProjectile,
        Decomposition,
        Stain,
        Pollution,
        EndIndex
    }
    
    [Serializable]
    public struct Pool
    {
        public enumPoolObject EnumType;
        public GameObject GameObject;
        public int Count;
        public void AddCount() { Count += 1; }
        public override string ToString()
        {
            return Count.ToString() + " " + EnumType.ToString() + " exists";
        }
    }

    [SerializeField]
    Pool[] pools;

    void AddPoolCount(enumPoolObject enumType)
    {
        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].EnumType == enumType)
            {
                pools[i].Count += 1;
                break;
            }
        }
    }

    #region
    //[Header("플레이어 스킬 오브젝트")]
    //[SerializeField]
    //GameObject cleaningWindPrefab;

    //[Header("적 오브젝트")]
    //[SerializeField]
    //GameObject dustyPrefab;

    //[SerializeField]
    //GameObject wildIntiPrefab;

    //[SerializeField]
    //GameObject highDustyPrefab;

    //[SerializeField]
    //GameObject summonerDustyPrefab;

    //[Header("스킬 오브젝트")]
    //[SerializeField]
    //GameObject toxicityPondPrefab;

    //[SerializeField]
    //GameObject sealPrefab;

    //[SerializeField]
    //GameObject minePrefab;

    //[SerializeField]
    //GameObject stormWindGroupPrefab;

    //[SerializeField]
    //GameObject stormWindPrefab;

    //[SerializeField]
    //GameObject turretPrefab;

    //[SerializeField]
    //GameObject turretProjectilePrefab;

    //[SerializeField]
    //GameObject decompositionPrefab;

    //[SerializeField]
    //GameObject stainPrefab;

    //[SerializeField]
    //GameObject pollutionPrefab;
    #endregion
    Dictionary<enumPoolObject, Queue<GameObject>> poolableQueueDict = new Dictionary<enumPoolObject, Queue<GameObject>>();
    Dictionary<enumPoolObject, Pool> poolDict = new Dictionary<enumPoolObject, Pool>();

    private void Awake()
    {
        // 간단한 싱글톤 구현
        if (Instance != null)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        Instance = this;

        // poolDict에 pool 배열 업로드
        AddPoolToDict();

        // 오브젝트의 큐를 Dict에 업로드
        AddQueueToDict();

        // 오브젝트 초기화
        InitializeGameObjects();
    }

    void AddPoolToDict()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            poolDict.Add(pools[i].EnumType, pools[i]);
        }
    }

    void AddQueueToDict()
    {
        for (int i = 0; i < (int)enumPoolObject.EndIndex; i++)
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();
            poolableQueueDict.Add((enumPoolObject)i, newQueue);
        }
    }

    void InitializeGameObjects()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            for (int j = 0; j < pools[i].Count; j++)
            {
                CreateNewObject(pools[i].EnumType, pools[i].GameObject, this.transform.position, this.transform.rotation);
            }
        }
    }

    public static GameObject GetObject(enumPoolObject objType, Vector3 pos, Quaternion rot)
    {
        if (Instance.poolableQueueDict[objType].Count <= 0)
        {
            GameObject obj = Instance.CreateNewObject(objType, pos, rot);   // Queue에 추가되서 나옴
            Instance.AddPoolCount(objType);
            if (obj == null)
                throw new System.Exception("objType is wrong!");
        }

        if (Instance.poolableQueueDict[objType].Count == 0)
            throw new System.Exception(objType + " type queue is empty");
        GameObject objToSpawn = Instance.poolableQueueDict[objType].Dequeue();

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

    public static T SpawnFromPool<T>(enumPoolObject objType, Vector3 pos, Quaternion rot, Transform parent) where T : Component
    {
        GameObject obj = GetObject(objType, pos, rot);

        if (obj.TryGetComponent(out T component))
        {
            obj.transform.SetParent(null);
            obj.transform.SetParent(parent);
            return component;
        }
        else
        {
            obj.SetActive(false);
            throw new System.Exception("component not found!");
        }
    }
    #region
    //GameObject CreateNewObject(enumPoolObject objType, Vector3 pos, Quaternion rot)
    //{
    //    GameObject obj = null;

    //    switch (objType)
    //    {
    //        case enumPoolObject.CleaningWind:
    //            obj = Instantiate(cleaningWindPrefab);
    //            break;
    //        case enumPoolObject.Dusty:
    //            obj = Instantiate(dustyPrefab);
    //            break;
    //        case enumPoolObject.WildInti:
    //            obj = Instantiate(wildIntiPrefab);
    //            break;
    //        case enumPoolObject.HighDusty:
    //            obj = Instantiate(highDustyPrefab);
    //            break;
    //        case enumPoolObject.SummonerDusty:
    //            obj = Instantiate(summonerDustyPrefab);
    //            break;
    //        case enumPoolObject.Toxicity:
    //            obj = Instantiate(toxicityPondPrefab);
    //            break;
    //        case enumPoolObject.Seal:
    //            obj = Instantiate(sealPrefab);
    //            break;
    //        case enumPoolObject.Mine:
    //            obj = Instantiate(minePrefab);
    //            break;
    //        case enumPoolObject.StormWindGroup:
    //            obj = Instantiate(stormWindGroupPrefab);
    //            break;
    //        case enumPoolObject.StormWind:
    //            obj = Instantiate(stormWindPrefab);
    //            break;
    //        case enumPoolObject.Turret:
    //            obj = Instantiate(turretPrefab);
    //            break;
    //        case enumPoolObject.TurretProjectile:
    //            obj = Instantiate(turretProjectilePrefab);
    //            break;
    //        case enumPoolObject.Decomposition:
    //            obj = Instantiate(decompositionPrefab);
    //            break;
    //        case enumPoolObject.Stain:
    //            obj = Instantiate(stainPrefab);
    //            break;
    //        case enumPoolObject.Pollution:
    //            obj = Instantiate(pollutionPrefab);
    //            break;
    //        default:
    //            break;
    //    }
    //    if (obj != null)
    //    {
    //        obj.transform.position = pos;
    //        obj.transform.rotation = rot;
    //        obj.SetActive(false);
    //        obj.transform.SetParent(ObjectPool.Instance.transform);
    //    }

    //    return obj;
    //}
    #endregion
    GameObject CreateNewObject(enumPoolObject objType, Vector3 pos, Quaternion rot)
    {
        GameObject obj = Instantiate(poolDict[objType].GameObject, pos, rot, ObjectPool.Instance.transform);

        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(false);
        obj.name = objType.ToString();
        obj.transform.SetParent(ObjectPool.Instance.transform);

        return obj;
    }

    GameObject CreateNewObject(enumPoolObject objType, GameObject objPrefab, Vector3 pos, Quaternion rot)
    {
        GameObject obj = Instantiate(objPrefab, pos, rot, ObjectPool.Instance.transform);

        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(false);
        obj.name = objType.ToString();
        obj.transform.SetParent(ObjectPool.Instance.transform);

        return obj;
    }

    public static void ReturnObject(enumPoolObject objType, GameObject obj)
    {
        obj.SetActive(false);
        Instance.poolableQueueDict[objType].Enqueue(obj);
    }

    public void PrintCurrentState()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            print(pools[i].EnumType.ToString() + " has " + pools[i].Count + " Object");
        }
    }
}
