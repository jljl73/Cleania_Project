using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_ArithmeticProgress : Status
{
    public StatusSO_ArithmeticProgress SO;

    [SerializeField, Range(1, 50)]
    int level = 1;
    public override int Level
    { get => level; set => level = value; }

    public override float this[Ability.Stat index]
    {
        get
        {
            switch(index)
            {
                case Ability.Stat.Strength:
                    return SO.Strength + (level - 1) * SO.LevelUpStrength;
                case Ability.Stat.Vitality:
                    return SO.Vitality + (level - 1) * SO.LevelUpVitality;
                case Ability.Stat.Attack:
                    return SO.Atk;
                case Ability.Stat.Defense:
                    return SO.Def;
                case Ability.Stat.CriticalChance:
                    return SO.CriticalChance / 100;
                case Ability.Stat.CriticalScale:
                    return SO.CriticalScale / 100;
                case Ability.Stat.AttackSpeed:
                    return SO.AttackSpeed;
                case Ability.Stat.MoveSpeed:
                    return SO.MoveSpeed;

                case Ability.Stat.Accuracy:
                    return SO.Accuracy / 100;
                case Ability.Stat.Dodge:
                    return SO.Dodge / 100;
                case Ability.Stat.Tenacity:
                    return SO.Tenacity / 100;

                case Ability.Stat.MaxHP:
                    return SO.BasicHP;
                case Ability.Stat.MaxMP:
                    return SO.BasicMP;


                default:
                    return 1;
            }
        }
    }

    //public float Strength = 24;
    //public float levelUpStrength = 4;

    //public float Vitality = 50;
    //public float levelUpVitality = 10;

    //public float Atk = 0;
    //public float Def = 0;
    //public float CriticalChance = 10;
    //public float CriticalScale = 200;
    //public float AttackSpeed = 0.0f;
    //public float MoveSpeed = 1.0f;

    //public float Accuracy = 100;
    //public float Dodge = 10;
    //public float Tenacity = 0;

    //public float BasicHP = 0;
    //public float BasicMP = 100;
}
