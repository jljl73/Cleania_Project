using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int level = 1;

    public float this[AbilityOption.Stat index]
    {
        get
        {
            switch(index)
            {
                case AbilityOption.Stat.Strength:
                    return Strength + level * levelUpStrength;
                case AbilityOption.Stat.Vitality:
                    return Vitality + level * levelUpVitality;
                case AbilityOption.Stat.Attack:
                    return Atk;
                case AbilityOption.Stat.Defense:
                    return Def;
                case AbilityOption.Stat.CriticalChance:
                    return CriticalChance / 100;
                case AbilityOption.Stat.CriticalScale:
                    return CriticalScale / 100;
                case AbilityOption.Stat.MoveSpeed:
                    return MoveSpeed;

                default:
                    return 0;
            }
        }
    }

    public float Strength = 24;
    public float levelUpStrength = 4;
    public float Vitality = 50;
    public float levelUpVitality = 10;
    public float Atk = 0;
    public float Def = 0;
    public float CriticalChance = 10;
    public float CriticalScale = 200;
    public float MoveSpeed = 1.0f;

}
