using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemStorage
{
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
}
