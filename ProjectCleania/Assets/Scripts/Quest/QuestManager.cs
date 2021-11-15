using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text;
using TMPro;

public class QuestManager : MonoBehaviour
{
    // 완료한 퀘스트
    List<Quest> clearQuests = new List<Quest>();
    // 수주받은 퀘스트
    [SerializeField]
    List<Quest> assignQuests = new List<Quest>();
    [SerializeField]
    Quest[] quests_All;
        

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

        for(int i = 0; i < quests_All.Length; ++i)
        {
            if (quests_All[i].State == Quest.STATE.Assign || quests_All[i].State == Quest.STATE.Clear)
                Assign(quests_All[i]);
            else if (quests_All[i].State == Quest.STATE.Reward)
                clearQuests.Add(quests_All[i]);
        }

        SetListHeight();
        ExpManager.Initailize(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            
        }
    }

    public void Assign(Quest quest)
    {
        assignQuests.Add(quest);
        quest.Assign();
        AddList(quest);
        GameManager.Instance.soundPlayer.PlaySound("QuestAssign");

        QuestDB.Save(quest);
    }
    
    public void Acheive(QuestNeed.TYPE type, int target)
    {
        for (int i = 0; i < assignQuests.Count; ++i)
        {
            assignQuests[i].Achieve(type, target);
        }
        SetMiniList();
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

    public void Reward(Quest quest)
    {
        quest.GetReward();
        clearQuests.Add(quest);
        DeleteList(quest);

        GameManager.Instance.soundPlayer.PlaySound("QuestReward");

        // 보상받기
        foreach (var q in quest.QuestRewards)
        {
            switch (q.type)
            {
                case QuestReward.TYPE.clean:
                    Debug.Log("크리스탈 위치 설정해야댐");
                    //GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>().AddCrystal(q.value);
                    break;
                case QuestReward.TYPE.exp:
                    ExpManager.Acquire(q.value);
                    break;
                case QuestReward.TYPE.item:
                    ItemInstance itemInstance = ItemInstance.Instantiate(q.value);
                    ItemController_v2 newItem = ItemController_v2.New(itemInstance, GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>());
                    newItem.PutInventory();
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