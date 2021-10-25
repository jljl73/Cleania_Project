using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        GameManager.Instance.npcManager = this;
    }

    public void Dosmth(GameObject item)
    {
        switch (curNPC)
        {
            case NPC.TYPE.None:
                break;
            case NPC.TYPE.Repair:
                Repair(item); break;
            case NPC.TYPE.Market:
                break;
            case NPC.TYPE.Enchant:
                break;
            case NPC.TYPE.Storage:
                break;
        }
    }

    void Repair(GameObject item)
    {
        repair.SelectItem(item);
    }

    void Sell(GameObject item)
    {
        market.SelectItem(item);
    }

    void Enchant(GameObject item)
    {
        enchant.SelectItem(item);
    }

    void Store(GameObject item)
    {
        
    }

}
