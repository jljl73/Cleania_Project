using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance_Etc : ItemInstance
{
    // used for unityengine only
    private ItemInstance_Etc() : base(null)
    { }

    protected ItemInstance_Etc(ItemSO itemSO, int count = 1) : base(itemSO, count)
    {
    }

    /// <summary>
    /// Used instead of Constructor.<para></para>
    /// Generate item data with its ScriptableObject.<para></para>
    /// returns 'ItemInstance_Etc' or null.
    /// </summary>
    /// <param name="itemSO"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    new static public ItemInstance_Etc Instantiate(ItemSO itemSO, int count = 1)
    {
        if (itemSO.OptionTable == null)
            switch (itemSO.MainCategory)
            {
                case ItemSO.enumMainCategory.Equipment:
                    return null;
                default:
                    return new ItemInstance_Etc(itemSO, count);
            }
        else
            return null;
    }
    /// <summary>
    /// Used instead of Constructor.<para></para>
    /// Generate item data with its ID.<para></para>
    /// returns 'ItemInstance_Etc' or null.
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    new static public ItemInstance_Etc Instantiate(int itemID, int count = 1)
    {
        ItemSO itemSO = ItemSO.Load(itemID);

        if (itemSO == null)
            return null;
        else
            return Instantiate(itemSO, count); // delegate to overload
    }
}
