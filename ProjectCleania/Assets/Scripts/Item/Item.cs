using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string KeyString;
    public GameObject itemController;
    public InventoryItemGenerator generator;
    //public ItemOption

    private void Start()
    {
        //itemController = transform.GetChild(0).gameObject;
        //itemController.KeyString = KeyString;
    }

    public void PutInventory()
    {
        generator.GenerateItem();
        //itemController.SetActive(true);
    }
}
