using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public GameObject slotParent;
    public GameObject[] slots;
    public Transform ItemList;

    // <Modified>
    enum StorageType
    {
        Inventory,
        Storage
    }

    [SerializeField]
    StorageType LinkedStorage;
    ItemStorage_LocalGrid myLocalGrid;
    public GameObject ItemContollerParent;
    // </Modified>


    [SerializeField]
    Storage otherStorage;
    //public int nSize = 10;
    int nSize;

    [SerializeField]
    ItemController_v2[] items;

    protected void Awake()
    {
        nSize = slotParent.transform.childCount;
        items = new ItemController_v2[nSize];

        for (int i = 0; i < nSize; ++i)
        {
            items[i] = null;
        }
        //gameObject.SetActive(false);

        //<>
        switch(LinkedStorage)
        {
            case StorageType.Inventory:
                GameManager.Instance.uiManager.InventoryPanel = gameObject;
                myLocalGrid = SavedData.Instance.Item_Inventory;
                break;
            case StorageType.Storage:
                GameManager.Instance.uiManager.StoragePanel = gameObject;
                myLocalGrid = SavedData.Instance.Item_Storage;
                break;
        }
        //</>
    }
        
    // �ڵ� �߰�
    public void Add(ItemController_v2 item, out int index)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] == null)
            {
                items[i] = item;
                ChangeParent(item);
                index = i;
                items[i].MoveTo(slotParent.transform.GetChild(i).position);

                //<Modified>
                myLocalGrid.Add(item.itemInstance,
                    new System.Drawing.Point(i % myLocalGrid.GridSize.Width, i / myLocalGrid.GridSize.Width));
                //</Modified>

                return;
            }
        }
        index = -1;
    }

    public void Move(int src, int dest)
    {
        ItemController_v2 temp = items[dest];
        items[dest] = items[src];
        items[src] = temp;
        items[dest].MoveTo(slotParent.transform.GetChild(dest).position);
        if (items[src] != null)
        {
            items[src].MoveTo(slotParent.transform.GetChild(src).position);
            items[src].prevIndex = src;
        }
    }

    public void Remove(int index)
    {
        items[index] = null;
    }

    void ChangeParent(ItemController_v2 item)
    {
        item.transform.SetParent(ItemList);
    }
}