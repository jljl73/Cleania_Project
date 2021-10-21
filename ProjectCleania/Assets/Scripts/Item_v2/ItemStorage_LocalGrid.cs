using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[System.Serializable]
public class ItemStorage_LocalGrid : ItemStorage, iSavedData
{
    public ItemStorage_LocalGrid(Size size)
    {
        GridSize = size;
        if (_referenceGrid == null || _referenceGrid.Length != gridSizeY || _referenceGrid[0].Length != gridSizeX)
            _InitGrid();
    }

    [SerializeField]
    int gridSizeX;
    [SerializeField]
    int gridSizeY;
    public Size GridSize
    {
        get => new Size(gridSizeX, gridSizeY);
        private set
        {
            gridSizeX = value.Width;
            gridSizeY = value.Height;
        }
    }

    Dictionary<ItemInstance, Point> _items = new Dictionary<ItemInstance, Point>();
    /// <summary>
    ///  You can't change storage's items with this accessor.<para></para>
    ///  use Add() and Remove() to modify storage.<para></para>
    ///  * created for foreach, search access
    /// </summary>
    public Dictionary<ItemInstance, Point> Items
    { get => new Dictionary<ItemInstance, Point>(_items); }

    ItemInstance[][] _referenceGrid;
    /// <summary>
    /// returns placed ItemInstance of that grid.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public ItemInstance this[int y, int x]
    { get => _referenceGrid[y][x]; }

    /// <summary>
    /// Default Add function.<para></para>
    /// item will be placed in mostly left-upper empty grid.<para></para>
    /// Returns true if ItemStorage had enough space and stored it well.<para></para>
    /// Returns false if there was not enough space or failed to store.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public override bool Add(ItemInstance item)
    {
        if (item == null)
            return false;

        for (int y = 0; y + item.SO.GridSize.Height <= gridSizeY; ++y)
            for (int x = 0; x + item.SO.GridSize.Width <= gridSizeX; ++x)
                if (_IsAreaEmpty(item.SO.GridSize, new Point(x, y)))
                {
                    _Add(item, new Point(x, y));
                    return true;
                }

        return false;
    }
    /// <summary>
    /// You can store item in wanted location with this function.<para></para>
    /// Returns true if that location was not reserved.<para></para>
    /// Returns false if that place(s) has owner already.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    public bool Add(ItemInstance item, Point location)
    {
        if(item == null)
            return false;

        // index range check
        if (location.X + item.SO.GridSize.Width > gridSizeX ||
            location.Y + item.SO.GridSize.Height > gridSizeY ||
            location.X < 0 || location.Y < 0)
            return false;

        // grid reservation check
        if (_IsAreaEmpty(item.SO.GridSize, location) == false)
            return false;

        _Add(item, location);

        return true;
    }
    void _Add(ItemInstance item, Point location)
    {
        item.CurrentStorage = this;

        // reserve grid
        for (int y = 0; y < item.SO.GridSize.Height; ++y)
            for (int x = 0; x < item.SO.GridSize.Width; ++x)
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

    /// <summary>
    /// Default Remove function.<para></para>
    /// ItemStorage will find out 'item' and remove it.<para></para>
    /// Returns true if there was 'item' in storage.<para></para>
    /// Returns false if 'item' wasn't here.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public override bool Remove(ItemInstance item)
    {
        // item check
        if (!_items.ContainsKey(item))
            return false;

        _Remove(item);

        return true;
    }
    /// <summary>
    /// Pinpoint Removing.<para></para>
    /// Returns true if there was item in that place.<para></para>
    /// Returns false if there was nothing.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public bool Remove(Point location)
    {
        if (location.X > gridSizeX || location.Y > gridSizeY ||
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
        if (item.CurrentStorage == this)
            item.CurrentStorage = null;

        // checkout reserve
        Point location = _items[item];
        for (int y = 0; y < item.SO.GridSize.Height; ++y)
            for (int x = 0; x < item.SO.GridSize.Width; ++x)
                _referenceGrid[y + location.Y][x + location.X] = null;

        // remove
        _items.Remove(item);
    }


    void _InitGrid()
    {
        _referenceGrid = new ItemInstance[gridSizeY][];
        for (int i = 0; i < gridSizeY; ++i)
            _referenceGrid[i] = new ItemInstance[gridSizeX];
    }



        // SAVE DATA IMPLEMENTATION

    [System.Serializable]
    public struct Gridded<T>
    {
        public Gridded(T item, Point location)
        {
            ItemData = item;
            LocationX = location.X;
            LocationY = location.Y;
        }

        [SerializeField]
        public T ItemData;
        [SerializeField]
        int LocationX;
        [SerializeField]
        int LocationY;

        public Point Location
        { get => new Point(LocationX, LocationY); }
    }

    [SerializeField]
    List<Gridded<ItemInstance_Equipment>> SD_equipments = new List<Gridded<ItemInstance_Equipment>>();
    [SerializeField]
    List<Gridded<ItemInstance_Etc>> SD_etcs = new List<Gridded<ItemInstance_Etc>>();

    void iSavedData.AfterLoad()
    {
        _InitGrid();

        _items.Clear();

        foreach(Gridded<ItemInstance_Etc> i in SD_etcs)
        {
            ((iSavedData)i.ItemData).AfterLoad();
            _Add(i.ItemData, i.Location);
        }
        foreach (Gridded<ItemInstance_Equipment> i in SD_equipments)
        {
            ((iSavedData)i.ItemData).AfterLoad();
            _Add(i.ItemData, i.Location);
        }

        // SD_etc.Clear();
        // SD_equipment.Clear();
    }

    void iSavedData.BeforeSave()
    {
        SD_etcs.Clear();
        SD_equipments.Clear();

        foreach (var i in _items)
        {
            ((iSavedData)i.Key).BeforeSave();

            switch (i.Key.SO.MainCategory)
            {
                case ItemSO.enumMainCategory.Equipment:
                    SD_equipments.Add(new Gridded<ItemInstance_Equipment>((ItemInstance_Equipment)i.Key, i.Value));
                    break;
                default:
                    SD_etcs.Add(new Gridded<ItemInstance_Etc>((ItemInstance_Etc)i.Key, i.Value));
                       break;
            }
        }
    }

}
