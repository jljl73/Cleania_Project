using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    protected ItemData()
    {

    }

    protected ItemData(ItemSO itemSO)
    {
        idea = itemSO;
    }

    static public ItemData New(ItemSO itemSO)
    {
        switch(itemSO.MainCategory)
        {
            case ItemSO.enumMainCategory.Equipment:
                return Equipment.New(itemSO);
               
            default:
                return new ItemData(itemSO);
        }
    }
    static public ItemData New(int itemID)
    {
        ItemSO itemSO = Resources.Load<ItemSO>($"ScriptableObject/ItemTable/{itemID.ToString()}");

        if (itemSO == null)
            return null;

        switch((ItemSO.enumMainCategory)(itemID / 1000000))
        {
            case ItemSO.enumMainCategory.Equipment:
                return Equipment.New(itemSO);
               
            default:
                return new ItemData(itemSO);
        }
    }

    [SerializeField]
    protected ItemSO idea;
    public ItemSO Idea
    { get => idea; }
    [SerializeField]
    protected int count;
}
