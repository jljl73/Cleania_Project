using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class NPC : MonoBehaviour
{
    public GameObject panel;
    public GameObject fieldName;
    public TextMesh textMesh;

    void Update()
    {
        textMesh.transform.rotation = Camera.main.transform.rotation;
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void ShowName(bool value)
    {
        fieldName.SetActive(value);
    }

}