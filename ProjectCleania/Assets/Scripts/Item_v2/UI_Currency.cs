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


    public void AddCrystal(int amount)
    {
        crystal += amount;
        if (crystal < 0)
            crystal = 0;
        TextCrystal.text = crystal.ToString();
    }
}
