using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavedData : MonoBehaviour
{
    static private SavedData _singleton;
    static public SavedData Instance
    {
        get
        {
            if(_singleton == null)
            {
                GameObject go = new GameObject("_SavedData");
                _singleton = go.AddComponent<SavedData>();

                _singleton.characterName = "debug";

                _singleton.Start();
            }

            return _singleton;
        }
    }

    [SerializeField]
    private string characterName;
    public string CharacterName
    {
        get => characterName;
        set
        {
            characterName = value;
            Load();
        }
    }

    string Path
    {
        get
        {
            return $"{Application.dataPath}/savedata/{characterName}.json";
        }
    }


    // save data 
    [SerializeField]
    ItemStorage_Equipments item_Equipments = new ItemStorage_Equipments();
    [SerializeField]
    ItemStorage_LocalGrid item_Inventory = new ItemStorage_LocalGrid(new System.Drawing.Size(10, 6));
    [SerializeField]
    ItemStorage_LocalGrid item_Storage = new ItemStorage_LocalGrid(new System.Drawing.Size(10, 10));
    [System.NonSerialized]
    ItemStorage_World item_World = new ItemStorage_World();

    public ItemStorage_Equipments Item_Equipments { get => item_Equipments; private set => item_Equipments = value; }
    public ItemStorage_LocalGrid Item_Inventory { get => item_Inventory; private set => item_Inventory = value; }
    public ItemStorage_LocalGrid Item_Storage { get => item_Storage; private set => item_Storage = value; }
    public ItemStorage_World Item_World { get => item_World; private set => item_World = value; }


    public int PlayerExp;

    //AbilityStatus vulnerable;
    //[SerializeField]
    //string vulnerableString;

    //[SerializeField]s
    //Vector3 playerPosition;


    // Exp int
    //      Init level by total xp

    /// <summary>
    /// Load saved game to primary memory.
    /// </summary>
    /// <returns></returns>
    public bool Load()
    {
        if (File.Exists(Path))
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Path), this);
            AfterLoad();
            return true;
        }
        else
        {
            Directory.CreateDirectory($"{Application.dataPath}/savedata");
            File.Create(Path);
            AfterLoad();
            return false;
        }
    }

    /// <summary>
    /// Save saved game to secondary memory.
    /// </summary>
    public void Save()
    {
        BeforeSave();
        File.WriteAllText(Path, JsonUtility.ToJson(this, true));
    }


    void AfterLoad()
    {
        ((iSavedData)item_Equipments).AfterLoad();
        ((iSavedData)item_Inventory).AfterLoad();
        ((iSavedData)item_Storage).AfterLoad();
        //((iSavedData)item_World).AfterLoad();

        //JsonUtility.FromJsonOverwrite(vulnerableString, vulnerable);
        //((iSavedData)vulnerable).AfterLoad();

        //GameManager.Instance.SinglePlayer.transform.position = playerPosition;
        //GameManager.Instance.SinglePlayer.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        //GameManager.Instance.player.playerMove.StopMoving();

        ExpManager.Initailize(PlayerExp);
    }

    void BeforeSave()
    {
        ((iSavedData)item_Equipments).BeforeSave();
        ((iSavedData)item_Inventory).BeforeSave();
        ((iSavedData)item_Storage).BeforeSave();
        //((iSavedData)item_World).BeforeSave();

        //((iSavedData)vulnerable).BeforeSave();
        //vulnerableString = JsonUtility.ToJson(vulnerable);

        //playerPosition = GameManager.Instance.SinglePlayer.transform.position;

        PlayerExp = ExpManager.Exp;
    }

    private void Awake()
    {
        if(_singleton != null)
        {
            _singleton.item_Equipments.ShareSubscribers(this.item_Equipments);
            _singleton.item_Inventory.ShareSubscribers(this.item_Inventory);
            _singleton.item_Storage.ShareSubscribers(this.item_Storage);
            _singleton.item_World.ShareSubscribers(this.item_World);
            Destroy(_singleton.gameObject);
        }

        _singleton = this;
    }

    private void Start()
    {
        item_World.ItemObjectPrefab = Resources.Load<GameObject>("Prefabs/ItemObject");
        Load();
        DontDestroyOnLoad(gameObject);
    }


    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDestroy()
    {
        if (_singleton == this)
            _singleton = null;
    }




    // tests

    public void Test_Add1101001()
    {
        if (!item_Inventory.Add(ItemInstance.Instantiate(1101001)))
            print("failed to add in inventory");
    }
    public void Test_AddRandom()
    {
        if (!item_Inventory.Add(ItemInstance.Instantiate_RandomByRank((ItemSO.enumRank)Random.Range(0, 3))))
            print("failed to add in inventory");
    }
    public void Test_Drop1101001()
    {
        if (!item_World.Add(ItemInstance.Instantiate(1101001)))
            print("failed to drop in world");
    }
    public void Test_RemoveAll()
    {
        foreach (var i in item_Inventory.Items)
        {
            item_Inventory.Remove(i.Key);
        }

        foreach(var i in item_World.Items)
        {
            item_World.Remove(i.Key);
        }
    }

    public void Test_EquipFirst()
    {
        for (int y = 0; y < item_Inventory.GridSize.Height; ++y)
            for (int x = 0; x < item_Inventory.GridSize.Width; ++x)
            {
                ItemInstance item = item_Inventory[y, x];
                if (item != null && item is ItemInstance_Equipment)
                {
                    item_Inventory.Remove(item);
                    ItemInstance old = item_Equipments[((ItemInstance_Equipment)item).EquipmentType];
                    item_Equipments.Remove(old);
                    item_Equipments.Add(item);
                    item_Inventory.Add(old);
                    return;
                }
            }   
    }
}