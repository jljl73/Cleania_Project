using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Text;
using System.Collections.ObjectModel;

[System.Serializable]
public struct QuestReward
{
    public enum TYPE { clean, exp, item };
    public TYPE type;
    public int value;
    public bool isReward;

}

[System.Serializable]
public struct QuestNeed
{
    public enum TYPE { Monster, Item, Trigger };
    public bool IsClear { get { return targetValue <= curValue; } }

    public TYPE type;
    public int target;
    public int targetValue;
    public int curValue;
    public string content;


    public void Achieve()
    {
        if(this.type == TYPE.Monster)
            ++curValue;
        else if(type == TYPE.Trigger)
        {
            targetValue = curValue;
        }
        else
        {
            //curValue = GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_ItemContainer>().GetNumberItem(this.target);

            int sum = 0;

            foreach (var item in SavedData.Instance.Item_Inventory.Items)
            {
                if (item.Key.SO.ID == this.target)  
                {
                    sum += item.Key.Count;
                }
            }

            curValue = sum;
        }

    }

    public string Contents
    {
        get
        {
            if (this.type == TYPE.Monster)
                return String.Format("{0} 처치 ({1}/{2})", Enemy.GetName(target), curValue, targetValue);
            else if (this.type == TYPE.Item)
                return String.Format("{0} 획득 ({1}/{2})", content, curValue, targetValue);
            else
                return String.Format("{0} ({1}/{2})", content, curValue, targetValue);
        }
    }
}

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Object/Quest")]
[System.Serializable]
public class Quest : ScriptableObject
{
    public enum STATE { Unassign, Assign, Clear, Reward };
    public enum CATEGORY { Tutorial, Main, Sub, Sudden };

    [SerializeField]
    int id;
    public int ID { get { return id; } }

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
        if (state == STATE.Reward) return;

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

    public void Reset()
    {
        for (int i = 0; i < questNeeds.Length; ++i)
        {
            questNeeds[i].curValue = 0;
        }
        state = STATE.Unassign;
    }

    public void Load(STATE state, string data)
    {
        string[] datas = data.Split(' ');
        int value;
        for (int i = 0; i < questNeeds.Length; ++i)
        {
            if (int.TryParse(datas[i], out value))
                questNeeds[i].curValue = value;
            else
                Debug.Log("QuestNeed data is Error");
        }
        this.state = state;
    }

    public string Incoding()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(questNeeds[0].curValue);
        for (int i = 1; i < questNeeds.Length; ++i)
        {
            sb.Append(" ");
            sb.Append(questNeeds[i].curValue);
        }

        return sb.ToString();
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

