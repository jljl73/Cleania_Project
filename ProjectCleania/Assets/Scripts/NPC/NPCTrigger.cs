using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public DialogManager dialogManager;
    public UIManager ui;
    List<GameObject> npcs = new List<GameObject>();

    GameObject npc;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            npc = FindCloseNPC();
            if (npc == null) return;

            switch (npc.GetComponent<NPC>().NPCType)
            {
                case NPC.TYPE.Repair:
                    ui.ShowRepairPanel();
                    break;
                case NPC.TYPE.Buy:
                    //ui.ShowBuyPanel();
                    dialogManager.ShowMarketDialog();
                    break;
                case NPC.TYPE.Sell:
                    ui.ShowSellPanel();
                    break;
                case NPC.TYPE.Enchant:
                    ui.ShowEnchantPanel();
                    break;
                case NPC.TYPE.Storage:
                    ui.ShowStoragePanel();
                    break;
            }
        }
    }

    GameObject FindCloseNPC()
    {
        if (npcs.Count == 0) return null;

        GameObject npc = npcs[0];
        float minDist = Vector3.Distance(transform.position, npc.transform.position);

        for (int i = npcs.Count - 1; i > 0; --i)
        {
            float dist = Vector3.Distance(transform.position, npcs[i].transform.position);
            if (dist < minDist)
            {
                npc = npcs[i];
                dist = minDist;
            }
        }

        return npc;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            npcs.Add(other.gameObject);
            other.GetComponent<NPC>().ShowName(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            ui.OffNPCPanels();
            npcs.Remove(other.gameObject);
            other.GetComponent<NPC>().ShowName(false);
        }
    }
}
