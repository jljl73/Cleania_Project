using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_v2 : MonoBehaviour
{
    private ItemInstance itemData;
    public ItemInstance ItemData
    {
        get => itemData;
        set
        {
            _SetItem(value);
        }
    }
    [System.NonSerialized]
    public ItemStorage_World Parent;


    void _SetItem(ItemInstance item)
    {
        itemData = item;

        if (item != null)
        {
            if (transform.childCount > 0)
                foreach (Transform child in transform)
                    GameObject.Destroy(child.gameObject);

            switch (item.SO.MainCategory)
            {
                case ItemSO.enumMainCategory.Equipment:
                    if (item.SO.SubCategory == ItemSO.enumSubCategory.MainWeapon)
                        GameObject.Instantiate(Resources.Load<GameObject>("External/Cleaning Clutter/Models/dustpan_01"), transform);
                    else
                        GameObject.Instantiate(Resources.Load<GameObject>("External/Cleaning Clutter/Models/bucket_01"), transform);
                    break;
                case ItemSO.enumMainCategory.Etc:
                    GameObject.Instantiate(Resources.Load<GameObject>("External/Cleaning Clutter/Models/trash_bags_01"), transform);
                    break;
            }
        }
    }
}
