using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRepair : MonoBehaviour
{
    public GameObject slot;
    GameObject Image = null;
    ItemController_v2 selectedItem;

    public void SelectItem(GameObject item)
    {
        if (Image != null) Destroy(Image);
        selectedItem = item.GetComponent<ItemController_v2>();
        // Image 불러오기 수정 필요할수도
        Image = Instantiate(selectedItem.transform.GetChild(0).gameObject, slot.transform);
    }

    public void RepairSelected()
    {
        if (!(selectedItem.itemInstance is ItemInstance_Equipment))
            return;
        ItemInstance_Equipment item = (ItemInstance_Equipment)selectedItem.itemInstance;

        if (EquipmentDealer.TryRepair(item, GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>()))
        {   // repair success
            // tryRepair change money ammount
            Debug.Log("Repaired");
        }
        else // repair fail
        {
            Debug.Log("Not Repaired");
        }
    }

    public void RepairAll()
    {
        int totalCost = 0;
        List<ItemInstance_Equipment> toRepair = new List<ItemInstance_Equipment>();

        foreach(var i in SavedData.Instance.Item_Inventory.Items)
        {
            if(i.Key is ItemInstance_Equipment)
            {
                totalCost += EquipmentDealer.GetRepairCost((ItemInstance_Equipment)i.Key);
                toRepair.Add((ItemInstance_Equipment)i.Key);
            }
        }
        Equipable equiped = GameManager.Instance.PlayerEquipments;
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
            Debug.Log("Repaired all items");
        }
        else
        {
            Debug.Log("Not repaired all items");
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
            Debug.Log("Repaired equiped items");
        }
        else
        {
            Debug.Log("Not repaired equiped items");
        }
    }
}
