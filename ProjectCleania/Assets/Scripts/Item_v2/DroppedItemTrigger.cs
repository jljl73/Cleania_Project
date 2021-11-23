using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemTrigger : MonoBehaviour
{
    //public UIManager ui;
    List<GameObject> droppedItems = new List<GameObject>();

    GameObject droppedItem;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            droppedItem = FindCloseDroppedItem();
            if (droppedItem == null) return;

            ItemObject_v2 container = droppedItem.GetComponent<ItemObject_v2>();
            ItemInstance itemData = container.ItemData;

            if (SavedData.Instance.Item_Inventory.Add(itemData))
            {
                container.Parent.Remove(itemData);
                droppedItems.Remove(droppedItem);
            }

        }
    }

    GameObject FindCloseDroppedItem()
    {
        if (droppedItems.Count == 0) return null;

        GameObject droppedItem = droppedItems[0];
        float minDist = Vector3.Distance(transform.position, droppedItem.transform.position);

        for (int i = droppedItems.Count - 1; i > 0; --i)
        {
            float dist = Vector3.Distance(transform.position, droppedItems[i].transform.position);
            if (dist < minDist)
            {
                droppedItem = droppedItems[i];
                dist = minDist;
            }
        }

        return droppedItem;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DroppedItem"))
        {
            droppedItems.Add(other.gameObject);
            //other.GetComponent<NPC>().ShowName(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DroppedItem"))
        {
            //ui.OffNPCPanels();
            droppedItems.Remove(other.gameObject);
            //other.GetComponent<NPC>().ShowName(false);
        }
    }
}
