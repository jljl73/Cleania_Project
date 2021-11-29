using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgressChecker : MonoBehaviour
{
    DialogManager dialogManager;
    [SerializeField]
    Quest[] quests;


    void Start()
    {
        dialogManager = transform.parent.GetComponent<DialogManager>();
    }

    public void ShowDialog()
    {
        int i = 0;
        for (; i < quests.Length; ++i)
        {
            if (quests[i].State != Quest.STATE.Reward)
                break;
        }
        transform.GetChild(i).GetComponent<DialogSelector>().ShowDialog();
    }

    public int ExistQuest()
    {
        int i = 0;
        for (; i < quests.Length; ++i)
        {
            if (quests[i].State != Quest.STATE.Reward)
                break;
        }
        Quest curQuest = transform.GetChild(i).GetComponent<DialogSelector>().quest;
        if (curQuest == null)
            return -1;
        
        return (int)curQuest.State;
    }
        
}
