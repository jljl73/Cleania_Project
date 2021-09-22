using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    //public GameObject itemInventory;
    public string ItemCode = "1101001";
    public string ItemName = "TestWeapon1";
    Item _item = new Item();

    private void Start()
    {
        _item.CodeParsing(ItemCode, ItemName);
        //_item = transform.parent.GetComponent<Item>();
    }

    public void CopyItem(Item item)
    {
        _item = item.DeepCopy();
    }

    public void PutInventory()
    {
        GameObject generator = GameObject.Find("Others").transform.Find("InventoryItemGenerator").gameObject;
        generator.GetComponent<InventoryItemGenerator>().GenerateItem(_item);
        GameObject.Find("ItemList").GetComponent<ItemList>().AddToInventory(_item);
        Destroy(gameObject);
    }
}
