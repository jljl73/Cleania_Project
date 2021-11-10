using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestSlot : MonoBehaviour, IPointerClickHandler
{
    public Quest quest { get; private set; }

    QuestManager qm;
    void Start()
    {
        qm = GameManager.Instance.uiManager.GetComponent<QuestManager>();
    }
    
    public void Initialize(Quest quest)
    {
        this.quest = quest;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        qm.QuestName.text = quest.Name;
        qm.QuestDetail.text = quest.Content;

        qm.RewardItem.transform.parent.gameObject.SetActive(false);
        qm.RewardClean.transform.parent.gameObject.SetActive(false);
        qm.RewardExp.transform.parent.gameObject.SetActive(false);
        foreach (var r in quest.QuestRewards)
        {
            switch (r.type)
            {
                case QuestReward.TYPE.clean:
                    qm.RewardClean.transform.parent.gameObject.SetActive(true);
                    qm.RewardClean.text = r.value.ToString();
                    break;
                case QuestReward.TYPE.exp:
                    qm.RewardExp.transform.parent.gameObject.SetActive(true);
                    qm.RewardExp.text = r.value.ToString();
                    break;
                    // 아이템 하나인경우만 가능 여러개면 좀 귀찮음...
                case QuestReward.TYPE.item:
                    qm.RewardItem.transform.parent.gameObject.SetActive(true);
                    qm.RewardItem.text = r.value.ToString();
                    break;
            }
        }
        QuestManager.ClickedQuest = quest;
    }

    private void OnDestroy()
    {
        QuestManager.ClickedQuest = null;
        qm.QuestName.text = "-";
        qm.QuestDetail.text = "";
        qm.RewardItem.transform.parent.gameObject.SetActive(false);
        qm.RewardClean.transform.parent.gameObject.SetActive(false);
        qm.RewardExp.transform.parent.gameObject.SetActive(false);
    }
}
