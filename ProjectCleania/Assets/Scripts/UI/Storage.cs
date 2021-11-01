using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    public GameObject slotParent;
    public GameObject[] slots;
    public Transform ItemList;


    [SerializeField]
    int crystal = 0;
    public int Crystal { get { return crystal; } }
    [SerializeField]
    Text TextCrystal;

 
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
    public ItemController_v2[] items
    { get; private set; }

    protected void Awake()
    {
        nSize = slotParent.transform.childCount;
        items = new ItemController_v2[nSize];

        for (int i = 0; i < nSize; ++i)
        {
            items[i] = null;
        }
        //gameObject.SetActive(false);
        TextCrystal.text = crystal.ToString();
    }
        


    private void Start()
    {
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

        Invoke("LoadItemControllers", 0.2f);
    }

    // ÀÚµ¿ Ãß°¡
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
                myLocalGrid.Add(item.itemInstance, i);
                //</Modified>

                return;
            }
        }
        index = -1;
    }

    // ÁöÁ¤ Ãß°¡
    public bool Add(ItemController_v2 item, out int index, int HopeIndex)
    {
        if (items[HopeIndex] == null)
        {
            items[HopeIndex] = item;
            ChangeParent(item);
            index = HopeIndex;
            items[HopeIndex].MoveTo(slotParent.transform.GetChild(HopeIndex).position);

            //<Modified>
            myLocalGrid.Add(item.itemInstance, HopeIndex);
            //</Modified>

            return true;
        }
        else
        {
            index = -1;
            return false;
        }
    }

    public void Move(int src, int dest)
    {
        // swap object
        ItemController_v2 temp = items[dest];
        items[dest] = items[src];
        items[src] = temp;

        if (items[dest] != null)
        {
            items[dest].MoveTo(slotParent.transform.GetChild(dest).position);
            //items[dest].prevIndex = dest;
        }
        if (items[src] != null)
        {
            items[src].MoveTo(slotParent.transform.GetChild(src).position);
            items[src].prevIndex = src;
        }

        //<Modified>
        myLocalGrid.Swap(src, dest);
        //</Modified>
    }

    public void Remove(int index)
    {
        if (index < 0)
            return;

        items[index] = null;

        //<Modified>
        myLocalGrid.Remove(index);
        //</Modified>
    }

    void ChangeParent(ItemController_v2 item)
    {
        item.transform.SetParent(ItemList);
    }

    public void AddCrystal(int amount)
    {
        crystal += amount;
        if (crystal < 0)
            crystal = 0;
        TextCrystal.text = crystal.ToString();
    }
   
    void LoadItemControllers()
    {
        for (int i = 0; i < nSize; ++i)
        {
            if (items[i] != null)
                ItemController_v2.Delete(items[i]);

            items[i] = null;
        }

        foreach (var i in myLocalGrid.Items)
        {
            ItemController_v2 controller = ItemController_v2.New(i.Key);
            controller.transform.SetParent(ItemContollerParent.transform);

            Add(controller, out controller.prevIndex, myLocalGrid.PointToIndex(i.Value));
        }
    }

}