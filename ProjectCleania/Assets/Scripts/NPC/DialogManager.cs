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

    [SerializeField]
    Transform PageTranform;
    public Transform Page { get => PageTranform; }
    [SerializeField]
    GameObject DialogBox;


    void Awake()
    {
        GameManager.Instance.dialogManager = this;
    }
    

    public void ShowMarketDialog(bool bActive)
    {
        ShowDialog(true);
        MarketDialog.ShowDialog();
    }

    public void ShowRepairDialog(bool bActive)
    {
        ShowDialog(true);
        RepairDialog.ShowDialog();
    }

    public void ShowEnchantDialog(bool bActive)
    {
        ShowDialog(true);
        EnchantDialog.ShowDialog();
    }

    public void ShowStorageDialog(bool bActive)
    {
        ShowDialog(true);
        StorageDialog.ShowDialog();
    }

    public void ShowQuestDialog(bool bActive)
    {
        ShowDialog(true);
        QuestDialog.ShowDialog();
    }

    public void ShowDungeonDialog(bool bActive)
    {
        ShowDialog(true);
        DungeonDialog.ShowDialog();
    }

    void ShowDialog(bool bActive)
    {
        DialogBox.SetActive(bActive);
        GameManager.Instance.soundPlayer.PlaySound(SoundPlayer.TYPE.NPCInteraction);
    }

    public void OffDialog()
    {
        ShowDialog(false);
    }
}
