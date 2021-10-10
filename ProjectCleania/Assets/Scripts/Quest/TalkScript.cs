using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Script
{
    [TextArea]
    public string ISays;

    public bool QuestLocation1;

    [TextArea]
    public string OtherPlayerSays;

    public bool QuestLocation2;

    public Script(string iSays, string otherPlayerSays, bool questLocation1, bool questLocation2)
    {
        ISays = iSays;
        OtherPlayerSays = otherPlayerSays;
        QuestLocation1 = questLocation1;
        QuestLocation2 = questLocation2;
    }
}

public class TalkScript : MonoBehaviour
{
    public List<Script> scriptList;

    QuestGiver questGiver;

    private void Awake()
    {
        questGiver = GetComponent<QuestGiver>();
    }

    public void Talk(QuestReciever otherPerson)
    {
        foreach (Script script in scriptList)
        {
            // script.ISays;
            if (script.QuestLocation1)
            {
                if (questGiver != null)
                {
                    // 쿼스트 줄 수 있으면 주고, 없으면 안줌
                    questGiver.GiveQuest(otherPerson);
                }
            }
            // script.OtherPlayerSays;
            if (script.QuestLocation2)
            {
                if (questGiver != null)
                    questGiver.GiveQuest(otherPerson);
            }
        }
    }
}
