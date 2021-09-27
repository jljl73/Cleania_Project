using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInventory : MonoBehaviour
{
    public enum Size { Height = 8, Width = 10, Area = 80 };
    public enum EquipmentType { Helmet = 1, Chest, Gloves, Weapon, Pants, Amulet, Boots };

    public GameObject[] slots;
    public GameObject[] EquipmentSlots;
    public GameObject ThrowPanel;
    public GameObject DividePanel;
    public GameObject InvenAlarmPanel;
    public Transform SlotsParent;

    ItemController currentItem;

    private void Awake()
    {
        slots = new GameObject[SlotsParent.childCount];
        for (int i = 0; i < SlotsParent.childCount; i++)
        {
            slots[i] = SlotsParent.GetChild(i).gameObject;
        }

        Transform Equipment = transform.Find("Equipment");

        EquipmentSlots = new GameObject[Equipment.childCount];
        for (int i = 1; i < EquipmentSlots.Length; i++)
        {
            EquipmentSlots[i] = Equipment.GetChild(i).gameObject;
        }
    }

    public GameObject GetSlotPosition(int index)
    {
        if (index >= slots.Length)
            return null;
        return slots[index];
    }

    public GameObject GetEquipmentSlot(EquipmentType type)
    {
        return EquipmentSlots[(int)type];
    }

    public void ShowThrowPanel(ItemController item)
    {
        currentItem = item;
        //Debug.Log(currentItem.name);
        ThrowPanel.SetActive(true);
    }

    public void ShowDividePanel(ItemController item)
    {
        currentItem = item;
        //Debug.Log(currentItem.name);
        DividePanel.GetComponent<ItemDividePanel>().SetMaxValue(item.count);
        DividePanel.SetActive(true);
    }

    public void ShowInvenAlarmPanel()
    {
        InvenAlarmPanel.SetActive(true);
    }

    void Clone(GameObject originalObject, int count)
    {
        GameObject temp = Instantiate(originalObject);
        temp.GetComponent<ItemController>().Initialize(originalObject.GetComponent<ItemController>().GetItem);
        currentItem.count -= count;

        if (currentItem.count == 0)
            currentItem.DestroyItem();

        temp.GetComponent<ItemController>().count = count;
    }

    //public void GenerateItem(string objectName, Item item)
    //{
    //    GameObject newItem = Instantiate(Resources.Load("Prefabs/" + objectName, typeof(GameObject)) as GameObject, transform.position, transform.rotation);

    //    //GameObject newItem = Instantiate(prefab);
    //    newItem.GetComponent<ItemController>().Initialize(this, transform.parent.GetComponent<Canvas>(), );
    //    newItem.SetActive(true);
    //}


    public void OnThrowOK()
    {
        //Debug.Log(currentItem.name);
        if(currentItem)
            currentItem.BackToField();
        currentItem = null;
    }

    public void OnDivideOK(int value)
    {
        //Debug.Log(currentItem.name);
        if (currentItem)
            Clone(currentItem.gameObject, value);
        currentItem = null;
    }

    public void OnAlarmOk()
    {
        InvenAlarmPanel.SetActive(false);
    }

}