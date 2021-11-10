using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentManager : MonoBehaviour
{
    ItemController_v2[] controllers;
    Equipable playerEquipable;
    UI_ItemContainer inventoryComp;

    private void Awake()
    {
        controllers = new ItemController_v2[transform.childCount];
    }

    void Start()
    { 
        playerEquipable = GameManager.Instance.PlayerEquipments;
        inventoryComp = GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_ItemContainer>();

        Invoke("LoadItemControllers", 0.2f);
    }

    void LoadItemControllers()
    {
        for (int i = 0; i < controllers.Length; ++i)
        {
            if (controllers[i] != null)
                ItemController_v2.Delete(controllers[i]);

            controllers[i] = null;
        }


        for (int i = 0; i < (int)ItemInstance_Equipment.Type.EnumTotal; ++i)
        {
            if (playerEquipable[(ItemInstance_Equipment.Type)i] != null)
            {
                ItemController_v2 item = ItemController_v2.New(playerEquipable[(ItemInstance_Equipment.Type)i], inventoryComp.GetComponent<Storage>());

                ItemInstance_Equipment equipment = (ItemInstance_Equipment)item.itemInstance;
                int ct = (int)equipment.EquipmentType;

                item.MoveTo(transform.GetChild(ct).position);
                controllers[ct] = item;
                item.wearing = true;
            }
        }
    }


    public void Equip(ItemController_v2 item)
    {
        ItemInstance_Equipment equipment = (ItemInstance_Equipment)item.itemInstance;
        int ct = (int)equipment.EquipmentType;

        if (ct<0 || ct >= controllers.Length) return;
        else if (item.Equals(controllers[ct]))
        {
            Unequip(ct);
            return;
        }

        Unequip(ct);
        item.PullInventory();
        item.MoveTo(transform.GetChild(ct).position);
        controllers[ct] = item;
        item.wearing = true;

        //<Modified>
        playerEquipable.Equip((ItemInstance_Equipment)item.itemInstance);
        //</Modified>
    }

    void Unequip(int ct)
    {
        if (controllers[ct] == null) return;

        controllers[ct].wearing = false;
        controllers[ct].PutInventory();
        //<Modified>
        playerEquipable.Unequip(((ItemInstance_Equipment)controllers[ct].itemInstance).EquipmentType);
        //</Modified>
        controllers[ct] = null;
    }
}