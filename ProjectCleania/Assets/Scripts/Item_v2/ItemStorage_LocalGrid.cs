using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ItemStorage_LocalGrid : ItemStorage
{
    [SerializeField]
    Size gridSize;
    public Size GridSize
    { get => gridSize; }




        // SAVE DATA IMPLEMENTATION

    public struct GriddedItem
    {
        ItemInstance ItemData;
        Point Location;
    }

    [SerializeField]
    List<GriddedItem> SD_items;

}
