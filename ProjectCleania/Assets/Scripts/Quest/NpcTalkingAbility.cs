using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Script
{
    [TextArea]
    public string ISays;

    public bool IsQuestLocation1;
    //public List<Script> QuestResponseScripts1;

    [TextArea]
    public string OtherPlayerSays;

    public bool IsQuestLocation2;
    //public List<Script> QuestResponseScripts2;

    public Script(string iSays, string otherPlayerSays, bool isQuestLocation1, bool isQuestLocation2)
    {
        ISays = iSays;
        OtherPlayerSays = otherPlayerSays;

        IsQuestLocation1 = isQuestLocation1;
        //QuestResponseScripts1 = responseScripts1;

        IsQuestLocation2 = isQuestLocation2;
        //QuestResponseScripts2 = responseScript2;
    }
}


public class NpcTalkingAbility : MonoBehaviour, IPointerDownHandler
{
    public GameObject TalkingPaper;
    Button nextButton;
    Text scriptText;

    public List<Script> scriptList;

    QuestGiver questGiver;

    enum turnToSpeack { Me, OtherPerson };
    turnToSpeack currentTurn = turnToSpeack.Me;
    int indexInScriptList = 0;
    bool playerInSpeakableArea = false;

    private void Awake()
    {
        questGiver = GetComponent<QuestGiver>();

        if (TalkingPaper != null)
        {
            nextButton = TalkingPaper.GetComponentInChildren<Button>();
            scriptText = TalkingPaper.GetComponentInChildren<Text>();
        }
    }

    void InitializeScript()
    {
        indexInScriptList = 0;
        currentTurn = turnToSpeack.Me;
    }

    void CheckIfConversationEnd()
    {
        if (indexInScriptList == (scriptList.Count))
        {
            TalkingPaper.SetActive(false);
            InitializeScript();
        }
        else
            TalkingPaper.SetActive(true);
    }
    
    

    public void Talk(QuestReciever otherPerson)
    {
        // X버튼이 대화 마지막에 눌리면 창이 닫혀야 하기 떄문에 앞에 위치
        CheckIfConversationEnd();

        if (currentTurn == turnToSpeack.Me)
        {
            // 내가 말함
            scriptText.text = scriptList[indexInScriptList].ISays;

            if (scriptList[indexInScriptList].IsQuestLocation1)
            {
                if (questGiver != null)
                {
                    // 쿼스트 줄 수 있으면 주고, 없으면 안줌
                    questGiver.GiveQuest(otherPerson);
                }
            }
        }
        else if (currentTurn == turnToSpeack.OtherPerson)
        {
            // Player(상대)가 말함
            scriptText.text = scriptList[indexInScriptList].OtherPlayerSays;

            if (scriptList[indexInScriptList].IsQuestLocation2)
            {
                if (questGiver != null)
                    questGiver.GiveQuest(otherPerson);
            }

            indexInScriptList++;
        }

        // 발언 차례 변경
        currentTurn = (currentTurn == turnToSpeack.Me) ? turnToSpeack.OtherPerson : turnToSpeack.Me;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerInSpeakableArea)
            Talk(GameManager.Instance.SinglePlayer.GetComponent<QuestReciever>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInSpeakableArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInSpeakableArea = false;
    }

}
