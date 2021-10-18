using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage_World : ItemStorage, iSavedData
{
    public ItemStorage_World()
    {
        _itemObjectPrefab = Resources.Load<GameObject>("Prefab/ItemObject");
    }

    GameObject _itemObjectPrefab;
    Queue<GameObject> _objectPool = new Queue<GameObject>();
    Dictionary<ItemInstance, GameObject> _items = new Dictionary<ItemInstance, GameObject>();


    public override bool Add(ItemInstance item)
    {
        return false;
    }
    public override bool Remove(ItemInstance item)
    {
        return false;
    }

    public bool Add(ItemInstance item, Vector3 position)
    {
        GameObject newItem = new GameObject(item.Info.ItemName);
        newItem.transform.position = position;

        return true;
    }

    public bool Remove(GameObject itemObject)
    {
        return false;
    }




        // SAVE DATA IMPLEMENTATION

    public struct PositionedItem
    {
        ItemInstance ItemData;
        Vector3 Position;
    }

    [SerializeField]
    List<PositionedItem> SD_items;

    void iSavedData.AfterLoad()
    {

    }

    void iSavedData.BeforeSave()
    {

    }
}
