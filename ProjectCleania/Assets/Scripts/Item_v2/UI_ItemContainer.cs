using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public partial class UI_ItemContainer : MonoBehaviour
{
    [SerializeField]
    GameObject slotParent;
    public GameObject SlotParent
    { get => slotParent; }
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

        Invoke("SelfLoad", 0.2f);
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

    public bool Remove(int index, bool sync = true)
    {
        if (sync)
            switch (containerType)
            {
                case StorageType.Inventory:
                    return RemoveSync(SavedData.Instance.Item_Inventory, index);
                case StorageType.Storage:
                    return RemoveSync(SavedData.Instance.Item_Storage, index);
                case StorageType.Equipment:
                    return RemoveSync(SavedData.Instance.Item_Equipments, index);

                default:
                    return false;
            }
        else
            return RemoveAsync(index);
    }





    public bool ImmigrateTo(UI_ItemController controller, UI_ItemContainer dstContainer, int dstIndex)
    {
        if (controller == null || controller.itemInstance == null)
            return false;
        

        ItemInstance srcItem = controller.itemInstance;
        ItemInstance dstItem = (dstContainer.controllers[dstIndex] == null ? null : dstContainer.controllers[dstIndex].itemInstance);
        UI_ItemContainer srcContainer = this;
        int srcIndex = -1;

        if (srcItem == dstItem)
            return true;

        for (int i = 0; i < slotParent.transform.childCount; ++i)
        {
            if (controllers[i] == controller)
            {
                srcIndex = i;
                break;
            }
        }
        if (srcIndex == -1)
        {
            Debug.LogError($"Logic error in {ToString()} : ImmigrateTo");
            return false;
        }


        if (dstItem != null)
            if (!dstContainer.Remove(dstIndex))
            {
                return false;
            }

        if (!srcContainer.Remove(srcIndex))
        {
            if (dstItem != null)
                dstContainer.Add(dstItem, dstIndex);
            return false;
        }

        if (!dstContainer.Add(srcItem, dstIndex))
        {
            if (dstItem != null)
                dstContainer.Add(dstItem, dstIndex);
            srcContainer.Add(srcItem, srcIndex);
            return false;
        }

        if (dstItem != null)
            if (!srcContainer.Add(dstItem, srcIndex))
            {
                dstContainer.Remove(dstIndex);
                if (dstItem != null)
                    dstContainer.Add(dstItem, dstIndex);
                srcContainer.Add(srcItem, srcIndex);
                return false;
            }

        return true;
    }
}



public partial class UI_ItemContainer
{

    void Synchronize(iItemStorage sender, ItemStorage_LocalGrid.SyncOperator oper, Point index)
    {
        switch(oper)
        {
            case ItemStorage<Point>.SyncOperator.Add:
                {
                    ItemStorage_LocalGrid storage = (ItemStorage_LocalGrid)sender;
                    ItemInstance item = storage[index];
                    AddAsync(item, storage.PointToIndex(index));
                    break;
                }
            case ItemStorage<Point>.SyncOperator.Remove:
                {
                    ItemStorage_LocalGrid storage = (ItemStorage_LocalGrid)sender;
                    ItemInstance item = storage[index];
                    RemoveAsync(storage.PointToIndex(index));
                    break;
                }
            case ItemStorage<Point>.SyncOperator.Refresh:
                LoadItemControllers(sender);
                break;

            default:
                Debug.LogError("Logic error in UI_ItemContainer : Synchronize");
                break;
        }
    }

    void Synchronize(iItemStorage sender, ItemStorage_Equipments.SyncOperator oper, ItemInstance_Equipment.Type index)
    {
        switch (oper)
        {
            case ItemStorage<ItemInstance_Equipment.Type>.SyncOperator.Add:
                {
                    ItemStorage_Equipments storage = (ItemStorage_Equipments)sender;
                    ItemInstance item = storage[index];
                    AddAsync(item, (int)index);
                    break;
                }
            case ItemStorage<ItemInstance_Equipment.Type>.SyncOperator.Remove:
                {
                    ItemStorage_Equipments storage = (ItemStorage_Equipments)sender;
                    ItemInstance item = storage[index];
                    RemoveAsync((int)index);
                    break;
                }
            case ItemStorage<ItemInstance_Equipment.Type>.SyncOperator.Refresh:
                LoadItemControllers(sender);
                break;

            default:
                Debug.LogError("Logic error in UI_ItemContainer : Synchronize");
                break;
        }
    }


    void LoadItemControllers(iItemStorage storage)
    {
        for (int i = slotParent.transform.childCount - 1; i >= 0; i--)
            if (controllers[i] != null)
                RemoveAsync(i);

        switch (ContainerType)
        {
            case StorageType.Inventory:
            case StorageType.Storage:
                foreach (var i in ((ItemStorage_LocalGrid)storage).Items)
                {
                    AddAsync(i.Key, ((ItemStorage_LocalGrid)storage).PointToIndex(i.Value));
                }
                break;
            case StorageType.Equipment:
                foreach (var i in ((ItemStorage_Equipments)storage).Items)
                {
                    AddAsync(i.Key, (int)i.Value);
                }
                break;
        }
    }

    void SelfLoad()
    {
        for (int i = slotParent.transform.childCount - 1; i >= 0; i--)
            if (controllers[i] != null)
                RemoveAsync(i);

        switch (ContainerType)
        {
            case StorageType.Inventory:
                foreach (var i in SavedData.Instance.Item_Inventory.Items)
                {
                    AddAsync(i.Key, SavedData.Instance.Item_Inventory.PointToIndex(i.Value));
                }
                break;
            case StorageType.Storage:
                foreach (var i in SavedData.Instance.Item_Storage.Items)
                {
                    AddAsync(i.Key, SavedData.Instance.Item_Storage.PointToIndex(i.Value));
                }
                break;
            case StorageType.Equipment:
                foreach (var i in SavedData.Instance.Item_Equipments.Items)
                {
                    AddAsync(i.Key, (int)i.Value);
                }
                break;
        }
    }

    bool AddSync(iItemStorage storage, ItemInstance item, int index)
    {
        if (index >= 0 && index < slotParent.transform.childCount)
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


    bool RemoveSync(iItemStorage storage, int index)
    {
        if (index >= 0 && index < slotParent.transform.childCount)
        {
            bool result = storage.Remove(controllers[index].itemInstance);
            return result;
        }
        else
        {
            Debug.LogError("Logic error in UI_ItemContainer : RemoveSync");
            return false;
        }
    }

    bool RemoveAsync(int index)
    {
        if (index >= 0 && index < slotParent.transform.childCount)
        {
            if (controllers[index] != null)
            {
                UI_ItemController.Delete(controllers[index]);
                controllers[index] = null;
                return true;
            }
            else
            {
                Debug.Log("Tried to remove empty reference : UI_ItemContainer");
                return false;
            }
        }
        else
        {
            Debug.LogError("Logic error in UI_ItemContainer : AddAsync");
            return false;
        }
    }
}
