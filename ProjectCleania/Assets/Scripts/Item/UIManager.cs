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
    public GameObject QuestPanel;
    public GameObject RepairPanel;

    
    private void Start()
    {
        ShowInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            skillPanel.OnOffPanel();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ExpandMapPanel.SetActive(!ExpandMapPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowSkillPanel();
        }

    }

    public void ShowInventory()
    {
        if (isActiveItemPanel)
            itemPanel.transform.Translate(new Vector3(2000, 0, 0));
        else
            itemPanel.transform.Translate(new Vector3(-2000, 0, 0));

        isActiveItemPanel = !isActiveItemPanel;
    }

    public void ShowSkillPanel()
    {
        skillPanel.OnOffPanel();
    }
    
    public void ShowQuestPanel()
    {
        QuestPanel.SetActive(!QuestPanel.activeSelf);
    }

    public void ShowPanel(bool value, string npcType)
    {
        switch(npcType)
        {
            case "Repair":
                RepairPanel.SetActive(value);
                break;
        }
    }

}
