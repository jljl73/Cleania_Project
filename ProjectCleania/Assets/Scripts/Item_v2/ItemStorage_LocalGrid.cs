using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ItemStorage_LocalGrid
{
    public struct GriddedItem
    {
        ItemInstance ItemData;
        Point Location;
    }

    [SerializeField]
    Size gridSize;
    public Size GridSize
    { get => gridSize; }

    [SerializeField]
    List<GriddedItem> SD_items;

}
