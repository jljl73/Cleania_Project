using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    protected ItemInstance()
    {

    }

    protected ItemInstance(ItemSO itemSO)
    {
        idea = itemSO;
    }

    /// <summary>
    /// Used instead of new ItemInstance()
    /// </summary>
    /// <param name="itemSO"></param>
    /// <returns></returns>
    static public ItemInstance Instantiate(ItemSO itemSO)
    {
        switch(itemSO.MainCategory)
        {
            case ItemSO.enumMainCategory.Equipment:
                return ItemInstance_Equipment.Instantiate(itemSO);
               
            default:
                return new ItemInstance(itemSO);
        }
    }
    static public ItemInstance Instantiate(int itemID)
    {
        ItemSO itemSO = Resources.Load<ItemSO>($"ScriptableObject/ItemTable/{itemID.ToString()}");

        if (itemSO == null)
            return null;

        switch((ItemSO.enumMainCategory)(itemID / 1000000))
        {
            case ItemSO.enumMainCategory.Equipment:
                return ItemInstance_Equipment.Instantiate(itemSO);
               
            default:
                return new ItemInstance(itemSO);
        }
    }

    [SerializeField]
    protected ItemSO idea;
    public ItemSO Idea
    { get => idea; }
    [SerializeField]
    protected int count;
}
