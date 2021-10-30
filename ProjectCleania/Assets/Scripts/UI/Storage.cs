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
    GameObject controllerPrefab;
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

        controllerPrefab = Resources.Load<GameObject>("Prefabs/ItemController_v2");
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

        Invoke("LoadItemControllers", 1.0f);
    }

    // 자동 추가
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

    // 지정 추가
    public bool Add(ItemController_v2 item, out int index, int HopeIndex)
    {
        if (items[HopeIndex] == null)
        {
            items[HopeIndex] = item;
            ChangeParent(item);
            index = HopeIndex;
            items[HopeIndex].MoveTo(slotParent.transform.GetChild(HopeIndex).position);

            //<Modified>
            myLocalGrid.Add(item.itemInstance,
                new System.Drawing.Point(HopeIndex % myLocalGrid.GridSize.Width, HopeIndex / myLocalGrid.GridSize.Width));
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

    void LoadItemControllers()
    {
        foreach(var i in items)
        {
            if (i != null)
                Destroy(i.gameObject);
        }


        for (int i = 0; i < nSize; ++i)
        {
            items[i] = null;
        }

        foreach (var i in myLocalGrid.Items)
        {
            GameObject newControllerObject = GameObject.Instantiate(controllerPrefab);
            newControllerObject.transform.SetParent(ItemContollerParent.transform);
            ItemController_v2 controller = newControllerObject.GetComponent<ItemController_v2>();

            controller.Initialize(i.Key);
            //Add(controller, out controller.prevIndex, i.Value.Y * myLocalGrid.GridSize.Width + i.Value.X);
        }
    }
}