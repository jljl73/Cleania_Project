using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class DroppedItemTrigger : MonoBehaviour
{
    //public UIManager ui;
    List<GameObject> droppedItems = new List<GameObject>();

    GameObject droppedItem;
    StringBuilder sb = new StringBuilder();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            droppedItem = FindCloseDroppedItem();
            if (droppedItem == null) return;

            PickUp(droppedItem);
        }
                
    }
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (droppedItems.Contains(InputManager.clickedObject))
                PickUp(InputManager.clickedObject);
        }
    }

    public void PickUp(GameObject item)
    {
        if (!droppedItems.Contains(item)) return;

        ItemObject_v2 container = item.GetComponent<ItemObject_v2>();
        ItemInstance itemData = container.ItemData;

        if (SavedData.Instance.Item_Inventory.Add(itemData))
        {
            //container.ItemData.CurrentStorage?.Remove(itemData);
            droppedItems.Remove(item);
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

            Debug.Log(InputManager.clickedObject);
            Debug.Log(other.gameObject);
            if (InputManager.clickedObject == other.gameObject)
                PickUp(other.gameObject);
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
