using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavedGame : MonoBehaviour
{
    public string characterName;    
    string Path
    {
        get
        {
            return $"{Application.dataPath}/savedata/{characterName}.json";
        }
    }

    
    public SavedGame_Inventory SavedInventory = new SavedGame_Inventory();
    //SavedGame_World
    //SavedGame_SkillSet
    public SavedGame_Equipments SavedEquipments = new SavedGame_Equipments();

    [SerializeField]
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
        SavedInventory.AfterLoad();
        SavedEquipments.AfterLoad();

        JsonUtility.FromJsonOverwrite(vulnerableString, vulnerable);
    }

    void BeforeSave()
    {
        SavedInventory.BeforeSave();
        SavedEquipments.BeforeSave();

        vulnerableString = JsonUtility.ToJson(vulnerable);
    }



    private void Start()
    {
        vulnerable = GameManager.Instance.PlayerAbility;

        Load();
    }

    private void OnDisable()
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
