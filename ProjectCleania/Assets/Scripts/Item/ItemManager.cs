using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    List<GameObject> items = new List<GameObject>();

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.I))
        //{
        //    for(int i = 0; i < items.Count; ++i)
        //    {
        //        items[i].SetActive(true);
        //    }
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item") && Input.GetKeyDown(KeyCode.G))
        {
            //Debug.Log(other.name);
            Loot(other.gameObject);
        }
    }

    void Loot(GameObject itemObject)
    {
        Item item = itemObject.GetComponent<Item>();
        item.PutInventory();
        //GameObject newItem = Instantiate(item.itemController);
        //newItem.GetComponent<ItemController>().PutInventory();
        //items.Add(newItem);
    }
}
