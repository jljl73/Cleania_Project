using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInMarket : MonoBehaviour
{
    ItemInstance itemInstance;

    [SerializeField]
    Text price;
    [SerializeField]
    Image image;
    [SerializeField]
    Text itemName;

    public void Initialize(ItemInstance itemInstance)
    {
        this.itemInstance = itemInstance;
        price.text = itemInstance.SO.Price.ToString();
        itemName.text = itemInstance.SO.ItemName;
        image.sprite = itemInstance.SO.ItemImage;
    }
}