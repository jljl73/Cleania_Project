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
    Storage storage;
    [SerializeField]
    Storage inventory;
    [SerializeField]
    public EquipmentManager equpiments;

    void Awake()
    {
        GameManager.Instance.npcManager = this;
    }

    public void Dosmth(ItemController_v2 item)
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

    void Repair(ItemController_v2 item)
    {
        repair.SelectItem(item);
    }

    void Sell(ItemController_v2 item)
    {
        market.SellItem(item);
    }

    void Enchant(ItemController_v2 item)
    {
        enchant.SelectItem(item);
    }

    void Store(ItemController_v2 item)
    {
        
    }

    void Equip(ItemController_v2 item)
    {
        equpiments.Equip(item);
    }
    
}
