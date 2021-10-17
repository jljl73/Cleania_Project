using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage_World : iSavedData
{
    public struct PositionedItem
    {
        ItemInstance ItemData;
        Vector3 Position;
    }

    List<GameObject> _items;

    public bool Add(ItemInstance item, Vector3 position)
    {
        GameObject newItem = new GameObject(item.Idea.ItemName);
        newItem.transform.position = position;

        return true;
    }

    public bool Add(int itemID, Vector3 position)
    {
        ItemInstance item = ItemInstance.Instantiate(itemID);

        // recycle
        if (item != null)
            return Add(item, position);
        else
            return false;
    }

    public bool Remove(GameObject itemObject)
    {
        return false;
    }




        // SAVE DATA IMPLEMENTATION

    [SerializeField]
    List<PositionedItem> SD_items;

    public void AfterLoad()
    {
        
    }

    public void BeforeSave()
    {
        
    }
}
