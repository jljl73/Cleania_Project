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

    public SavedData_Items Item = new SavedData_Items();
    public SavedData_Equipments Equipment = new SavedData_Equipments();

    AbilityStatus vulnerable;
    [SerializeField]
    string vulnerableString;


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
        ((iSavedData)Equipment).AfterLoad();
        ((iSavedData)Item).AfterLoad();

        JsonUtility.FromJsonOverwrite(vulnerableString, vulnerable);
    }

    void BeforeSave()
    {
        ((iSavedData)Equipment).BeforeSave();
        ((iSavedData)Item).BeforeSave();

        vulnerableString = JsonUtility.ToJson(vulnerable);
    }

    private void Awake()
    {
        _singleton = this;
    }

    private void Start()
    {
        vulnerable = GameManager.Instance.PlayerAbility;
        Item.World.ItemObjectPrefab = Resources.Load<GameObject>("Prefabs/ItemObject");

        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }


    public void Test_Add1101001()
    {
        if (!Item.Inventory.Add(ItemInstance.Instantiate(1101001)))
            print("failed to add in inventory");
    }
    public void Test_Drop1101001()
    {
        if (!Item.World.Add(ItemInstance.Instantiate(1101001)))
            print("failed to drop in world");
    }
    public void Test_RemoveAll()
    {
        foreach (var i in Item.Inventory.Items)
        {
            Item.Inventory.Remove(i.Key);
        }

        foreach(var i in Item.World.Items)
        {
            Item.World.Remove(i.Key);
        }
    }
}
