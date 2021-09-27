using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ItemObject : MonoBehaviour
{
    //public GameObject itemInventory;
    public string ItemCode = "1101001";
    public string ItemName = "TestWeapon1";

    Item _item = new Item();
    EquipmentOption equipmentOption;
    
    private void Start()
    {
        _item.CodeParsing(ItemCode, ItemName);
        equipmentOption = new EquipmentOption(_item.ItemSmallCategory, _item.ItemRank, _item.ItemID, 1);

        GameObject.Find("Others").transform.Find("ItemList").GetComponent<ItemList>().AddOption(equipmentOption);


    }

    public void CopyItem(Item item)
    {
        _item = item.DeepCopy();
    }

    public void PutInventory()
    {
        GameObject generator = GameObject.Find("Others").transform.Find("InventoryItemGenerator").gameObject;
                generator.GetComponent<InventoryItemGenerator>().GenerateItem(_item);

        GameObject.Find("Others").transform.Find("ItemList").GetComponent<ItemList>().AddToInventory(_item);
        Destroy(gameObject);
    }
}
