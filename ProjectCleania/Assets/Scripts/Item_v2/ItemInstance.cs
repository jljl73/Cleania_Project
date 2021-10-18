using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ItemInstance
{
    protected ItemInstance(ItemSO itemSO)
    {
        info = itemSO;
    }

    /// <summary>
    /// Used instead of new ItemInstance()
    /// </summary>
    /// <param name="itemSO"></param>
    /// <returns></returns>
    static public ItemInstance Instantiate(ItemSO itemSO)
    {
        if (itemSO == null)
            return null;
        else
            switch (itemSO.MainCategory)
            {
                case ItemSO.enumMainCategory.Equipment:
                    return ItemInstance_Equipment.Instantiate(itemSO);

                default:
                    return new ItemInstance_Etc(itemSO);
            }
    }
    static public ItemInstance Instantiate(int itemID)
    {
        ItemSO itemSO = ItemSO.Load(itemID);

        if (itemSO == null)
            return null;
        else
            return Instantiate(itemSO); // delegate to overload
    }

    [SerializeField]
    protected ItemSO info;
    public ItemSO Info
    { get => info; }
    [SerializeField]
    protected int count;


}
