using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool isActiveItemPanel;

    public Canvas canvas_;
    public Canvas GetCanvas { get { return canvas_; } }

    public GameObject InventoryPanel;
    public GameObject StoragePanel;
    public GameObject SkillPanel;
    public GameObject ExpandMapPanel;
    public GameObject QuestPanel;
    public GameObject RepairPanel;
    public GameObject MarketPanel;
    public GameObject EnchantPanel;
    public GameObject MenuPanel;
    public GameObject SettingPanel;

    List<GameObject> sPanels = new List<GameObject>();

    GameObject currentPanel;

    void Awake()
    {
        GameManager.Instance.uiManager = this;
    }

    void Update()
    {
        if (GameManager.Instance.isChatting) return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ShowSkillPanel();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ExpandMapPanel.SetActive(!ExpandMapPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowQuestPanel();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OffAllPanels();
        }
    }

    public void ShowInventory()
    {
        ShowPanel(InventoryPanel.gameObject);
    }

    public void ShowSkillPanel()
    {
        ShowPanel(SkillPanel);
    }
    
    public void ShowQuestPanel()
    {
        ShowPanel(QuestPanel);
    }

    public void ShowRepairPanel()
    {
        ShowPanel(RepairPanel);
    }

    public void ShowMarketPanel()
    {
        ShowPanel(MarketPanel);
    }
    
    public void ShowEnchantPanel()
    {
        ShowPanel(EnchantPanel);
    }

    public void ShowStoragePanel()
    {
        ShowPanel(StoragePanel.gameObject);
    }

    public void ShowMenuPanel()
    {
        ShowPanel(MenuPanel);
    }

    public NPC.TYPE GetCurrentNPC()
    {
        if (StoragePanel.gameObject.activeSelf) return NPC.TYPE.Storage;
        else if (MarketPanel.activeSelf) return NPC.TYPE.Market;
        else if (EnchantPanel.activeSelf) return NPC.TYPE.Enchant;
        else if (RepairPanel.activeSelf) return NPC.TYPE.Repair;
        else return NPC.TYPE.None;
    }

    void OffAllPanels()
    {
        //if (isActiveItemPanel) ShowInventory();
        this.InventoryPanel.gameObject.SetActive(false);
        this.StoragePanel.gameObject.SetActive(false);
        this.SkillPanel.SetActive(false);
        this.ExpandMapPanel.SetActive(false);
        this.QuestPanel.SetActive(false);
        this.RepairPanel.SetActive(false);
        this.MarketPanel.SetActive(false);
        this.EnchantPanel.SetActive(false);
    }

    public void OffNPCPanels()
    {
        this.RepairPanel.SetActive(false);
        this.MarketPanel.SetActive(false);
        this.EnchantPanel.SetActive(false);
        this.StoragePanel.gameObject.SetActive(false);
    }

    public void ShowPanel(GameObject panel)
    {
        Debug.Log(panel.name);
        panel.SetActive(!panel.activeSelf);


        if (panel.activeSelf)
        {
            panel.transform.SetAsLastSibling();
            sPanels.Add(panel);
        }
        else
            sPanels.Remove(panel);

    }

    void CloseLastPanel()
    {
        if (sPanels.Count > 0)
            ShowPanel(sPanels[sPanels.Count - 1]);
        else
            ShowPanel(MenuPanel);
    }
}
