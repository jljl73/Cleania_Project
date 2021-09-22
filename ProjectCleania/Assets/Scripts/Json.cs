using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class ItemInfo
{
    public string ID;
    public string Name;
    public string Lank;

    public ItemInfo(string id, string name, string lank)
    {
        ID = id;
        Name = name;
        Lank = lank;
    }
}

public class Json : MonoBehaviour
{
    public List<ItemInfo> itemInfos = new List<ItemInfo>();
    JsonData jsonData;

    private void Awake()
    {
        LoadItemTable();
    }

    public void AddItem(string id, string name, string lank)
    {
        itemInfos.Add(new ItemInfo(id, name, lank));
        SaveItem();
    }

    public void SaveItem()
    {
        jsonData = JsonMapper.ToJson(itemInfos);
        File.WriteAllText(Application.dataPath + "/Resources/JsonData/PlayerItemInfos.json",
            jsonData.ToString());
    }

    public void LoadItemTable()
    {
        if(File.Exists(Application.dataPath + "/Resources/JsonData/ItemCode.json"))
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Resources/JsonData/ItemCode.json");

            jsonData = JsonMapper.ToObject(jsonString);
            Debug.Log(jsonData);
        }
    }

    public void ParsingJsonItemInfo(Item item)
    {
        for (int i = 0; i < jsonData.Count; ++i)
        {
            if(jsonData[i]["Key"].ToString() == "1101001")
            {
                string name = jsonData[i]["Name"].ToString();
                int h = int.Parse(jsonData[i]["Height"].ToString());
                int w = int.Parse(jsonData[i]["Width"].ToString());
                //item.InitializeData("1101000001", name, w, h);
                break;
            }
        }
    }
}
