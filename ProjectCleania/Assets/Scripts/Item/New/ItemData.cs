using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public ItemData(ItemSO itemSO)
    {
        idea = itemSO;
        if (idea != null && idea.MainCategory == ItemSO.enumMainCategory.Equipment)
            equipmentData = new Equipment(idea.OptionTable, 1);
    }

    //[SerializeField]
    //int ID;
    [SerializeField]
    ItemSO idea;
    [SerializeField]
    Equipment equipmentData;
    
}
