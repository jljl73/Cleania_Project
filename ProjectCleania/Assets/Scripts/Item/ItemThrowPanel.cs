using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowPanel : MonoBehaviour
{
    public UI_ItemController controller;

    public void OnClickedOK()
    {
        ItemInstance item = controller.itemInstance;
        controller.currentContainer.Remove(controller);
        SavedData.Instance.Item_World.Add(item);

        gameObject.SetActive(false);
    }

    public void OnClickedCancel()
    {
        gameObject.SetActive(false);
    }
}
