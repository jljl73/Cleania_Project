using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject slotParent;
    public Transform ItemList;
    public int nColumn = 10;
    int sizeSlots;

    List<int> emptySlots = new List<int>();
    List<int> filledSlots = new List<int>();
    List<int> tempSlots = new List<int>();

    void Awake()
    {
        sizeSlots = slotParent.transform.childCount;
        for (int i = 0; i < sizeSlots; ++i)
        {
            emptySlots.Add(i);
            slots[i] = slotParent.transform.GetChild(i).gameObject;
        }
    }

    
    bool isEmptySlot()
    {
        foreach (int i in tempSlots)
            if (filledSlots.Contains(i)) return false;

        return true;
    }

    void SetItemToSlot(int index, int height, int width)
    {
        filledSlots.AddRange(tempSlots);
        emptySlots = emptySlots.Except(tempSlots).ToList();
    }

    // �ڵ� �߰�
    public GameObject Add(GameObject item, out int index)
    {
        tempSlots.Clear();
        ItemController_v2 controller = item.GetComponent<ItemController_v2>();
        int width = controller.w;
        int height = controller.h;

        for (int i = 0; i < emptySlots.Count; ++i)
        {
            MakeSlotList(i, height, width);
            if (isEmptySlot())
            {
                SetItemToSlot(i, height, width);
                index = tempSlots[0];
                ChangeParent(item);
                return slots[tempSlots[0]];
            }
        }
        index = -1;
        return null;
    }

    // �巡�� �߰�
    public GameObject Add(GameObject item, int index)
    {
        tempSlots.Clear();
        ItemController_v2 controller = item.GetComponent<ItemController_v2>();
        int width = controller.w;
        int height = controller.h;
        MakeSlotList(index, height, width);

        if (isEmptySlot())
        {
            SetItemToSlot(index, height, width);
            ChangeParent(item);
            return slots[tempSlots[0]];
        }

        return null;
    }

    public void Remove(int index, int height, int width)
    {
        MakeSlotList(index, height, width);
        emptySlots.AddRange(tempSlots);
        filledSlots = filledSlots.Except(tempSlots).ToList();
    }

    void MakeSlotList(int index, int height, int width)
    {
        tempSlots.Clear();
        for (int w = 0; w < width; w++)
            for (int h = 0; h < height; h++)
                tempSlots.Add(index + h * nColumn + w);
    }

    void ChangeParent(GameObject item)
    {
        item.transform.SetParent(ItemList);
    }
}