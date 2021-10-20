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

    List<int> ListOpendUI = new List<int>();

    
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
        if (isActiveItemPanel)
            itemPanel.transform.Translate(new Vector3(20000, 0, 0));
        else
            itemPanel.transform.Translate(new Vector3(-20000, 0, 0));

        isActiveItemPanel = !isActiveItemPanel;

    }

    public void ShowSkillPanel()
    {
        SkillPanel.SetActive(!SkillPanel.activeSelf);
    }
    
    public void ShowQuestPanel()
    {
        QuestPanel.SetActive(!QuestPanel.activeSelf);
    }

    public void ShowRepairPanel()
    {
        RepairPanel.SetActive(!RepairPanel.activeSelf);
    }

    public void ShowBuyPanel()
    {
        BuyPanel.SetActive(!BuyPanel.activeSelf);
    }

    public void ShowSellPanel()
    {
        SellPanel.SetActive(!SellPanel.activeSelf);
    }

    public void ShowEnchantPanel()
    {
        EnchantPanel.SetActive(!EnchantPanel.activeSelf);
    }

    public void ShowStoragePanel()
    {
        StoragePanel.SetActive(!StoragePanel.activeSelf);
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


}
