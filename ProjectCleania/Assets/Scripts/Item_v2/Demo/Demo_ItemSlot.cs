using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo_ItemSlot : MonoBehaviour
{
    Image image;
    Button button;
    ItemInstance _item;

    private void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void Set(ItemInstance item)
    {
        if (item == _item)
            return;
        else if (item != null)
        {
            _item = item;

            button.enabled = true;

            image.enabled = true;
            image.sprite = item.SO.ItemImage;
            transform.localScale = new Vector3(item.SO.GridSize.Width, item.SO.GridSize.Height, 1);
        }
        else
        {
            _item = null;

            button.enabled = false;

            image.enabled = false;
        }
    }
    

    public void TryEquip()
    {
        if (_item is ItemInstance_Equipment)
        {
            ItemStorage_LocalGrid inven = SavedData.Instance.Item_Inventory;
            var loc = inven[_item];

            inven.Remove(_item);

            ItemInstance_Equipment oldEquip = GameManager.Instance.PlayerEquipments.Equip((ItemInstance_Equipment)_item);
            inven.Add(oldEquip, loc);

            // _item will be changed next frame
        }
    }

    public void TryUnequip()
    {
        if (SavedData.Instance.Item_Inventory.Add(_item))
        {
            ItemInstance_Equipment equipment = (ItemInstance_Equipment)_item;
            GameManager.Instance.PlayerEquipments.Unequip(equipment.EquipmentType);
        }
    }
}
