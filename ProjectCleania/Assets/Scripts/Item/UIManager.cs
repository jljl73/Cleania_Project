using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject ItemPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ItemPanel.SetActive(!ItemPanel.activeSelf);
        }
    }
}
