using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuestCompensation
{
    public float CleanCompensation;
    public float XpCompensation;
    public List<Item> ItemCompensation;

    public QuestCompensation (float clean, float xp, List<Item> itemList)
    {
        CleanCompensation = clean;
        XpCompensation = xp;
        ItemCompensation = itemList;
    }
}

[System.Serializable]
public struct QuestMission
{
    public int EnemyCode;
    public int RequiredCount;
    int currentCount;

    public QuestMission (int enemyCode, int requiredCount)
    {
        EnemyCode = enemyCode;
        RequiredCount = requiredCount;
        currentCount = 0;
    }

    public void Feed(int enemyCode)
    {
        if (EnemyCode == enemyCode)
            currentCount++;
    }

    public bool IsCompleted()
    {
        return currentCount == RequiredCount;
    }
}

public class Quest : MonoBehaviour
{
    public int QuestID;
    // public int QuestID { get { return questID; } set { questID = value; } }

    public int NeededLevel;
    // ���� ����: ���� �ذ��ؾ� �ϴ� ����Ʈ��?

    public QuestCompensation Compensation;
    public List<QuestMission> missionList;

    bool isFinished = false;
    public bool IsFinished { get { return isFinished; } }
    public void Feed(int EnemyCode)
    {
        // ��� �̼��� ���� ���� ���� ������ �̼ǿ��� Feed
        foreach (QuestMission mission in missionList)
        {
            mission.Feed(EnemyCode);
        }

        // �ϳ��� �Ϸ� �ȵ� �� ������ �̿Ϸ�!
        bool tempIsFinished = true;
        foreach (QuestMission mission in missionList)
        {
            if (!mission.IsCompleted())
                tempIsFinished = false;
        }

        if (tempIsFinished)
            isFinished = true;

        return;
    }

    public void CompensateTo(QuestReciever QuestReciever)
    {
        QuestReciever.GetReward(Compensation);
    }

    public bool CanBeGivenTo(QuestReciever QuestReciever)
    {
        // �÷��̾��� ������ Ȯ�� �� �־��� �� �ִ��� Ȯ��.
        return true;
    }
}
