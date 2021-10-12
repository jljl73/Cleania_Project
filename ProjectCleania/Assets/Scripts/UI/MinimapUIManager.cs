using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapUIManager : MonoBehaviour
{
    public void OnClickMenuButton()
    {
        if (GameManager.Instance.menuManager != null)
            GameManager.Instance.menuManager.PopUpMenuUI();
    }
}
