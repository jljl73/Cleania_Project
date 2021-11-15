using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrowPanel : MonoBehaviour
{
    [System.NonSerialized]
    public UI_ItemController controller;

    public void OnClickedOK()
    {
        if (controller != null)
        {
            ItemInstance item = controller.itemInstance;
            controller.currentContainer.Remove(controller);
            SavedData.Instance.Item_World.Add(item);

            controller = null;
        }
        gameObject.SetActive(false);
    }

    public void OnClickedCancel()
    {
        controller = null;

        gameObject.SetActive(false);
    }
}
