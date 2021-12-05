using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static GameObject currentDialog = null;
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
        ShowDialog(MarketDialog.gameObject, true);
    }

    public void ShowRepairDialog(bool bActive)
    {
        RepairDialog.ShowDialog();
        ShowDialog(RepairDialog.gameObject, true);
    }

    public void ShowEnchantDialog(bool bActive)
    {
        EnchantDialog.ShowDialog();
        ShowDialog(EnchantDialog.gameObject, true);
    }

    public void ShowStorageDialog(bool bActive)
    {
        StorageDialog.ShowDialog();
        ShowDialog(StorageDialog.gameObject, true);
    }

    public void ShowQuestDialog(bool bActive)
    {
        QuestDialog.ShowDialog();
        ShowDialog(QuestDialog.gameObject, true);
    }

    public void ShowDungeonDialog(bool bActive)
    {
        DungeonDialog.ShowDialog();
        ShowDialog(DungeonDialog.gameObject, true);
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
