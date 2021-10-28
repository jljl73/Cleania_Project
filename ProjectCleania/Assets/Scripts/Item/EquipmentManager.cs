using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] slots;

    void Start()
    {
        for (int i = 0; i < slots.Length; ++i)
            slots[i] = null;
    }

    public void Equip(GameObject item)
    {
        ItemSO.enumSubCategory category = item.GetComponent<ItemController_v2>().itemInstance.SO.SubCategory;
        int ct = GetIndex(category);

        if (ct == 0) return;
        else if (item.Equals(slots[ct]))
        {
            Unequip(ct);
            return;
        }

        Unequip(ct);
        item.GetComponent<ItemController_v2>().PullInventory();
        item.GetComponent<ItemController_v2>().MoveTo(transform.GetChild(ct).position);
        slots[ct] = item;
    }

    void Unequip(int ct)
    {
        if (slots[ct] == null) return;

        slots[ct].GetComponent<ItemController_v2>().PutInventory();
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