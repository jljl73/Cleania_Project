using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCRepair : MonoBehaviour
{
    UI_ItemController selectedItem;
    [SerializeField]
    Image backgroundImage;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    Text selectedCost;

    private void OnEnable()
    {
        selectedItem = null;
        itemImage.enabled = false;
        backgroundImage.enabled = false;
        selectedCost.text = "-";
    }

    public void SelectItem(UI_ItemController item)
    {
        if (item == null || item.itemInstance == null)
            return;

        selectedItem = item;
        itemImage.enabled = true;
        itemImage.sprite = selectedItem.itemInstance.SO.ItemImage;
        backgroundImage.enabled = true;
        //backgroundImage.sprite = ;

        if ((ItemInstance_Equipment)selectedItem.itemInstance is ItemInstance_Equipment)
            selectedCost.text = EquipmentDealer.GetRepairCost((ItemInstance_Equipment)selectedItem.itemInstance).ToString();
        else
            selectedCost.text = "-";
    }

    public void RepairSelected()
    {
        if (selectedItem == null || !(selectedItem.itemInstance is ItemInstance_Equipment))
            return;

        ItemInstance_Equipment item = (ItemInstance_Equipment)selectedItem.itemInstance;

        if (EquipmentDealer.TryRepair(item, GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>()))
        {   // repair success
            // tryRepair change money ammount
            SelectItem(selectedItem);   // refresh
            UI_MessageBox.Show("���õ� ����� ������ �Ϸ��Ͽ����ϴ�.");
        }
        else 
        {   // repair fail
            UI_MessageBox.Show("Ŭ���� �����մϴ�.");
        }
    }

    public void RepairAll()
    {
        int totalCost = 0;
        List<ItemInstance_Equipment> toRepair = new List<ItemInstance_Equipment>();
        Equipable equiped = GameManager.Instance.PlayerEquipments;

        foreach(var i in SavedData.Instance.Item_Inventory.Items)
        {
            if(i.Key is ItemInstance_Equipment)
            {
                totalCost += EquipmentDealer.GetRepairCost((ItemInstance_Equipment)i.Key);
                toRepair.Add((ItemInstance_Equipment)i.Key);
            }
        }
        for (ItemInstance_Equipment.Type i = ItemInstance_Equipment.Type.MainWeapon; i < ItemInstance_Equipment.Type.EnumTotal; ++i)
        {
            if(equiped[i] != null)
            {
                totalCost += EquipmentDealer.GetRepairCost(equiped[i]);
                toRepair.Add(equiped[i]);
            }
        }

        if (totalCost <= GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>().Crystal)
        {
            foreach(var i in toRepair)
            {
                EquipmentDealer.TryRepair(i, GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>());
            }
            SelectItem(selectedItem);   // refresh
            UI_MessageBox.Show("��� ����� ������ �Ϸ��Ͽ����ϴ�.");
        }
        else
        {
            Debug.Log("Ŭ���� �����մϴ�.");
        }
    }

    public void RepairEquipment()
    {
        int totalCost = 0;
        List<ItemInstance_Equipment> toRepair = new List<ItemInstance_Equipment>();
        Equipable equiped = GameManager.Instance.PlayerEquipments;

        for (ItemInstance_Equipment.Type i = ItemInstance_Equipment.Type.MainWeapon; i < ItemInstance_Equipment.Type.EnumTotal; ++i)
        {
            if (equiped[i] != null)
            {
                totalCost += EquipmentDealer.GetRepairCost(equiped[i]);
                toRepair.Add(equiped[i]);
            }
        }

        if (totalCost <= GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>().Crystal)
        {
            foreach (var i in toRepair)
            {
                EquipmentDealer.TryRepair(i, GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>());
            }
            SelectItem(selectedItem);   // refresh
            UI_MessageBox.Show("�������� ����� ������ �Ϸ��Ͽ����ϴ�.");
        }
        else
        {
            UI_MessageBox.Show("Ŭ���� �����մϴ�.");
        }
    }
}
