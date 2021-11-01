using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    public QuestManager questManager;
    public Button[] buttons;
    public GameObject[] details;

    int prevIndex = -1;
    void Start()
    {
        for (int i = buttons.Length - 1; i >= 0; --i)
        {
            // 람다식은 그대로 넣으면 문제생김
            int temp = i;
            buttons[i].onClick.AddListener(() => ShowDetailQuest(temp));
        }
        questManager.AddEvent(UpdateList);
    }

    void ShowDetailQuest(int index)
    {
        for (int i = buttons.Length - 1; i >= 0; --i)
        {
            details[i].SetActive(false);
        }
        if (prevIndex == index) return;

        details[index].SetActive(true);
    }

    void UpdateList()
    {
        Quest q = questManager.quests[0];
        details[(int)q.Catergory].transform.GetChild(0).GetComponent<Text>().text = q.Name;
        //for (int i = 0; i < questManager.quests.Count; ++i)
        //{
        //    Quest q = questManager.quests[i];
        //    details[(int)q.Catergory].transform.GetChild(0).GetComponent<Text>().text = q.Name;
        //}
    }

}
