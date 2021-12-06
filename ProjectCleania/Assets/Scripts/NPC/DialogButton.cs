using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogButton : MonoBehaviour
{
    Dialog dialog = null;
    Button button;
    public enum TYPE { NextPage, NextPageWithQuest, QuestAssign, QuestReward, NpcPanel, Close };

    Quest quest;
    [SerializeField]
    TYPE type;
    [SerializeField]
    string value;

    
    public void Initialize(TYPE type, Quest quest, string value, Dialog dialog)
    {
        this.type = type;
        this.value = value;
        this.quest = quest;
        this.dialog = dialog;
        AddListener();
    }

    void AddListener()
    {
        if (button == null) button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        switch (type)
        {
            case TYPE.NextPage:
                button.onClick.AddListener(NextPage);
                break;
            case TYPE.NextPageWithQuest:
                button.onClick.AddListener(NextPageWithQuest);
                break;
            case TYPE.QuestAssign:
                button.onClick.AddListener(QuestAssign);
                button.onClick.AddListener(CloseDialog);
                break;
            case TYPE.QuestReward:
                button.onClick.AddListener(QuestGetReward);
                button.onClick.AddListener(CloseDialog);
                break;
            case TYPE.NpcPanel:
                button.onClick.AddListener(ShowPanel);
                button.onClick.AddListener(CloseDialog);
                break;
            case TYPE.Close:
                button.onClick.AddListener(CloseDialog);
                break;
        }
    }

    void NextPage()
    {
        int next = int.Parse(value);
        dialog.ChangePage(next);
    }

    void NextPageWithQuest()
    {
        string[] values = value.Split(' ');
        int next = int.Parse(values[(int)quest.State]);
        dialog.ChangePage(next);
    }

    void QuestAssign()
    {
        if (quest == null)
        {
            Debug.Log("Quest is null");
            return;
        }
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Assign(quest);
    }

    void QuestGetReward()
    {
        if (quest == null)
        {
            Debug.Log("Quest is null");
            return;
        }
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Reward(quest, true);
    }

    void ShowPanel()
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
        //GameManager.Instance.dialogManager.ShowDialog(dialog.gameObject, false);
        GameManager.Instance.dialogManager.OffDialog();
        //dialog.gameObject.SetActive(false);
    }
}
