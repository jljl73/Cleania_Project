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
        public int iButton;
        public int prev;
        public int next;
    }

    [System.Serializable]
    public struct NextPageWithQuest
    {
        public Quest quest;
        public int iButton;
        public int prev;
        public int[] next;
    }

    
    public NextPage[] nextPages;
    public NextPageWithQuest[] nextPagesWithQuest;

    void Start()
    {
        pages = transform.GetChild(transform.childCount - 1);

        for (int i = 0; i < nextPages.Length; ++i)
        {
            NextPage np = nextPages[i];
            pages.GetChild(np.prev).GetChild(1).GetChild(np.iButton).GetComponent<Button>().onClick.AddListener(() => ChangePage(np));
        }

        for (int i = 0; i < nextPagesWithQuest.Length;++i)
        {
            NextPageWithQuest np = nextPagesWithQuest[i];
            pages.GetChild(np.prev).GetChild(1).GetChild(np.iButton).GetComponent<Button>().onClick.AddListener(() => ChangePage(np));
        }
    }

    public void ChangePage(NextPage np)
    {
        pages.GetChild(np.prev).gameObject.SetActive(false);
        pages.GetChild(np.next).gameObject.SetActive(true);
    }

    public void ChangePage(NextPageWithQuest np)
    {
        pages.GetChild(np.prev).gameObject.SetActive(false);
        int nextIdx = (int)np.quest.State;
        pages.GetChild(np.next[nextIdx]).gameObject.SetActive(true);
    }

    public void AssignQuest(Quest quest)
    {
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Assign(quest);
    }

    public void GetQuestReward(Quest quest)
    {
        GameManager.Instance.uiManager.GetComponent<QuestManager>().Reward(quest);
    }

    public void CloseDialog()
    {
        pages.parent.gameObject.SetActive(false);
    }
}
