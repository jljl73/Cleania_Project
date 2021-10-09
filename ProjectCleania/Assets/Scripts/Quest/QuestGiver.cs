using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> questList;

    public void Talk(QuestReciever playerQuestReciever)
    {
        Quest availableQuest = GetAvailableQuest(playerQuestReciever);
        if (availableQuest == null)
            return;

        GiveQuest(playerQuestReciever, availableQuest);
    }

    public void GiveQuest(QuestReciever playerQuestReciever, Quest availableQuest)
    {
        //Quest availableQuest = GetAvailableQuest(playerQuestReciever);
        //if (availableQuest == null)
        //    return;

        playerQuestReciever.GetNewQuest(availableQuest);
    }

    Quest GetAvailableQuest(QuestReciever playerQuestReciever)
    {
        // 플레이어 정보를 받아, 퀘스트가 진행 가능한지 알아본 후, 부여 가능한 퀘스트를 돌려준다.
        foreach (Quest quest in questList)
        {
            if (!playerQuestReciever.CheckIfAvailable(quest))
                break;

            if (playerQuestReciever.CheckIfDone(quest))
                continue;

            return quest;
        }
        return null;
    }
}
