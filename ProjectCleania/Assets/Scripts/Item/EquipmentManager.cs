using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    ItemController_v2[] slots;
    Equipable playerEquipable;

    void Start()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            slots[i] = null;
        }

        playerEquipable = GameManager.Instance.PlayerEquipments;

        Invoke("LoadItemControllers", 0.2f);
    }

    void LoadItemControllers()
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i] != null)
                ItemController_v2.Delete(slots[i]);

            slots[i] = null;
        }


        for (int i = 0; i < (int)ItemInstance_Equipment.Type.EnumTotal; ++i)
        {
            if (playerEquipable[(ItemInstance_Equipment.Type)i] != null)
            {
                ItemController_v2 item = ItemController_v2.New(playerEquipable[(ItemInstance_Equipment.Type)i]);
                item.transform.SetParent(GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>().ItemContollerParent.transform);

                ItemSO.enumSubCategory category = item.itemInstance.SO.SubCategory;
                int ct = GetIndex(category);

                item.MoveTo(transform.GetChild(ct).position);
                slots[ct] = item;
            }
        }
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

        //<Modified>
        playerEquipable.Equip((ItemInstance_Equipment)item.itemInstance);
        //</Modified>
    }

    void Unequip(int ct)
    {
        if (slots[ct] == null) return;

        slots[ct].PutInventory();
        //<Modified>
        playerEquipable.Unequip(((ItemInstance_Equipment)slots[ct].itemInstance).EquipmentType);
        //</Modified>
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