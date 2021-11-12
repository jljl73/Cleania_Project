using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogButton : MonoBehaviour
{
    Dialog dialog = null;
    Button button;
    enum Func { NextPage, QuestAssign, QuestReward, NpcPanel, Close };

    Quest quest;
    [SerializeField]
    Func func;
    [SerializeField]
    string value;

    void Start()
    {
        dialog = GetComponentInParent<Dialog>();
        button = GetComponent<Button>();
        AddListener();
    }

    void AddListener()
    {
        switch (func)
        {
            case Func.NextPage:
                button.onClick.AddListener(() => NextPage(int.Parse(value)));
                break;
            case Func.QuestAssign:
                button.onClick.AddListener(QuestAssign);
                break;
            case Func.QuestReward:
                button.onClick.AddListener(QuestGetReward);
                break;
            case Func.NpcPanel:
                button.onClick.AddListener(() => ShowPanel(value));
                break;
            case Func.Close:
                button.onClick.AddListener(CloseDialog);
                break;
        }
    }


    void NextPage(int next)
    {
        dialog.NextPage(next);
    }

    void QuestAssign()
    {
        quest = Resources.Load("ScriptableObject/QuestTable/" + value) as Quest;
        if (quest == null)
        {
            Debug.Log("Quest is null");
            return;
        }
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Assign(quest);
    }

    void QuestGetReward()
    {
        quest = Resources.Load("ScriptableObject/QuestTable/" + value) as Quest;
        if(quest == null)
        {
            Debug.Log("Quest is null");
            return;
        }
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Reward(quest);
    }

    void ShowPanel(string value)
    {
        switch (value)
        {
            case "Repair":
                GameManager.Instance.uiManager.GetComponent<UIManager>().ShowRepairPanel(true);
                break;
            case "Market":
                GameManager.Instance.uiManager.GetComponent<UIManager>().ShowMarketPanel(true);
                break;
            case "Enchant":
                GameManager.Instance.uiManager.GetComponent<UIManager>().ShowEnchantPanel(true);
                break;
            case "Storage":
                GameManager.Instance.uiManager.GetComponent<UIManager>().ShowStoragePanel(true);
                break;
        }
    }

    void CloseDialog()
    {
        if (dialog == null) return;
        dialog.gameObject.SetActive(false);
    }
}
