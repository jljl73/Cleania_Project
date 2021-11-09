using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class ItemStorage_Equipments : ItemStorage<ItemInstance_Equipment.Type>, iSavedData
{
    private ItemInstance_Equipment[] _reference = new ItemInstance_Equipment[(int)ItemInstance_Equipment.Type.EnumTotal];

    public ItemInstance this[ItemInstance_Equipment.Type type]
    {
        get
        {
            if(type != ItemInstance_Equipment.Type.EnumTotal)
                return _reference[(int)type];
            else
                return null;
        }
    }

    public override bool Add(ItemInstance item)
    {
        if (item == null || !(item is ItemInstance_Equipment))
            return false;

        ItemInstance_Equipment equip = (ItemInstance_Equipment)item;

        if (_reference[(int)equip.EquipmentType] != null)
            return false;

        _Add(equip);

        return true;
    }

    public override bool Remove(ItemInstance item)
    {
        if (item == null || !(item is ItemInstance_Equipment))
            return false;

        if (!_items.ContainsKey(item))
            return false;

        _Remove((ItemInstance_Equipment)item);

        return true;
    }
}




public partial class ItemStorage_Equipments
{
    void _Add(ItemInstance_Equipment item)
    {
        if (item.CurrentStorage == null)
            item.CurrentStorage = this;
        else
            Debug.Log("Logic error in ItemStorage_LocalGrid : _Add");

        // reserve grid
        _reference[(int)item.EquipmentType] = item;

        // add
        _items.Add(item, item.EquipmentType);

        // Sync
        OnSynchronize(this, SyncOperator.Add, item.EquipmentType);
    }

    void _Remove(ItemInstance_Equipment item)
    {
        if (item.CurrentStorage == this)
            item.CurrentStorage = null;
        else
            Debug.Log("Logic error in ItemStorage_LocalGrid : _Remove");

        // checkout reserve
        _reference[(int)item.EquipmentType] = null;

        // remove
        _items.Remove(item);

        // Sync
        OnSynchronize(this, SyncOperator.Remove, item.EquipmentType);
    }


    // SAVE DATA IMPLEMENTATION

    [SerializeField]
    List<ItemInstance_Equipment> SD_equipments = new List<ItemInstance_Equipment>();

    void iSavedData.AfterLoad()
    {
        _items.Clear();
        OnSynchronize(this, SyncOperator.Refresh, ItemInstance_Equipment.Type.EnumTotal);

        foreach (var i in SD_equipments)
        {
            ((iSavedData)i).AfterLoad();
            _Add(i);
        }

        // SD_equipment.Clear();
    }

    void iSavedData.BeforeSave()
    {
        SD_equipments.Clear();

        foreach (var i in _items)
        {
            ((iSavedData)i.Key).BeforeSave();

            switch (i.Key.SO.MainCategory)
            {
                case ItemSO.enumMainCategory.Equipment:
                    SD_equipments.Add((ItemInstance_Equipment)i.Key);
                    break;
            }
        }
    }
}
