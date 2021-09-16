using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public enum Size { Height = 8, Width = 10, Area = 80 };

    public GameObject[] slots;
    public GameObject ThrowPanel;
    public GameObject DividePanel;
    public Transform SlotsParent;

    ItemController currentItem;

    private void Awake()
    {
        slots = new GameObject[SlotsParent.childCount];
        for (int i = 0; i < SlotsParent.childCount; i++)
        {
            slots[i] = SlotsParent.GetChild(i).gameObject;
        }
    }


    public GameObject getSlotPosition(int index)
    {
        if (index >= slots.Length)
            return null;
        return slots[index];
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

    void Clone(GameObject originalObject, int count)
    {
        GameObject temp = Instantiate(originalObject);
        currentItem.count -= count;

        if (currentItem.count == 0)
            currentItem.DestroyItem();

        temp.GetComponent<ItemController>().count = count;
    }

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
}
