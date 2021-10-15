using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public ItemData()
    {

    }

    public ItemData(ItemSO itemSO)
    {
        idea = itemSO;
    }

    [SerializeField]
    protected ItemSO idea;
    [SerializeField]
    protected int count;
}
