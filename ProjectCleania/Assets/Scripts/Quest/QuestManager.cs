using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text;
using TMPro;
using System.Drawing;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    List<Quest> quests_All;
    public List<Quest> Quests { get => quests_All; }
        

    // UI
    [SerializeField]
    Sprite spritePlus;
    [SerializeField]
    Sprite spriteMinus;
    public Button[] buttons;
    public GameObject[] details;


    public GameObject TextPrefab;
    public Text QuestName;
    public Text QuestDetail;
    public TextMeshProUGUI RewardExp;
    public TextMeshProUGUI RewardClean;
    public TextMeshProUGUI RewardItem;
    public static Quest ClickedQuest = null;
    int prevIndex = -1;

    UnityEvent UpdateQuest = new UnityEvent();
    public GameObject Effect;



    public void AddListener_Update(UnityAction action)
    {
        UpdateQuest.AddListener(action);
    }


    void Start()
    {
        QuestDB.Instance.SetNickName(SavedData.Instance.CharacterName);
        QuestDB.Instance.Load(quests_All);
        UpdateQuest.Invoke();
        SavedData.Instance.Item_Inventory.Subscribe(Synchronize, Point.Empty);
    }
    

    public void Assign(Quest quest)
    {
        quest.Assign();
        UpdateQuest.Invoke();
        Reward(quest, false);

        GameManager.Instance.soundPlayer.PlaySound(SoundPlayer.TYPE.QuestAssign);
        QuestDB.Instance.Save(quests_All);
    }
    
    public void Acheive(QuestNeed.TYPE type, int target)
    {
        for (int i = 0; i < Quests.Count; ++i)
        {
            if(Quests[i].State == Quest.STATE.Assign || Quests[i].State == Quest.STATE.Clear)
                Quests[i].Achieve(type, target);
        }

        UpdateQuest.Invoke();
        QuestDB.Instance.Save(quests_All);
    }


    void Synchronize(iItemStorage sender, ItemStorage_LocalGrid.SyncOperator oper, Point index)
    {
        if (this == null)
        {
            ((ItemStorage_LocalGrid)sender).QuitSubscribe(Synchronize);
            return;
        }

        Debug.Log("Quest Item Synchronize");
        Acheive(QuestNeed.TYPE.Item, 0);
    }


    public void Reward(Quest quest, bool IsReward)
    {
        if (IsReward)
        {
            quest.GetReward();
            UpdateQuest.Invoke();
            GameManager.Instance.soundPlayer.PlaySound(SoundPlayer.TYPE.QuestReward);

            Instantiate(Effect, GameManager.Instance.player.transform);
        }

        // 보상받기
        foreach (var q in quest.QuestRewards)
        {
            if (q.isReward != IsReward) continue;

            switch (q.type)
            {
                case QuestReward.TYPE.clean:
                    GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>().AddCrystal(q.value, UI_Currency.SourceType.Reward);
                    break;
                case QuestReward.TYPE.exp:
                    ExpManager.Acquire(q.value);
                    break;
                case QuestReward.TYPE.item:
                    SavedData.Instance.Item_Inventory.Add(ItemInstance.Instantiate(q.value));
                    break;
            }
        }
    }

    public void Abandon()
    {
        if (ClickedQuest == null) return;
        if (ClickedQuest.State == Quest.STATE.Reward) return;

        ClickedQuest.Reset();
        UpdateQuest.Invoke();
    }


    #region MINI UI
    //[SerializeField]
    //Transform miniLists;

    //void SetMiniList()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    int q = 0;
    //    if (miniLists == null) return;
    //    for (; q < miniLists.childCount && q < assignQuests.Count; ++q)
    //    {
    //        sb.Clear();
    //        sb.Append(assignQuests[q].Name);
    //        sb.Append("\n");
    //        miniLists.GetChild(q).gameObject.SetActive(true);
    //        for (int i = 0; i < assignQuests[q].QuestNeeds.Length; ++i)
    //        {
    //            sb.Append(assignQuests[q].QuestNeeds[i].Contents);
    //            sb.Append("\n");
    //        }
    //        miniLists.GetChild(q).GetChild(0).GetComponent<Text>().text = sb.ToString();
    //    }

    //    for (; q < miniLists.childCount; ++q)
    //    {
    //        miniLists.GetChild(q).gameObject.SetActive(false);
    //    }
    //}

    #endregion
}