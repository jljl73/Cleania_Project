using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestSlot : MonoBehaviour, IPointerClickHandler
{
    Quest quest;
    public void Initialize(Quest quest)
    {
        this.quest = quest;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.uiManager.GetComponent<QuestManager>().QuestName.text = quest.Name;
        GameManager.Instance.uiManager.GetComponent<QuestManager>().QuestDetail.text = quest.Content;
    }
}
