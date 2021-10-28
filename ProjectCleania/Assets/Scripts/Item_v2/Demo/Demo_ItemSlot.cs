using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Demo_ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    Image _image;
    Button _button;
    ItemInstance _item;

    [System.NonSerialized]
    public Text[] optionTexts;
    [System.NonSerialized]
    public Image optionPanel;

    private void Start()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    public void Set(ItemInstance item)
    {
        if (item == _item)
            return;
        else if (item != null)
        {
            _item = item;

            _button.enabled = true;

            _image.enabled = true;
            _image.sprite = item.SO.ItemImage;
            transform.localScale = new Vector3(item.SO.GridSize.Width, item.SO.GridSize.Height, 1);
        }
        else
        {
            _item = null;

            _button.enabled = false;

            _image.enabled = false;
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

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        optionPanel.gameObject.SetActive(true);

        int i = 0;
        optionTexts[i++].text = _item.SO.ItemName;
        optionTexts[i++].text = _item.SO.MainCategory.ToString();
        optionTexts[i++].text = _item.SO.Rank.ToString();

        if (_item is ItemInstance_Equipment)
            optionTexts[i++].text = _item.SO.SubCategory.ToString();

        optionTexts[i++].text = _item.SO.ToolTip;

        if (_item is ItemInstance_Equipment)
        {
            List<string> stats = ((ItemInstance_Equipment)_item).StaticProperties_ToString();
            foreach (string s in stats)
                optionTexts[i++].text = s;

            List<string> dynamics = ((ItemInstance_Equipment)_item).DynamicProperties_ToString();
            foreach (string s in dynamics)
                optionTexts[i++].text = s;

        }
                
        optionPanel.rectTransform.sizeDelta = new Vector2(optionPanel.rectTransform.sizeDelta.x, i * 20);

        while(i > 0)
            optionTexts[--i].enabled = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        optionPanel.gameObject.SetActive(false);
        foreach (var i in optionTexts)
            i.enabled = false;
    }

    void IPointerMoveHandler.OnPointerMove(PointerEventData eventData)
    {
        optionPanel.rectTransform.position = eventData.position;
    }
}
