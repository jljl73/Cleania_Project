using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class NPC : MonoBehaviour
{
    public enum TYPE { None, Repair, Market, Enchant, Storage, Portal, Quest, Dungeon };

    public GameObject fieldName;
    public TextMesh textMesh;

    public TYPE npctype;
    public TYPE NPCType { get { return npctype; } }

    public string Value;


    void Update()
    {
        textMesh.transform.rotation = Camera.main.transform.rotation;
    }
    
    public void ShowName(bool value)
    {
        fieldName.SetActive(value);
    }

}