using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    // 완료한 퀘스트
    List<Quest> clearQuests = new List<Quest>();
    // 수주받은 퀘스트
    [SerializeField]
    List<Quest> quests = new List<Quest>();
    [SerializeField]
    Quest[] quests_All;
        
    UnityEvent addEvent = new UnityEvent();

    // UI
    public Button[] buttons;
    public GameObject[] details;
    public GameObject TextPrefab;
    public Text QuestName;
    public Text QuestDetail;
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
            if (quests_All[i].State == Quest.STATE.Assign)
                quests.Add(quests_All[i]);
            else if (quests_All[i].State == Quest.STATE.Clear)
                clearQuests.Add(quests_All[i]);
        }

        SetListHeight();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Acheive(QuestNeed.TYPE.Monster, 0);
        }
    }

    public void Add(Quest quest)
    {
        quests.Add(quest);
        quest.Assign();
        AddList(quest);
    }
    
    public void Acheive(QuestNeed.TYPE type, int target)
    {
        Debug.Log(type.ToString() + " " + target);
        for (int i = 0; i < quests.Count; ++i)
        {
            quests[i].Achieve(type, target);
            Debug.Log(type.ToString() + " " + target);
        }
        addEvent.Invoke();
    }

    #region UI
    void ShowDetailQuest(int index)
    {
        for (int i = buttons.Length - 1; i >= 0; --i)
        {
            details[i].SetActive(false);
        }
        if (prevIndex == index) return;

        details[index].SetActive(true);
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

    void SetListHeight()
    {
        for (int i = 0; i < details.Length; ++i)
        {
            details[i].GetComponent<RectTransform>().sizeDelta = new Vector2(250, 41 * details[i].transform.childCount);
        }
    }
    #endregion

    #region MINI UI


    #endregion
}