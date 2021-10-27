using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCManager : MonoBehaviour
{
    public NPC.TYPE curNPC;
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
    GameObject equpiments;

    void Awake()
    {
        GameManager.Instance.npcManager = this;
    }

    public void Dosmth(GameObject item)
    {
        switch (curNPC)
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
        }
    }

    void Repair(GameObject item)
    {
        repair.SelectItem(item);
    }

    void Sell(GameObject item)
    {
        market.SellItem(item);
    }

    void Enchant(GameObject item)
    {
        enchant.SelectItem(item);
    }

    void Store(GameObject item)
    {
        
    }

    void Equip(GameObject item)
    {

        ItemSO.enumSubCategory eCat = item.GetComponent<ItemController_v2>().itemInstance.SO.SubCategory;
        int ct = 1;
        foreach(ItemSO.enumSubCategory c in Enum.GetValues(typeof(ItemSO.enumSubCategory)))
        {
            if (eCat == c)
                break;
            ++ct;
        }

        if (ct < equpiments.transform.childCount)
            item.transform.position = equpiments.transform.GetChild(ct).position;


    }
}
