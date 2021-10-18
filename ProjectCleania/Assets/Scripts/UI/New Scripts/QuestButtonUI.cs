using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestButtonUI : MonoBehaviour 
{
    public Button[] buttons;
    public GameObject[] details;

    int prevIndex = -1;
    void Start()
    {
        for(int i = buttons.Length-1; i >= 0; --i)
        {
            // ���ٽ��� �״�� ������ ��������
            int temp = i;
            buttons[i].onClick.AddListener(() => ShowDetailQuest(temp));
        }
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

}
