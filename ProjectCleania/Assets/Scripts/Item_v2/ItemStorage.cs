using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemStorage
{
    public abstract bool Add(ItemInstance item);

    public abstract bool Remove(ItemInstance item);
}
