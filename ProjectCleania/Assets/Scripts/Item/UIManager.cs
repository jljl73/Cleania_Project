using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    bool isActiveItemPanel = true;
    public GameObject itemPanel;
    public GameObject ItemPanel { get { return itemPanel; } }

    public ItemInventory itemInventory_;
    public ItemInventory GetItemInventory { get { return itemInventory_; } }

    public Canvas canvas_;
    public Canvas GetCanvas { get { return canvas_; } }

    public SkillPanel skillPanel;
    public GameObject ExpandMapPanel; 

    
    private void Start()
    {
        OnOffInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOffInventory();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            skillPanel.OnOffPanel();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ExpandMapPanel.SetActive(!ExpandMapPanel.activeSelf);
        }

    }

    public void OnOffInventory()
    {
        if (isActiveItemPanel)
            itemPanel.transform.Translate(new Vector3(2000, 0, 0));
        else
            itemPanel.transform.Translate(new Vector3(-2000, 0, 0));

        isActiveItemPanel = !isActiveItemPanel;
    }

    public void OnOffSkillPanel()
    {
        skillPanel.OnOffPanel();
    }

}
