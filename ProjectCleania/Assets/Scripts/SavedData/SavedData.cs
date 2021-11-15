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

                _singleton.OnEnable();
            }

            return _singleton;
        }
    }

    public string characterName;    
    string Path
    {
        get
        {
            return $"{Application.dataPath}/savedata/{characterName}.json";
        }
    }


    // save data 
    public ItemStorage_Equipments Item_Equipments = new ItemStorage_Equipments();
    public ItemStorage_LocalGrid Item_Inventory = new ItemStorage_LocalGrid(new System.Drawing.Size(10, 6));
    public ItemStorage_LocalGrid Item_Storage = new ItemStorage_LocalGrid(new System.Drawing.Size(10, 10));
    [System.NonSerialized]
    public ItemStorage_World Item_World = new ItemStorage_World();


    //AbilityStatus vulnerable;
    ////[SerializeField]
    //[System.NonSerialized]
    //string vulnerableString;

    ////[SerializeField]
    //[System.NonSerialized]
    //Vector3 playerPosition;
    //

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
        ((iSavedData)Item_Equipments).AfterLoad();
        ((iSavedData)Item_Inventory).AfterLoad();
        ((iSavedData)Item_Storage).AfterLoad();
        //((iSavedData)Item_World).AfterLoad();

        //JsonUtility.FromJsonOverwrite(vulnerableString, vulnerable);
        //((iSavedData)vulnerable).AfterLoad();

        //GameManager.Instance.SinglePlayer.transform.position = playerPosition;
        //GameManager.Instance.SinglePlayer.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        //GameManager.Instance.player.playerMove.StopMoving();

        GameManager.Instance.PlayerAbility.FullHP();
        GameManager.Instance.PlayerAbility.FullMP();
    }

    void BeforeSave()
    {
        ((iSavedData)Item_Equipments).BeforeSave();
        ((iSavedData)Item_Inventory).BeforeSave();
        ((iSavedData)Item_Storage).BeforeSave();
        //((iSavedData)Item_World).BeforeSave();

        //((iSavedData)vulnerable).BeforeSave();
        //vulnerableString = JsonUtility.ToJson(vulnerable);

        //playerPosition = GameManager.Instance.SinglePlayer.transform.position;
    }

    private void Awake()
    {
        if(_singleton != null)
        {
            _singleton.Item_Equipments.ShareSubscribers(this.Item_Equipments);
            _singleton.Item_Inventory.ShareSubscribers(this.Item_Inventory);
            _singleton.Item_Storage.ShareSubscribers(this.Item_Storage);
            _singleton.Item_World.ShareSubscribers(this.Item_World);
            Destroy(_singleton.gameObject);
        }

        _singleton = this;
    }

    private void OnEnable()
    {
        Item_World.ItemObjectPrefab = Resources.Load<GameObject>("Prefabs/ItemObject");
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


    public void Test_Add1101001()
    {
        if (!Item_Inventory.Add(ItemInstance.Instantiate(1101001)))
            print("failed to add in inventory");
    }
    public void Test_AddRandom()
    {
        if (!Item_Inventory.Add(ItemInstance.Instantiate_RandomByRank((ItemSO.enumRank)Random.Range(0, 3))))
            print("failed to add in inventory");
    }
    public void Test_Drop1101001()
    {
        if (!Item_World.Add(ItemInstance.Instantiate(1101001)))
            print("failed to drop in world");
    }
    public void Test_RemoveAll()
    {
        foreach (var i in Item_Inventory.Items)
        {
            Item_Inventory.Remove(i.Key);
        }

        foreach(var i in Item_World.Items)
        {
            Item_World.Remove(i.Key);
        }
    }

    public void Test_EquipFirst()
    {
        for (int y = 0; y < Item_Inventory.GridSize.Height; ++y)
            for (int x = 0; x < Item_Inventory.GridSize.Width; ++x)
            {
                ItemInstance item = Item_Inventory[y, x];
                if (item != null && item is ItemInstance_Equipment)
                {
                    Item_Inventory.Remove(item);
                    ItemInstance old = Item_Equipments[((ItemInstance_Equipment)item).EquipmentType];
                    Item_Equipments.Remove(old);
                    Item_Equipments.Add(item);
                    Item_Inventory.Add(old);
                    return;
                }
            }   
    }
}