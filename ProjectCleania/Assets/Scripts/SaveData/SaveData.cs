using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    public string characterName;
    string jsonString;
    string Path
    {
        get
        {
            return $"{Application.dataPath}/savedata/{characterName}.json";
        }
    }

    [SerializeField]
    string itemJson;
    [SerializeField]
    SaveData_Item item = new SaveData_Item();



    public bool Load()
    {
        if (File.Exists(Path))
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Path), this);

            SubLoad();

            return true;
        }
        else
        {
            Directory.CreateDirectory($"{Application.dataPath}/savedata");
            File.Create(Path);
            return false;
        }
    }

    public void Save()
    {
        SubSave();

        jsonString = JsonUtility.ToJson(this, true);
        File.WriteAllText(Path, jsonString);
        print($"saved in {Path}");
    }

    void SubLoad()
    {
        JsonUtility.FromJsonOverwrite(itemJson, item);
    }

    void SubSave()
    {
        itemJson = JsonUtility.ToJson(item, true);
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    private void Update()
    {
        item.wow2++;
    }

    private void OnDestroy()
    {
        Save();
    }

}
