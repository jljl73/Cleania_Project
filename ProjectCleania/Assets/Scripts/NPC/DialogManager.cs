using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public GameObject MarketDialog;
    public GameObject RepairDialog;
    public GameObject EnchantDialog;
    public GameObject StorageDialog;
    GameObject QuestDialog = null;

    public void ShowMarketDialog(bool bActive)
    {
        ShowDialog(MarketDialog, bActive);
    }

    public void ShowRepairDialog(bool bActive)
    {
        ShowDialog(RepairDialog, bActive);
    }

    public void ShowEnchantDialog(bool bActive)
    {
        ShowDialog(EnchantDialog, bActive);
    }

    public void ShowStorageDialog(bool bActive)
    {
        ShowDialog(StorageDialog, bActive);
    }

    public void ShowQuestDialog(string dialogName)
    {
        QuestDialog = transform.Find(dialogName).gameObject;
        ShowDialog(QuestDialog, true);
    }

    public void ShowDialog(GameObject dialog, bool bActive)
    {
        dialog.SetActive(bActive);
        GameManager.Instance.soundPlayer.PlaySound(SoundPlayer.TYPE.NPCInteraction);
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
