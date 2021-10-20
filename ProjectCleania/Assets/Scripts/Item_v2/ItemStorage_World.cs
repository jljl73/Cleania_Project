using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStorage_World : ItemStorage, iSavedData
{
    public ItemStorage_World()
    {
        
    }

    [System.NonSerialized]
    public GameObject ItemObjectPrefab;
    Queue<GameObject> _objectPool = new Queue<GameObject>();
    Dictionary<ItemInstance, GameObject> _items = new Dictionary<ItemInstance, GameObject>();
    public Dictionary<ItemInstance, GameObject> Items
    { get => new Dictionary<ItemInstance, GameObject>(_items); }


    public override bool Add(ItemInstance item)
    {
        GameObject player = GameManager.Instance.SinglePlayer;
        _Add(item, player.transform.position, player.transform.rotation);

        return true;
    }
    public bool Add(ItemInstance item, Vector3 position)
    {
        _Add(item, position, Quaternion.identity);

        return true;
    }
    void _Add(ItemInstance item, Vector3 position, Quaternion rotation)
    {
        GameObject newObject;

        if(_objectPool.Count > 0)
        {
            newObject = _objectPool.Dequeue();
            newObject.transform.position = position;
            newObject.transform.rotation = rotation;
            newObject.SetActive(true);
        }
        else
        {
            newObject = GameObject.Instantiate(ItemObjectPrefab, position, rotation);
        }

        ItemObject_v2 container = newObject.GetComponent<ItemObject_v2>();
        container.Parent = this;
        container.ItemData = item;

        _items.Add(item, newObject);
    }


    public override bool Remove(ItemInstance item)
    {
        if (!_items.ContainsKey(item))
            return false;

        _Remove(item);
        return true;
    }
    public bool Remove(GameObject itemObject)
    {
        if (!_items.ContainsValue(itemObject))
            return false;

        _Remove(itemObject.GetComponent<ItemObject_v2>().ItemData);
        return true;
    }
    void _Remove(ItemInstance item)
    {
        GameObject removingObject = _items[item];
        _items.Remove(item);

        removingObject.SetActive(false);
        removingObject.GetComponent<ItemObject_v2>().ItemData = null;
        _objectPool.Enqueue(removingObject);
    }




        // SAVE DATA IMPLEMENTATION

    [System.Serializable]
    public struct Positioned<T>
    {
        public Positioned(T item, Vector3 position)
        {
            ItemData = item;
            Position = position;
        }

        [SerializeField]
        public T ItemData;
        [SerializeField]
        public Vector3 Position;
    }

    [SerializeField]
    List<Positioned<ItemInstance_Equipment>> SD_equipments = new List<Positioned<ItemInstance_Equipment>>();
    [SerializeField]
    List<Positioned<ItemInstance_Etc>> SD_etcs = new List<Positioned<ItemInstance_Etc>>();

    void iSavedData.AfterLoad()
    {
        _items.Clear();

        foreach (Positioned<ItemInstance_Etc> i in SD_etcs)
        {
            _Add(i.ItemData, i.Position, Quaternion.identity);
        }
        foreach (Positioned<ItemInstance_Equipment> i in SD_equipments)
        {
            _Add(i.ItemData, i.Position, Quaternion.identity);
        }

        // SD_etc.Clear();
        // SD_equipment.Clear();
    }

    void iSavedData.BeforeSave()
    {
        SD_etcs.Clear();
        SD_equipments.Clear();

        foreach (var i in _items)
        {
            switch (i.Key.Info.MainCategory)
            {
                case ItemSO.enumMainCategory.Equipment:
                    SD_equipments.Add(new Positioned<ItemInstance_Equipment>((ItemInstance_Equipment)i.Key, i.Value.transform.position));
                    break;
                default:
                    SD_etcs.Add(new Positioned<ItemInstance_Etc>((ItemInstance_Etc)i.Key, i.Value.transform.position));
                    break;
            }
        }
    }
}
