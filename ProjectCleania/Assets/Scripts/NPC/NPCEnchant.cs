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
    Text[] optionTexts;
    [SerializeField]
    int enchantCost = 1000;
    [SerializeField]
    Text costPanel;

    UI_ItemController selectedItem;
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
        //costPanel.text = "-";

        foreach (var v in optionTexts)
            v.transform.parent.gameObject.SetActive(false);

        
    }

    public void SelectItem(UI_ItemController item)
    {
        if (!(item.itemInstance is ItemInstance_Equipment))
            return;

        OnEnable();

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
                optionTexts[i].fontStyle = FontStyle.Bold;
            else
                optionTexts[i].fontStyle = FontStyle.Normal;
        }

        int ct = 0;
        foreach(var v in equipment.DynamicProperties_ToString())
        {
            optionTexts[ct].transform.parent.gameObject.SetActive(true);
            optionTexts[ct++].text = v;
        }

        costPanel.text = enchantCost.ToString();
    }

    enum FailCase
    {
        ���_���õ���_�ʾҽ��ϴ�,
        �ɼ���_���õ���_�ʾҽ��ϴ�,
        �ɼ���_���õ�_����Դϴ�,
        Ŭ����_�����մϴ�
    }

    public void SelectOptionToChange(int index)
    {
        if (selectedItem == null || equipment == null)
            EnchantFail(FailCase.���_���õ���_�ʾҽ��ϴ�);
        else if (equipment.ChangedOption.Stat != Ability.Stat.EnumTotal || equipment.ChangedOption.How != Ability.Enhance.EnumTotal)
            EnchantFail(FailCase.�ɼ���_���õ�_����Դϴ�);
        else
        {
            equipment.ChangedOption = optionDatas[index];
            SelectItem(selectedItem);
            UI_MessageBox.Message("�ɼ��� ���õǾ����ϴ�.");
        }
    }


    public void ChangeSelectedOption()
    {
        if (selectedItem == null || equipment == null)
            EnchantFail(FailCase.���_���õ���_�ʾҽ��ϴ�);
        else if (equipment.ChangedOption.Stat == Ability.Stat.EnumTotal || equipment.ChangedOption.How == Ability.Enhance.EnumTotal)
            EnchantFail(FailCase.�ɼ���_���õ���_�ʾҽ��ϴ�);
        else if (GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>().Crystal < enchantCost)
            EnchantFail(FailCase.Ŭ����_�����մϴ�);
        else
        {
            if (EquipmentDealer.TryChangeDynamic(equipment, EquipmentDealer.CandidateDynamicOption(equipment)))
            {
                GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>().AddCrystal(-enchantCost);
                SelectItem(selectedItem);   // refresh
            }
            else
                Debug.LogError("Logic error in NPCEnchant : SelectOptionToChange");
        }

        //EquipmentDealer.ShuffleDynamics(equipment);
        //SelectItem(selectedItem);
    }


    void EnchantFail(FailCase reason)
    {
        switch (reason)
        {
            default:
                OnEnable(); // reset panel
                UI_MessageBox.Message($"��þƮ ���� : {reason.ToString()}");
                break;
        }
    }

}
