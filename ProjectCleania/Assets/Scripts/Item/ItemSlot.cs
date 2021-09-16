using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public bool IsActive;// { get; private set; }
    public int Index { get; private set; }

    public ItemController itemController { get; private set; }

    void Awake()
    {
        IsActive = false;
        Index = transform.GetSiblingIndex();
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
