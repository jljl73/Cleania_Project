using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowPanel : MonoBehaviour
{
    public ItemInventory itemInventory;

    public void OnClickedOK()
    {
        itemInventory.OnThrowOK();
        gameObject.SetActive(false);
    }

    public void OnClickedCancel()
    {
        gameObject.SetActive(false);
    }
}
