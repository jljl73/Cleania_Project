using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    UnityEvent addEvent = new UnityEvent();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            Acheive(QuestNeed.TYPE.Monster, 0);
    }

    public void AddEvent(UnityAction action)
    {
        addEvent.AddListener(action);
    }

    public void Add(Quest quest)
    {
        quests.Add(quest);
        quest.Assign();
        addEvent.Invoke();
    }
    
    public void Acheive(QuestNeed.TYPE type, int target)
    {
        for (int i = 0; i < quests.Count; ++i)
        {
            quests[i].Achieve(type, target);
        }
        addEvent.Invoke();
    }
}