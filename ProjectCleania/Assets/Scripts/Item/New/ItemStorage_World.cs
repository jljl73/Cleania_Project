using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage_World : iSavedData
{
    public struct PositionedItem
    {
        ItemData ItemData;
        Vector3 Position;
    }

    List<GameObject> _items;

    public bool Add(ItemData item, Vector3 position)
    {
        GameObject newItem = new GameObject(item.Idea.ItemName);
        newItem.transform.position = position;

        return true;
    }

    public bool Add(int itemID, Vector3 position)
    {
        ItemData item = ItemData.New(itemID);

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
