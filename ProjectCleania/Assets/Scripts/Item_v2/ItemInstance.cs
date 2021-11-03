using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class ItemInstance
{
    /// <summary>
    /// [Not public] Use Instantiate() instead.
    /// </summary>
    /// <param name="itemSO"></param>
    protected ItemInstance(ItemSO itemSO, int count = 1)
    {
        so = itemSO;
        this.count = count;
    }

    /// <summary>
    /// Used instead of Constructor.<para></para>
    /// Generate item data with its ScripableObject.<para></para>
    /// returns 'ItemInstance_Equipment' or 'ItemInstance_Etc' or null.
    /// </summary>
    /// <param name="itemSO"></param>
    /// <returns></returns>
    static public ItemInstance Instantiate(ItemSO itemSO, int count = 1)
    {
        if (itemSO == null)
            return null;
        else
            switch (itemSO.MainCategory)
            {
                case ItemSO.enumMainCategory.Equipment:
                    return ItemInstance_Equipment.Instantiate(itemSO);

                default:
                    return ItemInstance_Etc.Instantiate(itemSO, count);
            }
    }
    /// <summary>
    /// Used instead of Constructor.<para></para>
    /// Generate item data with its ID.<para></para>
    /// returns 'ItemInstance_Equipment' or 'ItemInstance_Etc' or null.
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    static public ItemInstance Instantiate(int itemID, int count = 1)
    {
        ItemSO itemSO = ItemSO.Load(itemID);

        if (itemSO == null)
            return null;
        else
            return Instantiate(itemSO, count); // delegate to overload
    }

    /// <summary>
    /// Random instantiate via rank.
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    static public ItemInstance Instantiate_RandomByRank(ItemSO.enumRank rank, int count = 1)
    {
        switch(rank)
        {
            case ItemSO.enumRank.Common:
                return Instantiate(ItemSO.CommonItemSO[Random.Range(0, ItemSO.CommonItemSO.Length)], count);
            case ItemSO.enumRank.Rare:
                return Instantiate(ItemSO.RareItemSO[Random.Range(0, ItemSO.RareItemSO.Length)], count);                
            case ItemSO.enumRank.Legendary:
                return Instantiate(ItemSO.LegendaryItemSO[Random.Range(0, ItemSO.LegendaryItemSO.Length)], count);                
            default:
                return null;
        }
    }

    protected ItemSO so;
    public ItemSO SO
    { get => so; }
    [SerializeField]
    protected int id;
    [SerializeField]
    protected int count;
    public int Count { get { return count; } }
    
    public ItemStorage CurrentStorage;
}
