using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public bool IsActive;// { get; private set; }
    public int index;
    public int Index
    {
        get
        {
            if (isEquipment)
                return index + 1000;
            return index;
        }
    }


    public ItemController itemController { get; private set; }
    public bool isEquipment { get; private set; }

    void Awake()
    {
        IsActive = false;
        index = transform.GetSiblingIndex();
        if (transform.parent.gameObject.name == "Equipment")
            isEquipment = true;
    }

    public void Actvivate(ItemController item)
    {
        itemController = item;
        IsActive = true;
    }

    public void Deactivate()
    {
        itemController = null;
        IsActive = false;
    }

}
