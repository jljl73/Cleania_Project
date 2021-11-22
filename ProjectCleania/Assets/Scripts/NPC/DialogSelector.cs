using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSelector : MonoBehaviour
{
    [SerializeField]
    DialogManager dialogManager;
    [SerializeField]
    Quest[] quests;

    //[System.Serializable]
    //public struct Decorator
    //{
    //    public Quest quest;
    //    public bool IsSuccess()
    //    {
    //        return quest.State == Quest.STATE.Reward;
    //    }

    //    bool isRewarded()
    //    { return quest.State == Quest.STATE.Reward; }
    //    bool isAssigned()
    //    { return quest.State != Quest.STATE.Unassign; }
    //}

    //[SerializeField]
    //Decorator[] decorators;

    public void ShowDialog()
    {
        dialogManager.ShowDialog(GetDialog(), true);
    }

    GameObject GetDialog()
    {
        int i = 0;
        for (;i < quests.Length; ++i)
        {
            if (quests[i].State != Quest.STATE.Reward)
                break;
        }
        return transform.GetChild(i).gameObject;
    }

}