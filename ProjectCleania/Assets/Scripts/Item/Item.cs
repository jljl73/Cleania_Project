using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    static int NextID = 0;

    public enum ITEMRANK { Normal, Rare, Legendary };
    public enum ITEMBIGCATEGORY { Equipment = 1, Material, Etc };
    public enum ITEMSMALLCATEGORY
    {
        Weapon = 101, Hat = 301, Chest = 302, Pants = 303, Hands = 304,
        Boots = 305, Etc = 999
    };


    public int ItemID;
    public ITEMBIGCATEGORY ItemBigCategory;
    public ITEMSMALLCATEGORY ItemSmallCategory;
    public ITEMRANK ItemRank;
    public int ItemNumber = 1;
    public int ItemCode;
    public string ItemName;

    public void CodeParsing(string itemCode, string itemName)
    {
        int n = int.Parse(itemCode.Substring(0, 1));
        ItemBigCategory = (ITEMBIGCATEGORY)n;
        
        n = int.Parse(itemCode.Substring(1, 3));
        ItemSmallCategory = (ITEMSMALLCATEGORY)n;

        n = int.Parse(itemCode.Substring(4, 1));
        ItemRank = (ITEMRANK)n;

        n = int.Parse(itemCode.Substring(5));
        ItemNumber = n;

        ItemID = NextID++;
        ItemCode = int.Parse(itemCode);
        ItemName = string.Copy(itemName);
    }

    public Item DeepCopy()
    {
        Item newItem = new Item
        {
            ItemID = this.ItemID,
            ItemBigCategory = this.ItemBigCategory,
            ItemSmallCategory = this.ItemSmallCategory,
            ItemRank = this.ItemRank,
            ItemNumber = this.ItemNumber,
            ItemName = this.ItemName,
            ItemCode = this.ItemCode
        };

        return newItem;
    }
}
