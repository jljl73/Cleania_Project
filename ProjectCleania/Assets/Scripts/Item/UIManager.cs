using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool isActiveItemPanel;

    public GameObject itemPanel;
    public GameObject ItemPanel { get { return itemPanel; } }

    public ItemInventory itemInventory_;
    public ItemInventory GetItemInventory { get { return itemInventory_; } }

    public Canvas canvas_;
    public Canvas GetCanvas { get { return canvas_; } }

    public GameObject SkillPanel;
    public GameObject ExpandMapPanel;
    public GameObject QuestPanel;
    public GameObject RepairPanel;
    public GameObject BuyPanel;
    public GameObject SellPanel;
    public GameObject EnchantPanel;
    public GameObject StoragePanel;

    Stack<GameObject> sPanels = new Stack<GameObject>();

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
            //OffAllPanels();
            CloseLastPanel();
        }
    }

    public void ShowInventory()
    {
        ShowPanel(itemPanel);
        //if (isActiveItemPanel)
        //    itemPanel.transform.Translate(new Vector3(20000, 0, 0));
        //else
        //    itemPanel.transform.Translate(new Vector3(-20000, 0, 0));

        //isActiveItemPanel = !isActiveItemPanel;
        //if (isActiveItemPanel)
        //    itemPanel.transform.SetAsLastSibling();
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

    public void ShowBuyPanel()
    {
        ShowPanel(BuyPanel);
    }

    public void ShowSellPanel()
    {
        ShowPanel(SellPanel);
    }

    public void ShowEnchantPanel()
    {
        ShowPanel(EnchantPanel);
    }

    public void ShowStoragePanel()
    {
        ShowPanel(StoragePanel);
    }

    void OffAllPanels()
    {
        if (isActiveItemPanel) ShowInventory();
        this.SkillPanel.SetActive(false);
        this.ExpandMapPanel.SetActive(false);
        this.QuestPanel.SetActive(false);
        this.RepairPanel.SetActive(false);
        this.BuyPanel.SetActive(false);
        this.SellPanel.SetActive(false);
        this.EnchantPanel.SetActive(false);
        this.StoragePanel.SetActive(false);
    }

    public void OffNPCPanels()
    {
        this.RepairPanel.SetActive(false);
        this.BuyPanel.SetActive(false);
        this.SellPanel.SetActive(false);
        this.EnchantPanel.SetActive(false);
        this.StoragePanel.SetActive(false);
    }

    void ShowPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
        if (panel.activeSelf)
        {
            panel.transform.SetAsLastSibling();
            sPanels.Push(panel);
        }
    }

    void CloseLastPanel()
    {
        if (sPanels.Count > 0)
            sPanels.Pop().SetActive(false);
    }
}
