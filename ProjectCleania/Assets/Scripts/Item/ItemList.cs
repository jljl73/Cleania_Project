using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class ItemList : MonoBehaviour
{
    JsonData jsonData;

    List<Item> items_Inventory = new List<Item>();
    List<Item> items_Field = new List<Item>();

    private void Start()
    {
        //SaveItem();
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
        //List<ItemInfo> itemInfos;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            foreach (Item i in items_Inventory)
                Debug.Log(i.ItemID.ToString() + ", " + i.ItemCode);

            jsonData = JsonMapper.ToJson(items_Inventory);
            File.WriteAllText(Application.dataPath + "/Resources/JsonData/PlayerInventory.json",
                jsonData.ToString());
        }
    }

}
