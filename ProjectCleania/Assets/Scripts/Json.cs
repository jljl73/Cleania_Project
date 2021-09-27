using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using LitJson;

public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;

    Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }

    public Serialization(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize()
    {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i)
            target.Add(keys[i], values[i]);
    }

}

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

    [System.Serializable]
    class equip
    {
        public int id;
        public int first;
        //public int[] vs = new int[3];
        //[SerializeField]
        //public List<int> vs = new List<int>();

        public equip(int id, int b)
        {
            first = id;
            //vs[0] = 1;
        }
    }


    List<equip> equips = new List<equip>();
    Dictionary<Item.ITEMBIGCATEGORY, int> diction = new Dictionary<Item.ITEMBIGCATEGORY, int>();
    

    private void Awake()
    {
        Test();
        //LoadItemTable();
    }

    void Test()
    {
        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);
        writer.PrettyPrint = true;
        equips.Add(new equip(2, 4));
        JsonMapper.ToJson(equips, writer);
        Debug.Log(sb);
        Debug.Log(writer.ToString());
        File.WriteAllText(Application.dataPath + "/Resources/JsonData/qqq.json", sb.ToString());
    }


    //public void AddItem(string id, string name, string lank)
    //{
    //    itemInfos.Add(new ItemInfo(id, name, lank));
    //    SaveItem();
    //}

    //public void SaveItem()
    //{
    //    jsonData = JsonMapper.ToJson(itemInfos);
    //    File.WriteAllText(Application.dataPath + "/Resources/JsonData/PlayerItemInfos.json",
    //        jsonData.ToString());
    //}

    //public void LoadItemTable()
    //{
    //    if (File.Exists(Application.dataPath + "/Resources/JsonData/ItemCode.json"))
    //    {
    //        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/JsonData/ItemCode.json");

    //        jsonData = JsonMapper.ToObject(jsonString);
    //    }
    //}

    //public void ParsingJsonItemInfo(Item item)
    //{
    //    for (int i = 0; i < jsonData.Count; ++i)
    //    {
    //        if (jsonData[i]["Key"].ToString() == "1101001")
    //        {
    //            string name = jsonData[i]["Name"].ToString();
    //            int h = int.Parse(jsonData[i]["Height"].ToString());
    //            int w = int.Parse(jsonData[i]["Width"].ToString());
    //            //item.InitializeData("1101000001", name, w, h);
    //            break;
    //        }
    //    }
    //}
}
