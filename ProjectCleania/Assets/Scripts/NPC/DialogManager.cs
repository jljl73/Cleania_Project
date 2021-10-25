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
    }
}
