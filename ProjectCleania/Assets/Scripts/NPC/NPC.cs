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

    public string m_Name;
    public string Name { get { return m_Name; } }

    public string Value;
    int count = 0;

    private void Start()
    {
        transform.Find("Marker").rotation = Camera.main.transform.rotation;
        fieldName.GetComponent<TextMeshPro>().text = m_Name;
    }

    void Update()
    {
        fieldName.transform.rotation = Camera.main.transform.rotation;
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

}