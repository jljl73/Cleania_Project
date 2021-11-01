using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestMiniPanel : MonoBehaviour
{
    bool isOpend = false;
    [SerializeField]
    GameObject buttonOpen;
    [SerializeField]
    GameObject buttonClose;

    public QuestManager questManager;
    public GameObject Toggle;

    void Start()
    {
        questManager.AddEvent(UpdateList);
    }

    public void OnClickedButton()
    {
        OpenQuestList(isOpend);
        isOpend = !isOpend;

        buttonClose.SetActive(!isOpend);
        buttonOpen.SetActive(isOpend);
    }
    
    void OpenQuestList(bool value)
    {
        int i = 2;
        int count = transform.childCount;
        for(; i < count; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }
    
    void UpdateList()
    {
        StringBuilder sb = new StringBuilder();
        for (int q = 0; q < questManager.quests.Count; ++q)
        {
            sb.Clear();
            sb.Append(questManager.quests[q].name);
            sb.Append("\n");
            for (int i = 0; i < questManager.quests[q].QuestNeeds.Length; ++i)
            {
                sb.Append(questManager.quests[q].QuestNeeds[i].Contents);
                sb.Append("\n");
            }
            Toggle.transform.GetChild(q).GetChild(0).GetComponent<Text>().text = sb.ToString();
        }
    }
}
