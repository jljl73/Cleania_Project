using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReciever : MonoBehaviour
{
    List<Quest> currentQuestList;
    List<Quest> finisheQuestdList;

    private void Awake()
    {
        currentQuestList = new List<Quest>();
        finisheQuestdList = new List<Quest>();
    }
    public void Collected(int enemyCode)
    {
        // ����Ʈ�� ���� enemyCode�� �ش��ϴ� ���� ������ �ִ��� Ȯ��.
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            currentQuestList[i].Feed(enemyCode);

            // ����Ʈ �Ϸ� ������ ������ ����Ʈ���� ����, �Ϸ� ����Ʈ�� �߰�
            if (currentQuestList[i].IsFinished)
            {
                finisheQuestdList.Add(currentQuestList[i]);
                currentQuestList.RemoveAt(i);
            }
        }
    }

    public Quest GiveBack(int questID, Quest finishedQuest)
    {
        for (int i = 0; i < finisheQuestdList.Count; i++)
        {
            if (finisheQuestdList[i].QuestID == questID)
            {
                Quest questToReturn = finisheQuestdList[i];
                finisheQuestdList.RemoveAt(i);
                return questToReturn;
            }
        }
        return null;
    }

    public bool CheckIfDone(Quest npcQuest)
    {
        foreach (Quest quest in finisheQuestdList)
        {
            if (quest.QuestID == npcQuest.QuestID)
                return true;
        }

        return false;
    }

    public bool CheckIfAvailable(Quest npcQuest)
    {
        return npcQuest.CanBeGivenTo(this);
    }

    public void GetNewQuest(Quest npcQuest)
    {
        currentQuestList.Add(npcQuest);
    }

    public void GetReward(QuestCompensation questCompensation)
    {
        // ���� ���� �÷��̾�� ����
    }

    
}
