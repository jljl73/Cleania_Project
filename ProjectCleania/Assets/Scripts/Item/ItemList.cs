using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text;

public class ItemList : MonoBehaviour
{
    public InventoryItemGenerator generator;
    JsonData jsonData;

    List<Item> items_Inventory = new List<Item>();
    List<Item> items_Field = new List<Item>();
    List<EquipmentOption> equipments = new List<EquipmentOption>();

    private void Start()
    {
        //SaveItem();
        //LoadItem();
        StartCoroutine(SaveItem());
    }

    public void AddToInventory(Item item)
    {
        //int number = int.Parse(code);

        int idx = items_Field.FindIndex(a => a.ItemID == item.ItemID);
        if (idx >= 0)
            items_Field.RemoveAt(idx);

        items_Inventory.Add(item.DeepCopy());
    }

    public void AddToField(Item item)
    {
        int idx = items_Inventory.FindIndex(a => a.ItemID == item.ItemID);
        if (idx >= 0)
            items_Inventory.RemoveAt(idx);

        items_Field.Add(item.DeepCopy());
    }

    IEnumerator SaveItem()
    {
        yield return new WaitForSeconds(1.0f);
        LoadItem();

        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            //foreach (Item i in items_Inventory)
            //    Debug.Log(i.ItemID.ToString() + ", " + i.ItemCode);

            jsonData = JsonMapper.ToJson(items_Inventory);
            File.WriteAllText(Application.dataPath + "/Resources/JsonData/PlayerInventory.json",
                jsonData.ToString());
        }
    }

    public void AddOption(EquipmentOption newEquipment)
    {
        equipments.Add(newEquipment);
        SaveItemOption();
    }

    void SaveItemOption()
    {
        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);
        //writer.PrettyPrint = true;
        JsonMapper.ToJson(equipments, writer);
        File.WriteAllText(Application.dataPath + "/Resources/JsonData/qqq.json", sb.ToString());

    }


    void LoadItem()
    {
        if (File.Exists(Application.dataPath + "/Resources/JsonData/PlayerInventory.json"))
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Resources/JsonData/PlayerInventory.json");

            jsonData = JsonMapper.ToObject(jsonString);

            for (int i = 0; i < jsonData.Count; ++i)
            {
                string itemCode = jsonData[i]["ItemCode"].ToString();
                string itemName = jsonData[i]["ItemName"].ToString();
                Item newItem = new Item();
                newItem.CodeParsing(itemCode, itemName);
                generator.GenerateItem(newItem);
                AddToInventory(newItem);
            }
        }
    }

    EquipmentOption LoadItemOption(Item.ITEMSMALLCATEGORY type, Item.ITEMRANK rank, int ItemID, int level)
    {
        EquipmentOption newOption = new EquipmentOption(type, rank, ItemID, level);

        List<EquipmentOption.Option> StaticOptionKeys = new List<EquipmentOption.Option>();
        List<int> StaticOptionValues = new List<int>();
        List<EquipmentOption.Option> VariableOptionKeys = new List<EquipmentOption.Option>();
        List<int> VariableOptionValues = new List<int>();

        if (File.Exists(Application.dataPath + "/Resources/JsonData/qqq.json"))
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Resources/JsonData/qqq.json");

            jsonData = JsonMapper.ToObject(jsonString);

            for (int i = 0; i < jsonData.Count; ++i)
            {
                int itemID = int.Parse(jsonData[i]["ItemID"].ToString());
                if (itemID != ItemID) continue;

                foreach (var value in jsonData[i]["StaticOptionKeys"])
                {
                    StaticOptionKeys.Add((EquipmentOption.Option)(int.Parse(value.ToString())));
                }
                foreach (var value in jsonData[i]["StaticOptionValues"])
                {
                    StaticOptionValues.Add((int.Parse(value.ToString())));
                }
                foreach (var value in jsonData[i]["VariableOptionKeys"])
                {
                    VariableOptionKeys.Add((EquipmentOption.Option)(int.Parse(value.ToString())));
                }
                foreach (var value in jsonData[i]["VariableOptionValues"])
                {
                    VariableOptionValues.Add((int.Parse(value.ToString())));
                }

                newOption.Load(StaticOptionKeys, StaticOptionValues, VariableOptionKeys, VariableOptionValues);

                break;
            }
        }
        equipments.Add(newOption);
        return newOption;
    }

    public EquipmentOption FindItemOption(int ItemID)
    {
        foreach(EquipmentOption e in equipments)
        {
            if (e.ItemID == ItemID)
                return e;
        }

        return null;
    }

}
