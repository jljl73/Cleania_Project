using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavedData : MonoBehaviour
{
    static private SavedData _singleton;
    static public SavedData Instance
    { get => _singleton;}

    public string characterName;    
    string Path
    {
        get
        {
            return $"{Application.dataPath}/savedata/{characterName}.json";
        }
    }


    // save data 

    public ItemStorage_World Item_World = new ItemStorage_World();
    public ItemStorage_LocalGrid Item_Inventory = new ItemStorage_LocalGrid(new System.Drawing.Size(10, 6));
    public ItemStorage_LocalGrid Item_Storage = new ItemStorage_LocalGrid(new System.Drawing.Size(10, 10));

    Equipable equipable;
    [SerializeField]
    string equipableStirng;

    AbilityStatus vulnerable;
    [SerializeField]
    string vulnerableString;
    //


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
        ((iSavedData)Item_World).AfterLoad();
        ((iSavedData)Item_Inventory).AfterLoad();
        ((iSavedData)Item_Storage).AfterLoad();

        JsonUtility.FromJsonOverwrite(equipableStirng, equipable);
        ((iSavedData)equipable).AfterLoad();

        JsonUtility.FromJsonOverwrite(vulnerableString, vulnerable);
        ((iSavedData)vulnerable).AfterLoad();
    }

    void BeforeSave()
    {
        ((iSavedData)Item_World).BeforeSave();
        ((iSavedData)Item_Inventory).BeforeSave();
        ((iSavedData)Item_Storage).BeforeSave();

        ((iSavedData)equipable).BeforeSave();
        equipableStirng = JsonUtility.ToJson(equipable);

        ((iSavedData)vulnerable).BeforeSave();
        vulnerableString = JsonUtility.ToJson(vulnerable);
    }

    private void Awake()
    {
        _singleton = this;
    }

    private void Start()
    {
        vulnerable = GameManager.Instance.PlayerAbility;
        equipable = GameManager.Instance.PlayerEquipments;
        Item_World.ItemObjectPrefab = Resources.Load<GameObject>("Prefabs/ItemObject");

        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }


    public void Test_Add1101001()
    {
        if (!Item_Inventory.Add(ItemInstance.Instantiate(1101001)))
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
}
