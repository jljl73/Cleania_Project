using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int level = 1;

    public float this[Ability.Stat index]
    {
        get
        {
            switch(index)
            {
                case Ability.Stat.Strength:
                    return Strength + (level - 1) * levelUpStrength;
                case Ability.Stat.Vitality:
                    return Vitality + (level - 1) * levelUpVitality;
                case Ability.Stat.Attack:
                    return Atk;
                case Ability.Stat.Defense:
                    return Def;
                case Ability.Stat.CriticalChance:
                    return CriticalChance / 100;
                case Ability.Stat.CriticalScale:
                    return CriticalScale / 100;
                case Ability.Stat.MoveSpeed:
                    return MoveSpeed;

                case Ability.Stat.Accuracy:
                    return 0;
                case Ability.Stat.Dodge:
                    return 0;
                case Ability.Stat.Toughness:
                    return 0;

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
