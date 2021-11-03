using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCEnchant : MonoBehaviour
{
    [SerializeField]
    Image backgroundImage;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    Text itemName;
    [SerializeField]
    Text itemDetail;
    [SerializeField]
    Text[] options; 

    ItemController_v2 selectedItem;
    ItemInstance_Equipment equipment;

    private void OnEnable()
    {
        selectedItem = null;
        equipment = null;
        itemImage.enabled = false;
        backgroundImage.enabled = false;
        itemName.enabled = false;
        itemDetail.enabled = false;

        foreach (var v in options)
            v.transform.parent.gameObject.SetActive(false);
    }

    public void SelectItem(ItemController_v2 item)
    {
        if (!(item.itemInstance is ItemInstance_Equipment))
            return;
        
        selectedItem = item;
        equipment = (ItemInstance_Equipment)item.itemInstance;
        itemImage.enabled = true;
        itemImage.sprite = selectedItem.itemInstance.SO.ItemImage;
        itemName.enabled = true;
        itemName.text = equipment.SO.ItemName;
        itemDetail.enabled = true;
        itemDetail.text = equipment.SO.ToolTip;
        backgroundImage.enabled = true;
        //backgroundImage.sprite = ;

        int ct = 0;
        foreach(var v in equipment.DynamicProperties_ToString())
        {
            options[ct].transform.parent.gameObject.SetActive(true);
            options[ct++].text = v;
        }

    }

    // public Change Option
    //
    // 

}
