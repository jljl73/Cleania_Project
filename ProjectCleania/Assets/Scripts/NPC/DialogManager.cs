using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public GameObject MarketDialog;
    public UIManager uiManager;

    public void ShowMarketDialog()
    {
        ShowDialog(MarketDialog);
    }

    void ShowDialog(GameObject dialog)
    {
        dialog.SetActive(!dialog.activeSelf);
    }
}
