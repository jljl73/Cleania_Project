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
    public GameObject SkillPanel;
    public GameObject ExpandMapPanel;
    public GameObject QuestPanel;
    public GameObject RepairPanel;
    public GameObject MarketPanel;
    public GameObject EnchantPanel;
    public GameObject StoragePanel;
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
            CloseLastPanel();
        }
    }

    public void ShowInventory()
    {
        ShowPanel(InventoryPanel);
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
        if(RepairPanel.activeSelf)
            GameManager.Instance.npcManager.curNPC = NPC.TYPE.Repair;
    }

    public void ShowMarketPanel()
    {
        ShowPanel(MarketPanel);
        if (MarketPanel.activeSelf)
            GameManager.Instance.npcManager.curNPC = NPC.TYPE.Market;
    }
    
    public void ShowEnchantPanel()
    {
        ShowPanel(EnchantPanel);
        if (EnchantPanel.activeSelf)
            GameManager.Instance.npcManager.curNPC = NPC.TYPE.Enchant;
    }

    public void ShowStoragePanel()
    {
        ShowPanel(StoragePanel);
        if (StoragePanel.activeSelf)
            GameManager.Instance.npcManager.curNPC = NPC.TYPE.Storage;
    }

    public void ShowMenuPanel()
    {
        ShowPanel(MenuPanel);
    }

    void OffAllPanels()
    {
        if (isActiveItemPanel) ShowInventory();
        this.SkillPanel.SetActive(false);
        this.ExpandMapPanel.SetActive(false);
        this.QuestPanel.SetActive(false);
        this.RepairPanel.SetActive(false);
        this.MarketPanel.SetActive(false);
        this.EnchantPanel.SetActive(false);
        this.StoragePanel.SetActive(false);
    }

    public void OffNPCPanels()
    {
        this.RepairPanel.SetActive(false);
        this.MarketPanel.SetActive(false);
        this.EnchantPanel.SetActive(false);
        this.StoragePanel.SetActive(false);
    }

    public void ShowPanel(GameObject panel)
    {
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
