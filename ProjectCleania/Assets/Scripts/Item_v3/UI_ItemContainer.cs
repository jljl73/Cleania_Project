using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemContainer : MonoBehaviour
{
    [SerializeField]
    GameObject slotParent;
    [SerializeField]
    GameObject itemContollerParent;
    public GameObject ItemContollerParent
    { get => itemContollerParent;}

    public enum StorageType
    {
        Inventory,
        Storage,
        Equipment
    }

    [SerializeField]
    StorageType containerType;
    public StorageType ContainerType
    { get => containerType; }
    System.Object originalModel;


    [SerializeField]
    int crystal = 0;
    public int Crystal { get { return crystal; } }
    [SerializeField]
    Text TextCrystal;



    private void Awake()
    {
        if (TextCrystal != null)
            TextCrystal.text = crystal.ToString();

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
                originalModel = SavedData.Instance.Item_Inventory;
                break;
            case StorageType.Storage:
                originalModel = SavedData.Instance.Item_Storage;
                break;
            case StorageType.Equipment:
                originalModel = GameManager.Instance.PlayerEquipments;
                break;

        }
        Invoke("LoadItemControllers", 0.2f);
    }


    void LoadItemControllers()
    {
        switch (ContainerType)
        {
            case StorageType.Inventory:
            case StorageType.Storage:
                foreach (var i in ((ItemStorage_LocalGrid)originalModel).Items)
                {
                    AddAsync(i.Key, ((ItemStorage_LocalGrid)originalModel).PointToIndex(i.Value));
                }
                break;
        }
    }

    bool AddSync(UI_ItemController controller, int index = -1)
    {
        switch(containerType)
        {
            case StorageType.Inventory:
            case StorageType.Storage:
                if (index >= 0 && index < itemContollerParent.transform.childCount)
                    return ((ItemStorage_LocalGrid)originalModel).Add(controller.itemInstance, index);
                else if (index == -1)
                    return ((ItemStorage_LocalGrid)originalModel).Add(controller.itemInstance);
                else
                {
                    Debug.LogError("Logic error in UI_ItemContainer : AddController");
                    return false;
                }

            case StorageType.Equipment:
                ((Equipable)originalModel).Equip((ItemInstance_Equipment)controller.itemInstance);
                return true;

            default:
                return false;
        }
    }

    bool AddAsync(ItemInstance item, int index)
    {
        UI_ItemController controller = UI_ItemController.New(item, this, index);
        if (controller != null)
            return true;
        else return false;
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

// add
// remove
// these only send message to ItemStorage or Equipable.

// LoadItemControllers
