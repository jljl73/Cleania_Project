using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[System.Serializable]
public class ItemStorage_LocalGrid : ItemStorage, iSavedData
{
    public ItemStorage_LocalGrid(Size size)
    {
        gridSize = size;
        if (_referenceGrid == null || new Size(_referenceGrid.Length, _referenceGrid[0].Length) != gridSize)
            _InitGrid();
    }

    [SerializeField]
    Size gridSize;
    public Size GridSize
    { get => gridSize; }

    Dictionary<ItemInstance, Point> _items = new Dictionary<ItemInstance, Point>();
    public Dictionary<ItemInstance, Point> Items
    { get => new Dictionary<ItemInstance, Point>(_items); }

    ItemInstance[][] _referenceGrid;

    public override bool Add(ItemInstance item)
    {
        if (item == null)
            return false;

        for (int y = 0; y + item.Info.GridSize.Height < gridSize.Height; ++y)
            for (int x = 0; x + item.Info.GridSize.Width < gridSize.Width; ++x)
                if (_IsAreaEmpty(item.Info.GridSize, new Point(x, y)))
                {
                    _Add(item, new Point(x, y));
                    return true;
                }

        return false;
    }
    public bool Add(ItemInstance item, Point location)
    {
        if(item == null)
            return false;

        // index range check
        if (location.X + item.Info.GridSize.Width >= gridSize.Width ||
            location.Y + item.Info.GridSize.Height >= gridSize.Height ||
            location.X < 0 || location.Y < 0)
            return false;

        // grid reservation check
        if (_IsAreaEmpty(item.Info.GridSize, location) == false)
            return false;

        _Add(item, location);

        return true;
    }
    void _Add(ItemInstance item, Point location)
    {
        // reserve grid
        for (int y = 0; y < item.Info.GridSize.Height; ++y)
            for (int x = 0; x < item.Info.GridSize.Width; ++x)
                _referenceGrid[y + location.Y][x + location.X] = item;

        // add
        _items.Add(item, location);
    }
    bool _IsAreaEmpty(Size area, Point location)
    {
        for (int y = 0; y < area.Height; ++y)
            for (int x = 0; x < area.Width; ++x)
                if (_referenceGrid[y + location.Y][x + location.X] != null)
                    return false;

        return true;
    }

    public override bool Remove(ItemInstance item)
    {
        // item check
        if (!_items.ContainsKey(item))
            return false;

        _Remove(item);

        return true;
    }
    public bool Remove(Point location)
    {
        if (location.X > gridSize.Width || location.Y > gridSize.Height ||
           location.X < 0 || location.Y < 0)
            return false;

        ItemInstance slotted = _referenceGrid[location.Y][location.X];

        if (slotted == null)
            return false;

        _Remove(slotted);

        return true;
    }
    void _Remove(ItemInstance item)
    {
        Point location = _items[item];

        // checkout reserve
        for (int y = 0; y < item.Info.GridSize.Height; ++y)
            for (int x = 0; x < item.Info.GridSize.Width; ++x)
                _referenceGrid[y + location.Y][x + location.X] = null;

        // remove
        _items.Remove(item);
    }


    void _InitGrid()
    {
        _referenceGrid = new ItemInstance[gridSize.Height][];
        for (int i = 0; i < gridSize.Height; ++i)
            _referenceGrid[i] = new ItemInstance[gridSize.Width];
    }



        // SAVE DATA IMPLEMENTATION

    [System.Serializable]
    public struct GriddedItem
    {
        public GriddedItem(ItemInstance item, Point location)
        {
            ItemData = item;
            Location = location;
        }

        public ItemInstance ItemData;
        public Point Location;
    }

    [SerializeField]
    List<GriddedItem> SD_items = new List<GriddedItem>();

    void iSavedData.AfterLoad()
    {
        _InitGrid();

        _items.Clear();

        foreach(GriddedItem i in SD_items)
        {
            _Add(i.ItemData, i.Location);
        }

        // SD_items.Clear();
    }

    void iSavedData.BeforeSave()
    {
        SD_items.Clear();

        foreach(var i in _items)
        {
            SD_items.Add(new GriddedItem(i.Key, i.Value));
        }
    }

}
