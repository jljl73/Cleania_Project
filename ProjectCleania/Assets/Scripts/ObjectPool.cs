using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool Instance;

    public enum enumPoolObject
    {
        Dusty,
        WildInti,
        HighDusty,
        SummonerDusty,
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
    [Header("�� ������Ʈ")]
    [SerializeField]
    GameObject dustyPrefab;

    [SerializeField]
    GameObject wildIntiPrefab;

    [SerializeField]
    GameObject highDustyPrefab;

    [SerializeField]
    GameObject summonerDustyPrefab;

    [Header("��ų ������Ʈ")]
    [SerializeField]
    GameObject toxicityPondPrefab;

    [SerializeField]
    GameObject sealPrefab;

    [SerializeField]
    GameObject minePrefab;

    [SerializeField]
    GameObject stormWindGroupPrefab;

    [SerializeField]
    GameObject stormWindPrefab;

    [SerializeField]
    GameObject turretPrefab;

    [SerializeField]
    GameObject turretProjectilePrefab;

    [SerializeField]
    GameObject decompositionPrefab;

    [SerializeField]
    GameObject stainPrefab;

    [SerializeField]
    GameObject pollutionPrefab;

    Dictionary<enumPoolObject, Queue<GameObject>> poolableDict = new Dictionary<enumPoolObject, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        AddQueueToDict();
    }

    void AddQueueToDict()
    {
        for (int i = 0; i < (int)enumPoolObject.EndIndex; i++)
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();
            poolableDict.Add((enumPoolObject)i, newQueue);
        }
    }

    public static GameObject GetObject(enumPoolObject objType, Vector3 pos, Quaternion rot)
    {
        if (Instance.poolableDict[objType].Count <= 0)
        {
            GameObject obj = Instance.CreateNewObject(objType, pos, rot);   // Queue�� �߰��Ǽ� ����
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
            obj.transform.SetParent(ObjectPool.Instance.transform);
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
            obj.transform.SetParent(parent);
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
            case enumPoolObject.Dusty:
                obj = Instantiate(dustyPrefab);
                break;
            case enumPoolObject.WildInti:
                obj = Instantiate(wildIntiPrefab);
                break;
            case enumPoolObject.HighDusty:
                obj = Instantiate(highDustyPrefab);
                break;
            case enumPoolObject.SummonerDusty:
                obj = Instantiate(summonerDustyPrefab);
                break;
            case enumPoolObject.Toxicity:
                obj = Instantiate(toxicityPondPrefab);
                break;
            case enumPoolObject.Seal:
                obj = Instantiate(sealPrefab);
                break;
            case enumPoolObject.Mine:
                obj = Instantiate(minePrefab);
                break;
            case enumPoolObject.StormWindGroup:
                obj = Instantiate(stormWindGroupPrefab);
                break;
            case enumPoolObject.StormWind:
                obj = Instantiate(stormWindPrefab);
                break;
            case enumPoolObject.Turret:
                obj = Instantiate(turretPrefab);
                break;
            case enumPoolObject.TurretProjectile:
                obj = Instantiate(turretProjectilePrefab);
                break;
            case enumPoolObject.Decomposition:
                obj = Instantiate(decompositionPrefab);
                break;
            case enumPoolObject.Stain:
                obj = Instantiate(stainPrefab);
                break;
            case enumPoolObject.Pollution:
                obj = Instantiate(pollutionPrefab);
                break;
            default:
                break;
        }
        if (obj != null)
        {
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(false);
        }
        
        return obj;
    }

    public static void ReturnObject(enumPoolObject objType, GameObject obj)
    {
        obj.SetActive(false);
        Instance.poolableDict[objType].Enqueue(obj);
    }
}
