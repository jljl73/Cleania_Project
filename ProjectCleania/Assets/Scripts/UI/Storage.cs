using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public GameObject slotParent;
    public GameObject[] slots;
    public Transform ItemList;

    [SerializeField]
    Storage otherStorage;
    //public int nSize = 10;
    int nSize;

    [SerializeField]
    GameObject[] items;

    void Awake()
    {
        nSize = slotParent.transform.childCount;
        items = new GameObject[nSize];

        for (int i = 0; i < nSize; ++i)
        {
            items[i] = null;
        }
        //gameObject.SetActive(false);
    }
        
    // �ڵ� �߰�
    public void Add(GameObject item, out int index)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] == null)
            {
                items[i] = item;
                ChangeParent(item);
                index = i;
                items[i].GetComponent<ItemController_v2>().MoveTo(slotParent.transform.GetChild(i).position);
                return;
            }
        }
        index = -1;
    }

    public void Move(int src, int dest)
    {
        GameObject temp = items[dest];
        items[dest] = items[src];
        items[src] = temp;
        items[dest].GetComponent<ItemController_v2>().MoveTo(slotParent.transform.GetChild(dest).position);
        if (items[src] != null)
        {
            items[src].GetComponent<ItemController_v2>().MoveTo(slotParent.transform.GetChild(src).position);
            items[src].GetComponent<ItemController_v2>().prevIndex = src;
        }
    }

    public void Remove(int index)
    {
        items[index] = null;
    }

    void ChangeParent(GameObject item)
    {
        item.transform.SetParent(ItemList);
    }
}