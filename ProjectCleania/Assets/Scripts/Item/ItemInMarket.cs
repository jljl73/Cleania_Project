using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInMarket : MonoBehaviour
{
    public ItemInstance itemInstance
    { private set; get; }

    [SerializeField]
    Text price;
    [SerializeField]
    Image image;
    [SerializeField]
    Text itemName;
    [SerializeField]
    Toggle toggle;

    public void Initialize(ItemInstance itemInstance, ToggleGroup toggleGroup)
    {
        this.itemInstance = itemInstance;
        price.text = itemInstance.SO.Price.ToString();
        itemName.text = itemInstance.SO.ItemName;
        image.sprite = itemInstance.SO.ItemImage;
        toggle.group = toggleGroup;
    }
}