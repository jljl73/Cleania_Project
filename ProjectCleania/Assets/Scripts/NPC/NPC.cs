using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class NPC : MonoBehaviour
{
    public GameObject fieldName;
    public TextMesh textMesh;

    public string npctype;
    public string NPCType { get { return npctype; } }

    void Update()
    {
        textMesh.transform.rotation = Camera.main.transform.rotation;
    }
    
    public void ShowName(bool value)
    {
        fieldName.SetActive(value);
    }

}