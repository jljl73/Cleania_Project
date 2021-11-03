using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public UIManager uiManager;
    public GameObject MarketDialog;
    public GameObject RepairDialog;
    public GameObject EnchantDialog;
    public GameObject StorageDialog;
    GameObject QuestDialog = null;

    public void ShowMarketDialog()
    {
        ShowDialog(MarketDialog);
    }

    public void ShowRepairDialog()
    {
        ShowDialog(RepairDialog);
    }

    public void ShowEnchantDialog()
    {
        ShowDialog(EnchantDialog);
    }

    public void ShowStorageDialog()
    {
        ShowDialog(StorageDialog);
    }

    public void ShowQuestDialog(string dialogName)
    {
        QuestDialog = transform.Find(dialogName).gameObject; ;
        ShowDialog(QuestDialog);
    }

    void ShowDialog(GameObject dialog)
    {
        dialog.SetActive(!dialog.activeSelf);
    }

    public void OffDialog()
    {
        MarketDialog.SetActive(false);
        RepairDialog.SetActive(false);
        EnchantDialog.SetActive(false);
        StorageDialog.SetActive(false);
        if(QuestDialog != null)
            QuestDialog.SetActive(false);
    }
}
