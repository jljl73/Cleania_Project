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
        // 리스트를 돌며 enemyCode에 해당하는 수집 조건이 있는지 확인.
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            currentQuestList[i].Feed(enemyCode);

            // 퀘스트 완료 됬으면 진행중 퀘스트에서 제거, 완료 퀘스트에 추가
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
        // 받은 상을 플레이어에게 적용
    }

    
}
