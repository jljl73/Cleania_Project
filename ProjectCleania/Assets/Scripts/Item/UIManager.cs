using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool isActiveItemPanel;

    public Canvas canvas_;
    public Canvas GetCanvas { get { return canvas_; } }

    [Header ("왼쪽 창")]
    public GameObject StoragePanel;
    public GameObject QuestPanel;
    public GameObject SkillPanel;
    public GameObject MarketPanel;

    [Header ("가운데 창")]
    public GameObject MenuPanel;
    public GameObject SettingPanel;
    public GameObject RepairPanel;
    public GameObject ExpandMapPanel;
    public GameObject EnchantPanel;
    public GameObject DiePanel;

    [Header ("오른쪽 창")]
    public GameObject InventoryPanel;

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
            ShowInventory(!InventoryPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ShowSkillPanel(!SkillPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ExpandMapPanel.SetActive(!ExpandMapPanel.activeSelf);
            
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowQuestPanel(!QuestPanel.activeSelf);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLastPanel();
        }
    }

    public void ShowInventory(bool bActive)
    {
        GameManager.Instance.soundPlayer?.PlaySound(SoundPlayer.TYPE.Inventory);
        ShowPanel(InventoryPanel.gameObject, bActive);
    }

    public void ShowSkillPanel(bool bActive)
    {
        OffLeftPanel();
        ShowPanel(SkillPanel, bActive);
    }
    
    public void ShowQuestPanel(bool bActive)
    {
        OffLeftPanel();
        ShowPanel(QuestPanel, bActive);
    }

    public void ShowRepairPanel(bool bActive)
    {
        OffMiddlePanel();
        if (bActive)
            ShowPanel(InventoryPanel, true);
        ShowPanel(RepairPanel, bActive);
    }

    public void ShowMarketPanel(bool bActive)
    {
        OffLeftPanel();
        if (bActive)
            ShowPanel(InventoryPanel, true);
        ShowPanel(MarketPanel, bActive);
    }
    
    public void ShowEnchantPanel(bool bActive)
    {
        OffMiddlePanel();
        if (bActive)
            ShowPanel(InventoryPanel, true);
        ShowPanel(EnchantPanel, bActive);
    }

    public void ShowStoragePanel(bool bActive)
    {
        OffLeftPanel();
        if (bActive)
        {
            ShowPanel(InventoryPanel, true);
            GameManager.Instance.soundPlayer.PlaySound(SoundPlayer.TYPE.Storage);
        }
        ShowPanel(StoragePanel.gameObject, bActive);
    }

    public void ShowSettingPanel(bool bActive)
    {
        ShowPanel(SettingPanel, bActive);
    }

    public void ShowMenuPanel(bool bActive)
    {
        OffMiddlePanel();
        ShowPanel(MenuPanel, bActive);
    }

    public void ShowDiePanel(bool bActive)
    {
        DiePanel.SetActive(bActive);
        if (bActive)
            DiePanel.transform.SetAsLastSibling();
    }
   

    public NPC.TYPE GetCurrentNPC()
    {
        if (StoragePanel.gameObject.activeSelf) return NPC.TYPE.Storage;
        else if (MarketPanel.activeSelf) return NPC.TYPE.Market;
        else if (EnchantPanel.activeSelf) return NPC.TYPE.Enchant;
        else if (RepairPanel.activeSelf) return NPC.TYPE.Repair;
        else return NPC.TYPE.None;
    }

    void OffLeftPanel()
    {
        ShowPanel(StoragePanel, false);
        ShowPanel(QuestPanel, false);
        ShowPanel(SkillPanel, false);
        ShowPanel(MarketPanel, false);
    }

    void OffMiddlePanel()
    {
        MenuPanel.SetActive(false);
        EnchantPanel.SetActive(false);
        RepairPanel.SetActive(false);
    }


    void OffAllPanels()
    {
        sPanels.Clear();
        //if (isActiveItemPanel) ShowInventory();
        this.InventoryPanel.gameObject.SetActive(false);
        this.StoragePanel.gameObject.SetActive(false);
        this.SkillPanel.SetActive(false);
        this.ExpandMapPanel.SetActive(false);
        this.QuestPanel.SetActive(false);
        this.RepairPanel.SetActive(false);
        this.MarketPanel.SetActive(false);
        this.EnchantPanel.SetActive(false);
        this.MenuPanel.SetActive(false);
    }

    public void OffNPCPanels()
    {
        ShowPanel(RepairPanel, false);
        ShowPanel(MarketPanel, false);
        ShowPanel(EnchantPanel, false);
        ShowPanel(StoragePanel, false);
    }

    public void ShowPanel(GameObject panel, bool bActive)
    {
        panel.SetActive(bActive);

        if (panel.activeSelf)
        {
            panel.transform.SetAsLastSibling();
            if (!sPanels.Contains(panel))
                sPanels.Add(panel);
        }
        else if(sPanels.Contains(panel))
            sPanels.Remove(panel);
    }

    void CloseLastPanel()
    {
        if(DialogManager.currentDialog != null)
        {
            GameManager.Instance.dialogManager.OffDialog();
            return;
        }

        if (sPanels.Count > 0)
            ShowPanel(sPanels[sPanels.Count - 1], false);
        else
            ShowPanel(MenuPanel, true);
    }
}
