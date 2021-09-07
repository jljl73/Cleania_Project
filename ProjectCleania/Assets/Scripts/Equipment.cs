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
        Shotes,
        EnumTotal
    }

    public Type equipmentType = Type.MainWeapon;
    public float atk = 0;
    public float atkPerSecond = 1.0f;
    public float def = 0;
    public float strength = 0;

    public Dictionary<StatusOption.Option, float> options;
}
