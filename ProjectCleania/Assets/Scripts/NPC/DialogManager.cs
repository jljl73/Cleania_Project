using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    GameObject currentDialog = null;
    public QuestProgressChecker MarketDialog;
    public QuestProgressChecker RepairDialog;
    public QuestProgressChecker EnchantDialog;
    public QuestProgressChecker StorageDialog;
    public QuestProgressChecker QuestDialog;
    public QuestProgressChecker DungeonDialog;

    void Awake()
    {
        GameManager.Instance.dialogManager = this;
    }



    public void ShowMarketDialog(bool bActive)
    {
        MarketDialog.ShowDialog();
    }

    public void ShowRepairDialog(bool bActive)
    {
        RepairDialog.ShowDialog();
    }

    public void ShowEnchantDialog(bool bActive)
    {
        EnchantDialog.ShowDialog();
    }

    public void ShowStorageDialog(bool bActive)
    {
        StorageDialog.ShowDialog();
    }

    public void ShowQuestDialog(bool bActive)
    {
        QuestDialog.ShowDialog();
    }

    public void ShowDungeonDialog(bool bActive)
    {
        DungeonDialog.ShowDialog();
    }

    public void ShowDialog(GameObject dialog, bool bActive)
    {
        if (dialog == null) return;
        dialog.SetActive(bActive);
        if (bActive)
            currentDialog = dialog;
        else
            currentDialog = null;

        GameManager.Instance.soundPlayer.PlaySound(SoundPlayer.TYPE.NPCInteraction);
    }

    public void OffDialog()
    {
        ShowDialog(currentDialog, false);
    }
}
