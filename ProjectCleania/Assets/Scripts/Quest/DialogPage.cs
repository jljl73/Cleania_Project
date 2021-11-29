using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPage : MonoBehaviour
{
    Transform pages;

    [System.Serializable]
    public struct NextPage
    {
        public int cur;
        public int next;
        public int iButton;
    }

    [System.Serializable]
    public struct NextPageWithQuest
    {
        public Quest quest;
        public int cur;
        public int[] next;
        public int iButton;
    }

    [System.Serializable]
    public struct st_CloseDialog
    {
        public int cur;
        public int iButton;
    }

    [System.Serializable]
    public struct st_AssignQuest
    {
        public int cur;
        public Quest quest;
        public int iButton;
    }

    [System.Serializable]
    public struct st_ClearQuest
    {
        public int cur;
        public Quest quest;
        public int iButton;
    }

    [System.Serializable]
    public struct st_ShowPanel
    {
        public int cur;
        public int iButton;
        public NPC.TYPE type;
    }


    public NextPage[] nextPages;
    public NextPageWithQuest[] nextPagesWithQuest;
    public st_CloseDialog[] st_CloseDialogs;
    public st_AssignQuest[] st_AssignQuests;
    public st_ClearQuest[] st_ClearQuests;
    public st_ShowPanel m_st_ShowPanel;
    
    void Awake()
    {
        pages = transform.GetChild(transform.childCount - 1);

        for (int i = 0; i < nextPages.Length; ++i)
        {
            NextPage np = nextPages[i];
            pages.GetChild(np.cur).GetChild(1).GetChild(np.iButton).GetComponent<Button>().onClick.AddListener(() => ChangePage(np));
        }

        for (int i = 0; i < nextPagesWithQuest.Length;++i)
        {
            NextPageWithQuest np = nextPagesWithQuest[i];
            pages.GetChild(np.cur).GetChild(1).GetChild(np.iButton).GetComponent<Button>().onClick.AddListener(() => ChangePage(np));
        }

        for (int i = 0; i < st_CloseDialogs.Length; ++i)
        {
            st_CloseDialog np = st_CloseDialogs[i];
            pages.GetChild(np.cur).GetChild(1).GetChild(np.iButton).GetComponent<Button>().onClick.AddListener(CloseDialog);
        }

        for (int i = 0; i < st_AssignQuests.Length; ++i)
        {
            st_AssignQuest np = st_AssignQuests[i];
            pages.GetChild(np.cur).GetChild(1).GetChild(np.iButton).GetComponent<Button>().onClick.AddListener(() => AssignQuest(np.quest));
        }

        for (int i = 0; i < st_ClearQuests.Length; ++i)
        {
            st_ClearQuest np = st_ClearQuests[i];
            pages.GetChild(np.cur).GetChild(1).GetChild(np.iButton).GetComponent<Button>().onClick.AddListener(() => GetQuestReward(np.quest));
        }

       pages.GetChild(m_st_ShowPanel.cur).GetChild(1).GetChild(m_st_ShowPanel.iButton).GetComponent<Button>().onClick.AddListener(() => ShowPanel(m_st_ShowPanel.type));
    }

    public void ChangePage(NextPage np)
    {
        pages.GetChild(np.cur).gameObject.SetActive(false);
        pages.GetChild(np.next).gameObject.SetActive(true);
    }

    public void ChangePage(NextPageWithQuest np)
    {
        pages.GetChild(np.cur).gameObject.SetActive(false);
        int nextIdx = (int)np.quest.State;
        if(nextIdx > -1)
            pages.GetChild(np.next[nextIdx]).gameObject.SetActive(true);
    }

    public void AssignQuest(Quest quest)
    {
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Assign(quest);
        CloseDialog();
    }

    public void GetQuestReward(Quest quest)
    {
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Reward(quest, true);
        CloseDialog();
    }

    public void ShowPanel(NPC.TYPE type)
    {
        switch (type)
        {
            case NPC.TYPE.Repair:
                GameManager.Instance.uiManager.ShowRepairPanel(true);
                break;
            case NPC.TYPE.Market:
                GameManager.Instance.uiManager.ShowMarketPanel(true);
                break;
            case NPC.TYPE.Enchant:
                GameManager.Instance.uiManager.ShowEnchantPanel(true);
                break;
            case NPC.TYPE.Storage:
                GameManager.Instance.uiManager.ShowStoragePanel(true);
                break;
        }
        CloseDialog();
    }

    public void CloseDialog()
    {
        pages.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        pages.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < pages.childCount; ++i)
        {
            pages.GetChild(i).gameObject.SetActive(false);
        }
    }
}
