using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public DialogManager dialogManager;
    public UIManager uiManager;
    List<GameObject> npcs = new List<GameObject>();

    void Start()
    {
        dialogManager = GameManager.Instance.MainCanvas.GetComponentInChildren<DialogManager>();
        uiManager = GameManager.Instance.uiManager;
    }

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
                    dialogManager.ShowRepairDialog(true, npc.GetComponent<NPC>().npcname);
                    break;
                case NPC.TYPE.Market:
                    dialogManager.ShowMarketDialog(true, npc.GetComponent<NPC>().npcname);
                    break;
                case NPC.TYPE.Enchant:
                    dialogManager.ShowEnchantDialog(true, npc.GetComponent<NPC>().npcname);
                    break;
                case NPC.TYPE.Storage:
                    dialogManager.ShowStorageDialog(true, npc.GetComponent<NPC>().npcname);
                    break;
                case NPC.TYPE.Portal:
                    GameManager.Instance.ChangeScene(npc.GetComponent<NPC>().Value);
                    break;
                case NPC.TYPE.Quest:
                    dialogManager.ShowQuestDialog(true, npc.GetComponent<NPC>().npcname);
                    break;
                case NPC.TYPE.Dungeon:
                    dialogManager.ShowDungeonDialog(true, npc.GetComponent<NPC>().npcname);
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
            uiManager.OffNPCPanels();
            dialogManager.OffDialog();
            npcs.Remove(other.gameObject);
            other.GetComponent<NPC>().ShowName(false);
        }
    }

}
