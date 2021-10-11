using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour, iSaveData
{
    public string characterName;    
    string Path
    {
        get
        {
            return $"{Application.dataPath}/savedata/{characterName}.json";
        }
    }

    [SerializeField]
    double playTime = 0;


    [SerializeField]
    SaveData_Item item = new SaveData_Item();

    AbilityStatus player;
    [SerializeField]
    string playerJson;



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

    public void Save()
    {
        BeforeSave();

        File.WriteAllText(Path, JsonUtility.ToJson(this, true));
        //print($"saved in {Path}");
    }

    public void AfterLoad()
    {
        item.AfterLoad();
        JsonUtility.FromJsonOverwrite(playerJson, player);
    }

    public void BeforeSave()
    {
        item.BeforeSave();
        playerJson = JsonUtility.ToJson(player);
    }


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.PlayerAbility;

        Load();
    }

    private void Update()
    {
        playTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        Save();
    }

}
