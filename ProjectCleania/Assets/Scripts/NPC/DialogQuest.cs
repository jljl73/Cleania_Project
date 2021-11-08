using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogQuest : Dialog
{
    public Quest prevQuest;

    public void Page0()
    {
        switch (prevQuest.State)
        {
            case Quest.STATE.Unassign:
                NextPage(1);
                break;
            case Quest.STATE.Assign:
                NextPage(9);
                break;
            case Quest.STATE.Clear:
                NextPage(6);
                break;
            case Quest.STATE.Reward:
                NextPage(10);
                break;
        }
    }

    public void ClearQuest(Quest quest)
    {
        foreach (var q in quest.QuestRewards)
        {
            switch (q.type)
            {
                case QuestReward.TYPE.clean:
                    GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>().AddCrystal(q.value);
                    break;
                case QuestReward.TYPE.exp:
                    ExpManager.Acquire(q.value);
                    break;
                case QuestReward.TYPE.item:
                    ItemInstance itemInstance = ItemInstance.Instantiate(q.value);
                    ItemController_v2 newItem = ItemController_v2.New(itemInstance, GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>());
                    newItem.PutInventory();
                    break;
            }
        }
        quest.GetReward();
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Clear(quest);
    }
}
