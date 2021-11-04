using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct QuestReward
{
    public enum TYPE { clean, exp, item };
    public TYPE type;
    public int value;

    public QuestReward(TYPE type, int value)
    {
        this.type = type;
        this.value = value;
    }
}

[System.Serializable]
public struct QuestNeed
{
    public enum TYPE { Monster, Item };
    public bool IsClear { get { return targetValue <= curValue; } }

    public TYPE type;
    public int target;
    public int targetValue;
    public int curValue;

    public QuestNeed(TYPE type, int target, int curValue, int targetValue)
    {
        this.type = type;
        this.target = target;
        this.curValue = curValue;
        this.targetValue = targetValue;
    }

    public void Achieve()
    {
        if(this.type == TYPE.Monster)
            ++curValue;
        else
        {
            curValue = GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>().GetNumberItem(this.target);
        }
    }

    public string Contents
    {
        get
        {
            return String.Format("{0} 처치 ({1}/{2})", target, curValue, targetValue);
        }
    }
}
[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Object/Quest")]
public class Quest : ScriptableObject
{
    public enum STATE { Unassign, Assign, Clear, Reward };
    public enum CATEGORY { Tutorial, Main, Sub, Sudden };

    [SerializeField]
    STATE state;
    public STATE State { get { return state; } }

    [SerializeField]
    CATEGORY category;
    public CATEGORY Catergory { get { return category; } }


    public string Name;
    public string Content;

    [SerializeField]
    QuestReward[] questRewards;
    public QuestReward[] QuestRewards { get { return questRewards; } }

    [SerializeField]
    QuestNeed[] questNeeds;
    public QuestNeed[] QuestNeeds { get { return questNeeds; } }

    public void Assign()
    {
        state = STATE.Assign;
    }

    public void GetReward()
    {
        state = STATE.Reward;
    }

    // 몬스터 잡는거만 체크
    public void Achieve(QuestNeed.TYPE type, int target)
    {
        for (int i = 0; i < questNeeds.Length; ++i)
        {
            if (questNeeds[i].type == type && questNeeds[i].target == target)
            {
                questNeeds[i].Achieve();
            }
            else if (questNeeds[i].type == QuestNeed.TYPE.Item)
                questNeeds[i].Achieve();
        }
        if (IsClear())
            state = STATE.Clear;
    }

    public bool IsClear()
    {
        for (int i = 0; i < questNeeds.Length; ++i)
        {
            if (!questNeeds[i].IsClear)
                return false;
        }
        
        return true;
    }
}
