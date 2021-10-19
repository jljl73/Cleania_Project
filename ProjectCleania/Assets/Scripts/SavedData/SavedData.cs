using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavedData : MonoBehaviour
{
    public string characterName;    
    string Path
    {
        get
        {
            return $"{Application.dataPath}/savedata/{characterName}.json";
        }
    }

    
    public SavedData_Inventory SavedInventory = new SavedData_Inventory();
    //SavedData_World
    //SavedData_SkillSet
    public SavedData_Equipments SavedEquipments = new SavedData_Equipments();

    public ItemStorage_World WorldStorage = new ItemStorage_World();

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
        ((iSavedData)SavedInventory).AfterLoad();
        ((iSavedData)SavedEquipments).AfterLoad();
        ((iSavedData)WorldStorage).AfterLoad();

        JsonUtility.FromJsonOverwrite(vulnerableString, vulnerable);
    }

    void BeforeSave()
    {
        ((iSavedData)SavedInventory).BeforeSave();
        ((iSavedData)SavedEquipments).BeforeSave();
        ((iSavedData)WorldStorage).BeforeSave();

        vulnerableString = JsonUtility.ToJson(vulnerable);
    }



    private void Start()
    {
        vulnerable = GameManager.Instance.PlayerAbility;
        WorldStorage.ItemObjectPrefab = Resources.Load<GameObject>("Prefabs/ItemObject");

        Load();
    }

   

    private void OnApplicationQuit()
    {
        Save();
    }


    public void Test_Add1101001()
    {
        SavedInventory.inventory.Add(ItemInstance.Instantiate(1101001));
    }
    public void Test_Drop1101001()
    {
        WorldStorage.Add(ItemInstance.Instantiate(1101001));
    }
    public void Test_RemoveAll()
    {
        foreach (var i in SavedInventory.inventory.Items)
        {
            SavedInventory.inventory.Remove(i.Key);
        }

        foreach(var i in WorldStorage.Items)
        {
            WorldStorage.Remove(i.Key);
        }
    }
}
