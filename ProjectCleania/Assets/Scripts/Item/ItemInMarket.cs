using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInMarket : MonoBehaviour
{
    public int ItemID
    { private set; get; }

    [SerializeField]
    Text price;
    [SerializeField]
    Image image;
    [SerializeField]
    Text itemName;
    [SerializeField]
    Toggle toggle;

    public void Initialize(int ItemID, ToggleGroup toggleGroup)
    {
        this.ItemID = ItemID;
        price.text = ItemSO.Load(ItemID).Price.ToString();
        itemName.text = ItemSO.Load(ItemID).ItemName;
        image.sprite = ItemSO.Load(ItemID).ItemImage;
        toggle.group = toggleGroup;
    }
}