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
        // �÷��̾� ������ �޾�, ����Ʈ�� ���� �������� �˾ƺ� ��, �ο� ������ ����Ʈ�� �����ش�.
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
