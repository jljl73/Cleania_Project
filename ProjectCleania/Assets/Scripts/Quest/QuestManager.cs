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
    // �Ϸ��� ����Ʈ
    List<Quest> clearQuests = new List<Quest>();
    // ���ֹ��� ����Ʈ
    [SerializeField]
    List<Quest> assignQuests = new List<Quest>();
    [SerializeField]
    List<Quest> quests_All;
        

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


    void Start()
    {
        for (int i = buttons.Length - 1; i >= 0; --i)
        {
            int temp = i;
            buttons[i].onClick.AddListener(() => ShowDetailQuest(temp));
        }

        QuestDB.Instance.SetNickName(SavedData.Instance.characterName);
        QuestDB.Instance.Load(quests_All);

        for(int i = 0; i < quests_All.Count; ++i)
        {
            if (quests_All[i].State == Quest.STATE.Assign || quests_All[i].State == Quest.STATE.Clear)
                AddList(quests_All[i]);
            else if (quests_All[i].State == Quest.STATE.Reward)
                clearQuests.Add(quests_All[i]);
        }

        //QuestDB.Instance.Load();
        SetListHeight();
        SavedData.Instance.Item_Inventory.Subscribe(Synchronize, Point.Empty);
    }
    

    public void Assign(Quest quest)
    {
        quest.Assign();
        AddList(quest);
        Reward(quest, false);

        GameManager.Instance.soundPlayer.PlaySound(SoundPlayer.TYPE.QuestAssign);

        QuestDB.Instance.Save(quests_All);
    }
    
    public void Acheive(QuestNeed.TYPE type, int target)
    {
        for (int i = 0; i < assignQuests.Count; ++i)
        {
            assignQuests[i].Achieve(type, target);
        }
        QuestDB.Instance.Save(quests_All);
        SetMiniList();
    }


    void Synchronize(iItemStorage sender, ItemStorage_LocalGrid.SyncOperator oper, Point index)
    {
        Debug.Log("Quest Item Synchronize");
        Acheive(QuestNeed.TYPE.Item, 0);
    }


    #region UI
    void ShowDetailQuest(int index)
    {
        for (int i = buttons.Length - 1; i >= 0; --i)
        {
            buttons[i].GetComponent<Image>().sprite = spritePlus;
            details[i].SetActive(false);
        }
        if (prevIndex == index)
        {
            prevIndex = -1;
            return;
        }

        details[index].SetActive(true);
        buttons[index].GetComponent<Image>().sprite = spriteMinus;
        prevIndex = index;
        SetListHeight();
    }

    void AddList(Quest quest)
    {
        int index = (int)quest.Catergory;
        GameObject newText = Instantiate(TextPrefab, details[index].transform);
        newText.GetComponent<Text>().text = quest.Name;
        newText.GetComponent<QuestSlot>().Initialize(quest);
        assignQuests.Add(quest);
        SetListHeight();
    }

    void DeleteList(Quest quest)
    {
        assignQuests.Remove(quest);
        int index = (int)quest.Catergory;
        for (int i = 0; i < details[index].transform.childCount; ++i)
        {
            if (details[index].transform.GetChild(i).GetComponent<QuestSlot>().quest == quest)
            {
                Destroy(details[index].transform.GetChild(i).gameObject);
                break;
            }
        }
        QuestDB.Instance.Save(quests_All);
        SetListHeight();
    }

    void SetListHeight()
    {
        for (int i = 0; i < details.Length; ++i)
        {
            details[i].GetComponent<RectTransform>().sizeDelta = new Vector2(250, 41 * details[i].transform.childCount);
        }
        SetMiniList();
    }

    public void Reward(Quest quest, bool IsReward)
    {
        quest.GetReward();
        clearQuests.Add(quest);
        DeleteList(quest);

        GameManager.Instance.soundPlayer.PlaySound(SoundPlayer.TYPE.QuestReward);

        // ����ޱ�
        foreach (var q in quest.QuestRewards)
        {
            if (q.isReward != IsReward) continue;

            switch (q.type)
            {
                case QuestReward.TYPE.clean:
                    Debug.Log("ũ����Ż ��ġ �����ؾߴ�");
                    //GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>().AddCrystal(q.value);
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
        DeleteList(ClickedQuest);
        ClickedQuest.Reset();
    }

    #endregion

    #region MINI UI
    [SerializeField]
    Transform miniLists;

    void SetMiniList()
    {
        StringBuilder sb = new StringBuilder();
        int q = 0;
        for(; q < miniLists.childCount && q < assignQuests.Count; ++q)
        {
            sb.Clear();
            sb.Append(assignQuests[q].Name);
            sb.Append("\n");
            miniLists.GetChild(q).gameObject.SetActive(true);
            for (int i = 0; i < assignQuests[q].QuestNeeds.Length; ++i)
            {
                sb.Append(assignQuests[q].QuestNeeds[i].Contents);
                sb.Append("\n");
            }
            miniLists.GetChild(q).GetChild(0).GetComponent<Text>().text = sb.ToString();
        }

        for(; q < miniLists.childCount; ++q)
        {
            miniLists.GetChild(q).gameObject.SetActive(false);
        }
    }

    #endregion
}