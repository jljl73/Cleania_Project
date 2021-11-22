using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    GameObject currentDialog = null;
    public DialogSelector MarketDialog;
    public DialogSelector RepairDialog;
    public DialogSelector EnchantDialog;
    public DialogSelector StorageDialog;
    public DialogSelector QuestDialog;
    public GameObject DungeonDialog;

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
        ShowDialog(DungeonDialog, true);
    }

    public void ShowDialog(GameObject dialog, bool bActive)
    {
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
