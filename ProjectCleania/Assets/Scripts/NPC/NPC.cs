using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class NPC : MonoBehaviour
{
    public enum TYPE { None, Repair, Market, Enchant, Storage, Portal, Quest, Dungeon };

    public GameObject fieldName;

    public TYPE npctype;
    public TYPE NPCType { get { return npctype; } }

    public string npcname;
    public string Name { get { return npcname; } }

    public string Value;
    [SerializeField]
    GameObject questMarker;
    int count = 0;
    [SerializeField]
    QuestProgressChecker progressChecker;
    int prevState = -2;

    void Start()
    {
        transform.Find("Marker").rotation = Camera.main.transform.rotation;
        fieldName.GetComponent<TextMeshPro>().text = npcname;
        fieldName.transform.rotation = Camera.main.transform.rotation;
        fieldName.gameObject.SetActive(false);

        questMarker = transform.Find("QuestMarker").gameObject;
        questMarker.transform.rotation = Camera.main.transform.rotation;
        progressChecker = GameManager.Instance.dialogManager.transform.GetChild(transform.GetSiblingIndex()).GetComponent<QuestProgressChecker>();
    }

    void Update()
    {
        int state = progressChecker.ExistQuest();
        if(state != prevState)
        {
            prevState = state;
            if (state == -1)
            { questMarker.SetActive(false); return; }
            questMarker.GetComponent<SpriteRenderer>().sprite = Resources.Load<DataSO>("ScriptableObject/AssetData").sprites[state];
            questMarker.SetActive(true);
        }
    }

    public void ShowName(bool value)
    {
        if (value) ++count;
        else --count;

        if(count > 0)
            fieldName.SetActive(true);
        else
            fieldName.SetActive(false);
    }

    void OnMouseEnter()
    {
        ShowName(true);
    }

    private void OnMouseExit()
    {
        ShowName(false);
    }



}