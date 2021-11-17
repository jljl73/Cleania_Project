using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogQuest : Dialog
{
    DialogManager dialogManager;
    public Quest prevQuest;
    Quest[] quests;
    GameObject[] dialogs;

    void ShowDialog()
    {
        for(int i = 0;i < quests.Length;++i)
        {
            if (quests[i].State == Quest.STATE.Reward)
                continue;
            else
                dialogManager.ShowDialog(dialogs[i], true);
        }
    }
}