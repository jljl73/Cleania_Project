using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Currency : MonoBehaviour
{
    public enum SyncType
    {
        SlectOne,
        Inventory,
        Storage
    }

    public enum SourceType
    {
        Trade,
        Hunt,
        Deposit,
        Reward
    }

    [SerializeField]
    SyncType syncWith;


    [SerializeField]
    int crystal = 0;
    public int Crystal { get { return crystal; } }
    [SerializeField]
    Text TextCrystal;

    private void Awake()
    {
        if (TextCrystal != null)
            TextCrystal.text = crystal.ToString();
    }


    public void AddCrystal(int amount, SourceType sourceType)
    {
        crystal += amount;
        if (crystal < 0)
            crystal = 0;
        TextCrystal.text = crystal.ToString();

        if(syncWith == SyncType.Inventory && sourceType != SourceType.Deposit && amount > 0)
            GameManager.Instance.chatManager.ShowAcquireClean(amount);
    }
}
