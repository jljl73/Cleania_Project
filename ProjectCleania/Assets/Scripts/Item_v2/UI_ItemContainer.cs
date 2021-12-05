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

    public int this[ItemInstance item]
    {
        get
        {
            for (int i = 0; i < controllers.Length; ++i)
                if (controllers[i] != null && controllers[i].itemInstance == item)
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

        Invoke("_SelfLoad", 0.1f);
    }


    public bool Add(ItemInstance item, int index = -1, bool sync = true)
    {
        if (item == null)
            return false;

        if (sync)
            switch (syncWith)
            {
                case SyncType.Inventory:
                    return _AddSync(SavedData.Instance.Item_Inventory, item, index);
                case SyncType.Storage:
                    return _AddSync(SavedData.Instance.Item_Storage, item, index);
                case SyncType.Equipment:
                    return _AddSync(SavedData.Instance.Item_Equipments, item, index);
            }

        return _AddAsync(item, index);
    }
    public bool Add(UI_ItemController controller, int index = -1, bool sync = true)
    {
        ItemInstance item = controller.itemInstance;

        if (controller.currentContainer != null)
            controller.currentContainer.Remove(controller);

        return Add(item, index, sync);
    }
    public bool AddSeparated(ItemInstance item)
    {
        switch(syncWith)
        {
            case SyncType.Inventory:
                return SavedData.Instance.Item_Inventory.AddSeparated(item);
            case SyncType.Storage:
                return SavedData.Instance.Item_Storage.AddSeparated(item);
            default:
                return false;
        }
    }


    public bool Remove(int index, bool sync = true)
    {
        if (sync)
            switch (syncWith)
            {
                case SyncType.Inventory:
                    return _RemoveSync(SavedData.Instance.Item_Inventory, index);
                case SyncType.Storage:
                    return _RemoveSync(SavedData.Instance.Item_Storage, index);
                case SyncType.Equipment:
                    return _RemoveSync(SavedData.Instance.Item_Equipments, index);
            }

        return _RemoveAsync(index);
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
        ItemInstance dstItem = (dstContainer[dstIndex] == null ? null : dstContainer[dstIndex].itemInstance);
        if (srcItem == dstItem)
            return true;

        UI_ItemContainer srcContainer = this;
        int srcIndex = this[controller];


        if (srcIndex == -1)
        {
            Debug.LogError($"Logic error in {ToString()} : ImmigrateTo");
            return false;
        }

        // 1 : remove dst from DST
        if (dstItem != null)
            if (!dstContainer.Remove(dstIndex))
            {
                return false;
            }

        // 2 : remove src from SRC
        if (!srcContainer.Remove(srcIndex))
        {
            if (dstItem != null)
                dstContainer.Add(dstItem, dstIndex);
            return false;
        }

        // 3 : add src to DST
        if (!dstContainer.Add(srcItem, dstIndex))
        {
            srcContainer.Add(srcItem, srcIndex);
            if (dstItem != null)
                dstContainer.Add(dstItem, dstIndex);
            return false;
        }

        // 4 : add dst to SRC
        if (dstItem != null)
            if (!srcContainer.Add(dstItem, srcIndex))
            {
                dstContainer.Remove(dstContainer[srcItem]);
                srcContainer.Add(srcItem, srcIndex);
                if (dstItem != null)
                    dstContainer.Add(dstItem, dstIndex);
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
            Debug.Log(controllers[i]);
        }
        Debug.Log(sum);
        return sum;
    }
}



public partial class UI_ItemContainer
{

    void Synchronize(iItemStorage sender, ItemStorage_LocalGrid.SyncOperator oper, Point index)
    {
        if (this == null)
        {
            ((ItemStorage<Point>)sender).QuitSubscribe(Synchronize);
            return;
        }

        switch(oper)
        {
            case ItemStorage<Point>.SyncOperator.Add:
                {
                    ItemStorage_LocalGrid storage = (ItemStorage_LocalGrid)sender;
                    ItemInstance item = storage[index];
                    _AddAsync(item, storage.PointToIndex(index));
                    break;
                }
            case ItemStorage<Point>.SyncOperator.Remove:
                {
                    ItemStorage_LocalGrid storage = (ItemStorage_LocalGrid)sender;
                    ItemInstance item = storage[index];
                    _RemoveAsync(storage.PointToIndex(index));
                    break;
                }
            case ItemStorage<Point>.SyncOperator.Refresh:
                _LoadControllers(sender);
                break;

            default:
                Debug.LogError("Logic error in UI_ItemContainer : Synchronize");
                break;
        }
    }

    void Synchronize(iItemStorage sender, ItemStorage_Equipments.SyncOperator oper, ItemInstance_Equipment.Type index)
    {
        if(this == null)
        {
            ((ItemStorage<ItemInstance_Equipment.Type>)sender).QuitSubscribe(Synchronize);
            return;
        }

        switch (oper)
        {
            case ItemStorage<ItemInstance_Equipment.Type>.SyncOperator.Add:
                {
                    ItemStorage_Equipments storage = (ItemStorage_Equipments)sender;
                    ItemInstance item = storage[index];
                    _AddAsync(item, (int)index);
                    break;
                }
            case ItemStorage<ItemInstance_Equipment.Type>.SyncOperator.Remove:
                {
                    ItemStorage_Equipments storage = (ItemStorage_Equipments)sender;
                    ItemInstance item = storage[index];
                    _RemoveAsync((int)index);
                    break;
                }
            case ItemStorage<ItemInstance_Equipment.Type>.SyncOperator.Refresh:
                _LoadControllers(sender);
                break;

            default:
                Debug.LogError("Logic error in UI_ItemContainer : Synchronize");
                break;
        }
    }


    void _LoadControllers(iItemStorage storage)
    {
        for (int i = slotParent.transform.childCount - 1; i >= 0; i--)
            if (controllers[i] != null)
                _RemoveAsync(i);

        switch (SyncWith)
        {
            case SyncType.Inventory:
            case SyncType.Storage:
                foreach (var i in ((ItemStorage_LocalGrid)storage).Items)
                {
                    _AddAsync(i.Key, ((ItemStorage_LocalGrid)storage).PointToIndex(i.Value));
                }
                break;
            case SyncType.Equipment:
                foreach (var i in ((ItemStorage_Equipments)storage).Items)
                {
                    _AddAsync(i.Key, (int)i.Value);
                }
                break;
        }
    }

    void _SelfLoad()
    {
        switch (SyncWith)
        {
            case SyncType.Inventory:
                _LoadControllers(SavedData.Instance.Item_Inventory);
                gameObject.SetActive(false);
                break;
            case SyncType.Storage:
                _LoadControllers(SavedData.Instance.Item_Storage);
                gameObject.SetActive(false);
                break;
            case SyncType.Equipment:
                _LoadControllers(SavedData.Instance.Item_Equipments);
                break;
        }
    }

    bool _AddSync(iItemStorage storage, ItemInstance item, int index)
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

    bool _AddAsync(ItemInstance item, int index )
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


    bool _RemoveSync(iItemStorage storage, int index)
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

    bool _RemoveAsync(int index)
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
