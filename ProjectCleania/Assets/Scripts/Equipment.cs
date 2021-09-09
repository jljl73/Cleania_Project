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

    public float this[AbilityOption.Equipment index]        // indexer
    {
        get => _options[(int)index];
    }

    public Type equipmentType = Type.MainWeapon;
    public float atk = 0;
    public float atkPerSecond = 1.0f;
    public float def = 0;
    public float strength = 0;

    float[] _options = new float[(int)AbilityOption.Equipment.EnumTotal];

}
