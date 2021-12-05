using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestMiniPanel : MonoBehaviour
{
    QuestManager QM { get => GameManager.Instance.uiManager.GetComponent<QuestManager>(); }

    [SerializeField]
    Transform miniLists;
    
    void Start()
    {
        QM.AddListener_Update(UpdateList);
    }


    public void UpdateList()
    {
        if (miniLists == null) return;

        StringBuilder sb = new StringBuilder();
        int index = 0;

        for (int q = 0; q < QM.Quests.Count; ++q)
        {
            if (QM.Quests[q].State != Quest.STATE.Assign && QM.Quests[q].State != Quest.STATE.Clear)
                continue;

            sb.Clear();
            sb.Append(QM.Quests[q].Name);
            sb.Append("\n");
            miniLists.GetChild(index).gameObject.SetActive(true);
            for (int i = 0; i < QM.Quests[q].QuestNeeds.Length; ++i)
            {
                sb.Append(QM.Quests[q].QuestNeeds[i].Contents);
                sb.Append("\n");
            }
            miniLists.GetChild(index).GetChild(0).GetComponent<Text>().text = sb.ToString();

            if (++index >= miniLists.childCount)
                break;
        }

        for (; index < miniLists.childCount; ++index)
        {
            miniLists.GetChild(index).gameObject.SetActive(false);
        }
    }
}
