using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogQuest : Dialog
{
    public Quest prevQuest;

    public void ClearQuest(Quest quest)
    {
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Reward(quest);
    }
}
