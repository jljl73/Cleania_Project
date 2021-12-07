using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField]
    TextMeshProUGUI NPCName;

    void Awake()
    {
        GameManager.Instance.dialogManager = this;
    }
    

    public void ShowMarketDialog(bool bActive, string npcName)
    {
        if (DialogBox.activeSelf) return;
        NPCName.text = npcName;
        ShowDialog(true);
        MarketDialog.ShowDialog();
    }

    public void ShowRepairDialog(bool bActive, string npcName)
    {
        if (DialogBox.activeSelf) return;
        NPCName.text = npcName;
        ShowDialog(true);
        RepairDialog.ShowDialog();
    }

    public void ShowEnchantDialog(bool bActive, string npcName)
    {
        if (DialogBox.activeSelf) return;
        NPCName.text = npcName;
        ShowDialog(true);
        EnchantDialog.ShowDialog();
    }

    public void ShowStorageDialog(bool bActive, string npcName)
    {
        if (DialogBox.activeSelf) return;
        NPCName.text = npcName;
        ShowDialog(true);
        StorageDialog.ShowDialog();
    }

    public void ShowQuestDialog(bool bActive, string npcName)
    {
        if (DialogBox.activeSelf) return;
        NPCName.text = npcName;
        ShowDialog(true);
        QuestDialog.ShowDialog();
    }

    public void ShowDungeonDialog(bool bActive, string npcName)
    {
        if (DialogBox.activeSelf) return;
        NPCName.text = npcName;
        ShowDialog(true);
        DungeonDialog.ShowDialog();
    }

    void ShowDialog(bool bActive)
    {
        DialogBox.SetActive(bActive);
        if(bActive)
            GameManager.Instance.soundPlayer.PlaySound(SoundPlayer.TYPE.NPCInteraction);
    }

    public void OffDialog()
    {
        ShowDialog(false);
    }
}
