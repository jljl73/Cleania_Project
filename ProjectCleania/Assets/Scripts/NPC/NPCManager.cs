using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCManager : MonoBehaviour
{
    [SerializeField]
    NPCMarket market;
    [SerializeField]
    NPCRepair repair;
    [SerializeField]
    NPCEnchant enchant;
    [SerializeField]
    UI_ItemContainer storage;
    [SerializeField]
    UI_ItemContainer inventory;
    [SerializeField]
    UI_ItemContainer equipments;

    void Awake()
    {
        GameManager.Instance.npcManager = this;
    }

    public void Dosmth(UI_ItemController item)
    {
        switch (GameManager.Instance.uiManager.GetCurrentNPC())
        {
            case NPC.TYPE.None:
                Equip(item);
                break;
            case NPC.TYPE.Repair:
                Repair(item);
                break;
            case NPC.TYPE.Market:
                Sell(item);
                break;
            case NPC.TYPE.Enchant:
                Enchant(item);
                break;
            case NPC.TYPE.Storage:
                Store(item);
                break;
            case NPC.TYPE.Quest:
                break;
        }
    }

    void Repair(UI_ItemController item)
    {
        repair.SelectItem(item);
    }

    void Sell(UI_ItemController item)
    {
        market.SellItem(item);
    }

    void Enchant(UI_ItemController item)
    {
        enchant.SelectItem(item);
    }

    void Store(UI_ItemController item)
    {
        item.MoveToStorage();
    }

    void Equip(UI_ItemController item)
    {
        ItemInstance_Equipment equip = (ItemInstance_Equipment)item.itemInstance;

        inventory.ImmigrateTo(item, equipments, (int)equip.EquipmentType);
    }
    
}
