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
    Ability.DynamicOption[] optionDatas;

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

        optionDatas = equipment.DynamicOptions;

        for (int i = 0; i < optionDatas.Length; ++i)
        {
            if (optionDatas[i].Equals(equipment.ChangedOption))
                options[i].fontStyle = FontStyle.Bold;
            else
                options[i].fontStyle = FontStyle.Normal;
        }

        int ct = 0;
        foreach(var v in equipment.DynamicProperties_ToString())
        {
            options[ct].transform.parent.gameObject.SetActive(true);
            options[ct++].text = v;
        }

    }

    enum FailCase
    {
        ControllerNotSelected,
        OptionNotSelected,
        OptionAlreadyChanged,
        NotEnoughMeney
    }

    public void SelectOptionToChange(int index)
    {
        //if (selectedItem == null || equipment == null)
        //    EnchantFail(FailCase.ControllerNotSelected);
        //else if (equipment.ChangedOption.Stat != Ability.Stat.EnumTotal || equipment.ChangedOption.How != Ability.Enhance.EnumTotal)
        //    EnchantFail(FailCase.OptionAlreadyChanged);
        //else
        //{
        //    equipment.ChangedOption = optionDatas[index];
        //}
    }


    public void ChangeSelectedOption()
    {
        //if (selectedItem == null || equipment == null)
        //    EnchantFail(FailCase.ControllerNotSelected);
        //else if (equipment.ChangedOption.Stat == Ability.Stat.EnumTotal || equipment.ChangedOption.How == Ability.Enhance.EnumTotal)
        //    EnchantFail(FailCase.OptionNotSelected);
        //else if (GameManager.Instance.uiManager.InventoryPanel.Crystal < 1000)
        //    EnchantFail(FailCase.NotEnoughMeney);
        //else
        //{
        //    if (EquipmentDealer.TryChangeDynamic(equipment, EquipmentDealer.CandidateDynamicOption(equipment)))
        //        SelectItem(selectedItem);   // refresh
        //    else
        //        Debug.LogError("Logic error in NPCEnchant : SelectOptionToChange");
        //}

        EquipmentDealer.ShuffleDynamics(equipment);
        SelectItem(selectedItem);
    }


    void EnchantFail(FailCase reason)
    {
        switch (reason)
        {
            default:
                OnEnable(); // reset panel
                Debug.Log($"enchant fail : {reason.ToString()}");
                break;
        }
    }

}
