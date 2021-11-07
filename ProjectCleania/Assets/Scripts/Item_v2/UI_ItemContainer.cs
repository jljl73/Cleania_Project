using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public partial class UI_ItemContainer : MonoBehaviour
{
    [SerializeField]
    GameObject slotParent;
    [SerializeField]
    GameObject itemContollerParent;
    public GameObject ItemContollerParent
    { get => itemContollerParent;}

    private UI_ItemController[] controllers;

    public enum StorageType
    {
        SelectOne,
        Inventory,
        Storage,
        Equipment
    }

    [SerializeField]
    StorageType containerType;
    public StorageType ContainerType
    { get => containerType; }


    [SerializeField]
    int crystal = 0;
    public int Crystal { get { return crystal; } }
    [SerializeField]
    Text TextCrystal;



    private void Awake()
    {
        if (TextCrystal != null)
            TextCrystal.text = crystal.ToString();

        controllers = new UI_ItemController[slotParent.transform.childCount];

        //switch (containerType)
        //{
        //    case StorageType.Inventory:
        //        GameManager.Instance.uiManager.InventoryPanel = this;
        //        break;
        //    case StorageType.Storage:
        //        GameManager.Instance.uiManager.StoragePanel = this;
        //        break;
        //    case StorageType.Equipment:
        //        //GameManager.Instance.uiManager.EqupmentPanel = this;
        //        //myLocalGrid = SavedData.Instance.Item_Storage;
        //        break;
        //}
    }

    private void Start()
    {
        switch(containerType)
        {
            case StorageType.Inventory:
                SavedData.Instance.Item_Inventory.Subscribe(Synchronize, Point.Empty);
                break;
            case StorageType.Storage:
                SavedData.Instance.Item_Storage.Subscribe(Synchronize, Point.Empty);
                break;
            case StorageType.Equipment:
                SavedData.Instance.Item_Equipments.Subscribe(Synchronize, ItemInstance_Equipment.Type.EnumTotal);
                break;

        }
        Invoke("LoadItemControllers", 0.2f);
    }

    public bool Add(ItemInstance item, int index = -1, bool sync = true)
    {
        if (item == null)
            return false;

        if (sync)
            switch (containerType)
            {
                case StorageType.Inventory:
                    return AddSync(SavedData.Instance.Item_Inventory, item, index);
                case StorageType.Storage:
                    return AddSync(SavedData.Instance.Item_Storage, item, index);
                case StorageType.Equipment:
                    return AddSync(SavedData.Instance.Item_Equipments, item, index);

                default:
                    return false;
            }
        else
            return AddAsync(item, index);
    }

    public bool Remove(ItemInstance item, int index = -1, bool sync = true)
    {


        return false;
    }

    



    public bool ImmigrateTo(UI_ItemController controller, UI_ItemContainer otherContainer, int index)
    {
        // remove destination
        switch(otherContainer.containerType)
        {
            case StorageType.Inventory:
            case StorageType.Storage:
                break;

            case StorageType.Equipment:
                break;
        }


        Debug.LogError("Logic error in UI_ItemContainer : MoveTo");
        return false;
    }
}



public partial class UI_ItemContainer
{

    void Synchronize(iItemStorage sender, ItemStorage_LocalGrid.SyncOperator oper, Point index)
    {
        switch(oper)
        {
            case ItemStorage<Point>.SyncOperator.Add:
                ItemStorage_LocalGrid storage = (ItemStorage_LocalGrid)sender;
                ItemInstance item = storage[index];
                AddAsync(item, storage.PointToIndex(index));
                break;
            case ItemStorage<Point>.SyncOperator.Remove:

                break;
            case ItemStorage<Point>.SyncOperator.Refresh:
                LoadItemControllers(sender);
                break;
        }
    }

    void Synchronize(iItemStorage sender, ItemStorage_Equipments.SyncOperator oper, ItemInstance_Equipment.Type index)
    {

    }


    void LoadItemControllers(iItemStorage storage)
    {
        switch (ContainerType)
        {
            case StorageType.Inventory:
            case StorageType.Storage:
                foreach (var i in ((ItemStorage_LocalGrid)storage).Items)
                {
                    AddAsync(i.Key, ((ItemStorage_LocalGrid)storage).PointToIndex(i.Value));
                }
                break;
        }
    }

    bool AddSync(iItemStorage storage, ItemInstance item, int index)
    {
        if (index >= 0 && index < itemContollerParent.transform.childCount)
            return ((ItemStorage_LocalGrid)storage).Add(item, index);
        else if (index == -1)
            return storage.Add(item);
        else
        {
            Debug.LogError("Logic error in UI_ItemContainer : AddSync");
            return false;
        }
    }

    bool AddAsync(ItemInstance item, int index )
    {
        if (index >= 0 && index < slotParent.transform.childCount &&
            controllers[index] == null)
        {
            controllers[index] = UI_ItemController.New(item, this, index);
            return true;
        }
        else
        {
            Debug.LogError("Logic error in UI_ItemContainer : AddAsync");
            return false;
        }
    }


    bool RemoveSync(ItemInstance item)
    {
        return false;
    }

    bool RemoveAsync(ItemInstance item)
    {
        return false;
    }
}
