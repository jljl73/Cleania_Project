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

    public enum SyncType
    {
        SelectOne,
        Inventory,
        Storage,
        Equipment
    }

    [SerializeField]
    SyncType syncWith;
    public SyncType SyncWith
    { get => syncWith; }

    public int this[UI_ItemController controller]
    {
        get
        {
            for (int i = 0; i < controllers.Length; ++i)
                if (controllers[i] == controller)
                    return i;

            return -1;
        }
    }

    public UI_ItemController this[int index]
    {
        get
        {
            if (index >= 0 && index < controllers.Length)
                return controllers[index];
            else
                return null;
        }
    }



    private void Awake()
    {
        controllers = new UI_ItemController[slotParent.transform.childCount];
    }

    private void Start()
    {
        switch(syncWith)
        {
            case SyncType.Inventory:
                SavedData.Instance.Item_Inventory.Subscribe(Synchronize, Point.Empty);
                break;
            case SyncType.Storage:
                SavedData.Instance.Item_Storage.Subscribe(Synchronize, Point.Empty);
                break;
            case SyncType.Equipment:
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
            switch (syncWith)
            {
                case SyncType.Inventory:
                    return AddSync(SavedData.Instance.Item_Inventory, item, index);
                case SyncType.Storage:
                    return AddSync(SavedData.Instance.Item_Storage, item, index);
                case SyncType.Equipment:
                    return AddSync(SavedData.Instance.Item_Equipments, item, index);
            }

        return AddAsync(item, index);
    }
    public bool Add(UI_ItemController controller, int index = -1, bool sync = true)
    {
        ItemInstance item = controller.itemInstance;

        if (controller.currentContainer != null)
            controller.currentContainer.Remove(controller);

        return Add(item, index, sync);
    }


    public bool Remove(int index, bool sync = true)
    {
        if (sync)
            switch (syncWith)
            {
                case SyncType.Inventory:
                    return RemoveSync(SavedData.Instance.Item_Inventory, index);
                case SyncType.Storage:
                    return RemoveSync(SavedData.Instance.Item_Storage, index);
                case SyncType.Equipment:
                    return RemoveSync(SavedData.Instance.Item_Equipments, index);
            }

        return RemoveAsync(index);
    }
    public bool Remove(UI_ItemController controller, bool sync = true)
    {
        int index = this[controller];

        if (index != -1)
            return Remove(index, sync);
        else
            return false;
    }



    public bool ImmigrateTo(UI_ItemController controller, UI_ItemContainer dstContainer, int dstIndex)
    {
        if (controller == null || controller.itemInstance == null)
            return false;
        //if (dstContainer.syncWith == SyncType.Equipment)
        //    return false;

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



    public int GetNumberItem(int itemCode)
    {
        int sum = 0;
        for (int i = 0; i < controllers.Length; ++i)
        {
            if (controllers[i] != null && controllers[i].itemInstance.SO.ID == itemCode)
            {
                Debug.Log(controllers[i].itemInstance.SO.ItemName);
                sum += controllers[i].itemInstance.Count;
            }
        }
        Debug.Log(sum);
        return sum;
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
                LoadControllers(sender);
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
                LoadControllers(sender);
                break;

            default:
                Debug.LogError("Logic error in UI_ItemContainer : Synchronize");
                break;
        }
    }


    void LoadControllers(iItemStorage storage)
    {
        for (int i = slotParent.transform.childCount - 1; i >= 0; i--)
            if (controllers[i] != null)
                RemoveAsync(i);

        switch (SyncWith)
        {
            case SyncType.Inventory:
            case SyncType.Storage:
                foreach (var i in ((ItemStorage_LocalGrid)storage).Items)
                {
                    AddAsync(i.Key, ((ItemStorage_LocalGrid)storage).PointToIndex(i.Value));
                }
                break;
            case SyncType.Equipment:
                foreach (var i in ((ItemStorage_Equipments)storage).Items)
                {
                    AddAsync(i.Key, (int)i.Value);
                }
                break;
        }
    }

    void SelfLoad()
    {
        switch (SyncWith)
        {
            case SyncType.Inventory:
                LoadControllers(SavedData.Instance.Item_Inventory);
                break;
            case SyncType.Storage:
                LoadControllers(SavedData.Instance.Item_Storage);
                break;
            case SyncType.Equipment:
                LoadControllers(SavedData.Instance.Item_Equipments);
                break;
        }
    }

    bool AddSync(iItemStorage storage, ItemInstance item, int index)
    {
        if (index >= 0 && index < slotParent.transform.childCount)
            switch (syncWith)
            {
                case SyncType.Inventory:
                case SyncType.Storage:
                    return ((ItemStorage_LocalGrid)storage).Add(item, index);
                case SyncType.Equipment:
                    return ((ItemStorage_Equipments)storage).Add(item);
                default:
                    Debug.LogError("Logic error in UI_ItemContainer : AddSync");
                    return false;
            }
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
        else if(index == -1)
        {
            for (int i = 0; i < controllers.Length; ++i)
                if (controllers[i] == null)
                {
                    controllers[i] = UI_ItemController.New(item, this, i);
                    return true;
                }

            return false;
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
