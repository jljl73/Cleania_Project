using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCRepair : MonoBehaviour
{
    ItemController_v2 selectedItem;
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

    public void SelectItem(ItemController_v2 item)
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

        if (EquipmentDealer.TryRepair(item, GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>()))
        {   // repair success
            // tryRepair change money ammount
            SelectItem(selectedItem);   // refresh
            UI_MessageBox.Message("선택된 장비의 수리를 완료하였습니다.");
        }
        else 
        {   // repair fail
            UI_MessageBox.Message("클린이 부족합니다.");
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

        if (totalCost <= GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>().Crystal)
        {
            foreach(var i in toRepair)
            {
                EquipmentDealer.TryRepair(i, GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>());
            }
            SelectItem(selectedItem);   // refresh
            UI_MessageBox.Message("모든 장비의 수리를 완료하였습니다.");
        }
        else
        {
            Debug.Log("클린이 부족합니다.");
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

        if (totalCost <= GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>().Crystal)
        {
            foreach (var i in toRepair)
            {
                EquipmentDealer.TryRepair(i, GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>());
            }
            SelectItem(selectedItem);   // refresh
            UI_MessageBox.Message("장착중인 장비의 수리를 완료하였습니다.");
        }
        else
        {
            UI_MessageBox.Message("클린이 부족합니다.");
        }
    }
}
