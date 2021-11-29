using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInMarket : MonoBehaviour
{
    public ItemSO ItemSO
    { private set; get; }

    [SerializeField]
    Text price;
    [SerializeField]
    Image image;
    [SerializeField]
    Text itemName;
    [SerializeField]
    Toggle toggle;

    public void Initialize(ItemSO SO, ToggleGroup toggleGroup)
    {
        this.ItemSO = SO;
        price.text = SO.Price.ToString();
        itemName.text = SO.ItemName;
        image.sprite = SO.ItemImage;
        toggle.group = toggleGroup;
    }
}