using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    ItemController_v2[] slots;

    void Start()
    {
        for (int i = 0; i < slots.Length; ++i)
            slots[i] = null;
    }

    public void Equip(ItemController_v2 item)
    {
        ItemSO.enumSubCategory category = item.itemInstance.SO.SubCategory;
        int ct = GetIndex(category);

        if (ct == 0) return;
        else if (item.Equals(slots[ct]))
        {
            Unequip(ct);
            return;
        }

        Unequip(ct);
        item.PullInventory();
        item.MoveTo(transform.GetChild(ct).position);
        slots[ct] = item;
    }

    void Unequip(int ct)
    {
        if (slots[ct] == null) return;

        slots[ct].PutInventory();
        slots[ct] = null;
    }

    int GetIndex(ItemSO.enumSubCategory category)
    {
        var list = Enum.GetValues(typeof(ItemSO.enumSubCategory));
        int index = 1;
        foreach(ItemSO.enumSubCategory e in list)
        {
            if (e == category)
                return index;
            ++index;
        }
        return 0;
    }
}