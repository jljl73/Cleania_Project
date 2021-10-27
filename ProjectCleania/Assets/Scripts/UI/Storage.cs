using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject slotParent;
    public Transform ItemList;

    [SerializeField]
    Storage otherStorage;
    //public int nSize = 10;
    int nSize;

    public GameObject[] items;

    void Awake()
    {
        nSize = slotParent.transform.childCount;
        items = new GameObject[nSize];

        for (int i = 0; i < nSize; ++i)
        {
            items[i] = null;
            slots[i] = slotParent.transform.GetChild(i).gameObject;
        }
        gameObject.SetActive(false);
    }
        
    // 자동 추가
    public GameObject Add(GameObject item, out int index)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] == null)
            {
                items[i] = item;
                ChangeParent(item);
                index = i;
                return slots[i];
            }
        }
        index = -1;
        return null;
    }

    // 드래그 추가
    public GameObject Add(GameObject item, int index)
    {
        if (items[index] == null)
            items[index] = item;
        else
        {
            Debug.Log("교체");
        }
        ChangeParent(item);
        return slots[index];
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