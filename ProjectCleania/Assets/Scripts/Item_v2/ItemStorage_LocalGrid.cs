using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// Interfaces<para></para>
/// 1. Add(), Remove()<para></para>
/// 2. this[ItemInstance]<para></para>
/// 3. this[int y, int x]<para></para>
/// 4. Item<para></para>
/// </summary>
[System.Serializable]
public partial class ItemStorage_LocalGrid : ItemStorage<Point>, iSavedData, IEnumerable
{
    public ItemStorage_LocalGrid(Size size)
    {
        GridSize = size;
        _InitGrid();
    }

    int gridSizeX;
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

    public Point this[ItemInstance item]
    {
        get
        {
            Point ret = Point.Empty;
            if (item == null)
                return ret;

            _items.TryGetValue(item, out ret);
            return ret;
        }
    }

    ItemInstance[][] _referenceGrid;
    /// <summary>
    /// returns placed ItemInstance of that grid.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public ItemInstance this[int y, int x]
    {
        get
        {
            if (y < 0 || y >= gridSizeY ||
                x < 0 || x >= gridSizeX)
                return null;
            else
                return _referenceGrid[y][x];
        }
    }
    public ItemInstance this[Point pos]
    {
        get => this[pos.Y, pos.X];
    }
    public ItemInstance this[int index]
    {
        get => this[IndexToPoint(index)];
    }


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
    /// Warper of Add(ItemInstance, Point)
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool Add(ItemInstance item, int index)
    {
        return Add(item, IndexToPoint(index));
    }
    /// <summary>
    /// You can store item in wanted location with this function.<para></para>
    /// Returns true if that location was not reserved.<para></para>
    /// Returns false if that place(s) has owner already.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool Add(ItemInstance item, Point position)
    {
        if(item == null)
            return false;

        // index range check
        if (position.X + item.SO.GridSize.Width > gridSizeX ||
            position.Y + item.SO.GridSize.Height > gridSizeY ||
            position.X < 0 || position.Y < 0)
            return false;

        // grid reservation check
        if (_IsAreaEmpty(item.SO.GridSize, position) == false)
            return false;

        _Add(item, position);

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
        if (item == null)
            return false;

        // item check
        if (!_items.ContainsKey(item))
            return false;

        _Remove(item);

        return true;
    }
    /// <summary>
    /// Warper of Remove(Point)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool Remove(int index)
    {
        return Remove(IndexToPoint(index));
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



    public bool SwapSameSize(ItemInstance a, ItemInstance b)
    {
        if (!_items.TryGetValue(a, out Point aPos) ||
            !_items.TryGetValue(b, out Point bPos))
            return false;

        _Remove(a);
        _Remove(b);
        _Add(a, bPos);
        _Add(b, aPos);

        return true;
    }
    public bool Swap(ItemInstance a, ItemInstance b)
    {
        if (a == null || b == null)
            return false;

        // check existance : both a and b should exist in localgrid
        if (!_items.TryGetValue(a, out Point aPos) ||
            !_items.TryGetValue(b, out Point bPos))
            return false;

        // swap start
        _Remove(a);
        _Remove(b);

        // check size
        if ((!_IsAreaEmpty(a.SO.GridSize, bPos)) ||
            (!_IsAreaEmpty(b.SO.GridSize, aPos)))
        {
            _Add(a, aPos);
            _Add(b, bPos);
            return false;
        }

        _Add(a, bPos);
        _Add(b, aPos);

        return true;
    }
    public bool Swap(int aIndex, int bIndex)
    {
        return Swap(IndexToPoint(aIndex), IndexToPoint(bIndex));
    }
    public bool Swap(Point aPos, Point bPos)
    {
        // check index out of range
        if (aPos.X >= gridSizeX || aPos.X < 0 || aPos.Y >= gridSizeY || aPos.Y < 0 ||
            bPos.X >= gridSizeX || bPos.X < 0 || bPos.Y >= gridSizeY || bPos.Y < 0)
            return false;

        ItemInstance a = _referenceGrid[aPos.Y][aPos.X];
        ItemInstance b = _referenceGrid[bPos.Y][bPos.X];

        // correct index & check existance
        bool aExists = false;
        bool bExists = false;
        if (a != null) aExists = _items.TryGetValue(a, out aPos);
        if (b != null) bExists = _items.TryGetValue(b, out bPos);

        // swap start
        if (aExists) _Remove(a);
        if (bExists) _Remove(b);

        // check size
        if ((aExists && !_IsAreaEmpty(a.SO.GridSize, bPos)) ||
            (bExists && !_IsAreaEmpty(b.SO.GridSize, aPos)))
        {
            if (aExists) _Add(a, aPos);
            if (bExists) _Add(b, bPos);
            return false;
        }

        if (aExists) _Add(a, bPos);
        if (bExists) _Add(b, aPos);

        return true;
    }

    public int PointToIndex(Point point)
    {
        return point.Y * gridSizeX + point.X;
    }

    public Point IndexToPoint(int index)
    {
        return new Point(index % gridSizeX, index / gridSizeX);
    }
}





// private
public partial class ItemStorage_LocalGrid
{
    void _Add(ItemInstance item, Point position)
    {
        if (item.CurrentStorage == null)
            item.CurrentStorage = this;
        else
            Debug.Log("Logic error in ItemStorage_LocalGrid : _Add");

        // reserve grid
        for (int y = 0; y < item.SO.GridSize.Height; ++y)
            for (int x = 0; x < item.SO.GridSize.Width; ++x)
                _referenceGrid[y + position.Y][x + position.X] = item;

        // add
        _items.Add(item, position);

        // Sync
        OnSynchronize(this, SyncOperator.Add, position);
    }

    void _Remove(ItemInstance item)
    {
        if (item.CurrentStorage == this)
            item.CurrentStorage = null;
        else
            Debug.Log("Logic error in ItemStorage_LocalGrid : _Remove");

        // checkout reserve
        Point location = _items[item];
        for (int y = 0; y < item.SO.GridSize.Height; ++y)
            for (int x = 0; x < item.SO.GridSize.Width; ++x)
                _referenceGrid[y + location.Y][x + location.X] = null;

        // remove
        _items.Remove(item);

        // Sync
        OnSynchronize(this, SyncOperator.Remove, location);
    }

    bool _IsAreaEmpty(Size area, Point location)
    {
        for (int y = 0; y < area.Height; ++y)
            for (int x = 0; x < area.Width; ++x)
                if (_referenceGrid[y + location.Y][x + location.X] != null)
                    return false;

        return true;
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
        _items.Clear();

        for (int y = 0; y < _referenceGrid.Length; ++y)
            for (int x = 0; x < _referenceGrid[y].Length; ++x)
                _referenceGrid[y][x] = null;

        OnSynchronize(this, SyncOperator.Refresh, Point.Empty);

        foreach (Gridded<ItemInstance_Etc> i in SD_etcs)
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

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)_items).GetEnumerator();
    }
}