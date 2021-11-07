using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemStorage<T> : iItemStorage
{
    protected Dictionary<ItemInstance, T> _items = new Dictionary<ItemInstance, T>();
    /// <summary>
    ///  You can't change storage's items with this accessor.<para></para>
    ///  use Add() and Remove() to modify storage.<para></para>
    ///  * created for foreach, search access
    /// </summary>
    public Dictionary<ItemInstance, T> Items
    { get => new Dictionary<ItemInstance, T>(_items); }


    /// <summary>
    /// Default Add function.<para></para>
    /// This will work for every ItemStorage, but if you want more detailed work, cast this into subclass.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public abstract bool Add(ItemInstance item);

    /// <summary>
    /// Default Remove function.<para></para>
    /// This will work for every ItemStorage, but if you want more detailed work, cast this into subclass.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public abstract bool Remove(ItemInstance item);


    // Synchronize
    public enum SyncOperator
    {
        Refresh,    // 0 operand
        Add,        // 1 operand
        Remove,     // 1 operand
    }
    protected event Action<iItemStorage, SyncOperator, T> SynchronizeEvent;

    protected virtual void OnSynchronize(iItemStorage storage, SyncOperator oper, T operand)
    {
        SynchronizeEvent?.Invoke(storage, oper, operand);
    }
    public virtual void Subscribe(Action<iItemStorage, SyncOperator, T> newDelegate, T garbageParam)
    {
        SynchronizeEvent += newDelegate;
        SynchronizeEvent?.Invoke(this, SyncOperator.Refresh, garbageParam);
    }
    public virtual void ShareSubscribers(ItemStorage<T> other)
    {
        var i = SynchronizeEvent.GetInvocationList();

        foreach (var e in i)
        {
            other.SynchronizeEvent += (Action<iItemStorage, SyncOperator, T>)e;
        }
    }
}
