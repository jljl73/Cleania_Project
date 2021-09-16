using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    bool isActiveItemPanel = true;
    public GameObject ItemPanel;
    
    private void Start()
    {
        OnOffItemPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOffItemPanel();
        }
    }

    void OnOffItemPanel()
    {
        if (isActiveItemPanel)
            ItemPanel.transform.Translate(new Vector3(2000, 0, 0));
        else
            ItemPanel.transform.Translate(new Vector3(-2000, 0, 0));

        isActiveItemPanel = !isActiveItemPanel;
    }
}
