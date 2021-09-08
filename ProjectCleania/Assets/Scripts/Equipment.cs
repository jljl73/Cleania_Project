using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public enum Type
    {
        MainWeapon,
        SubWeapon,
        Hat,
        Top,
        Pants,
        Gloves,
        Shoes,
        EnumTotal
    }

    public float this[int index]        // indexer
    {
        get => _options[(StatusOption.Option)index];
    }

    public Type equipmentType = Type.MainWeapon;
    public float atk = 0;
    public float atkPerSecond = 1.0f;
    public float def = 0;
    public float strength = 0;

    Dictionary<StatusOption.Option, float> _options;

}
