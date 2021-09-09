using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatus : MonoBehaviour
{
    Status status;          // status is essential unlike equips or buffs
    EquipmentSlot equipments;
    BuffManager buffs;

    float[] _stats = new float[(int)Ability.Stat.EnumTotal];
    public float this[Ability.Stat stat]
    {
        get
        {
            return RefreshStat(stat);
        }
    }

    float _HP = 100;
    public float HP
    { get => _HP; }
    float _MP = 100;
    public float MP
    { get => _MP; }

    private void Awake()
    {
        status = GetComponent<Status>();
        equipments = GetComponent<EquipmentSlot>();
        buffs = GetComponent<BuffManager>();

        //RefreshAll();
        for (Ability.Stat i = 0; i < Ability.Stat.EnumTotal; ++i)
        {
            RefreshStat(i);
        }
        FullHP();
        FullMP();
    }



    float RefreshStat(Ability.Stat stat)
    {
        if (status == null)
            return -1;

        _stats[(int)stat] = status[stat];       // default status
        switch(stat)
        {
            case Ability.Stat.Attack:
                //_stats[(int)Ability.Stat.Attack] *= 1 + this[Ability.Stat.Strength] * 0.01f;
                break;

            case Ability.Stat.MaxHP:
                //_stats[(int)Ability.Stat.MaxHP] += 
                break;

            default:
                break;
        }


        if (equipments != null)                  // equipments stat & enchant adjust
        {
            _stats[(int)stat] += equipments[stat];  // equipments stat

            for (Ability.Enhance i = 0; i < Ability.Enhance.EnumTotal; ++i) // enchant adjust
            {
                switch (i)
                {
                    case Ability.Enhance.Absolute:
                        _stats[(int)stat] += equipments[stat, i];
                        break;

                    case Ability.Enhance.NegMulti_Percent:
                    case Ability.Enhance.PosMulti_Percent:
                    case Ability.Enhance.Addition_Percent:
                        if (equipments[stat, i] != 0)
                            _stats[(int)stat] *= equipments[stat, i];
                        break;

                    //case Ability.Enhance.Addition:
                    //    _stats[(int)stat] += equipments[stat, i];
                    //    break;

                    default:
                        // error code
                        break;
                }
            }
        }

        if (buffs != null)
        {
            switch (stat)
            {
                case Ability.Stat.MoveSpeed:
                    //if (buffs[Ability.Buff.MoveSpeed_Buff] != 0)
                        _stats[(int)stat] *= 1 + buffs[Ability.Buff.MoveSpeed_Buff];
                    break;
                case Ability.Stat.AttackSpeed:
                    //if (buffs[Ability.Buff.AttackSpeed_Buff] != 0)
                        _stats[(int)stat] *= 1 + buffs[Ability.Buff.AttackSpeed_Buff];
                    break;
                case Ability.Stat.Attack:
                    //if (buffs[Ability.Buff.Attack_Buff] != 0)
                        _stats[(int)stat] *= 1 + buffs[Ability.Buff.Attack_Buff];
                    break;
                case Ability.Stat.Defense:
                    //if (buffs[Ability.Buff.Defense_Buff] != 0)
                        _stats[(int)stat] *= 1 + buffs[Ability.Buff.Defense_Buff];
                    break;

                default:
                    break;
            }
        }

        

        return _stats[(int)stat];
    }

    void FullHP()
    {
        _HP = this[Ability.Stat.MaxHP];
    }
    void FullMP()
    {
        _MP = this[Ability.Stat.MaxMP];
    }

    public float TotalDamage()
    {
        //float value = this[Ability.Stat.Attack] * this[Ability.Stat.Strength] * 0.01f;
        float value = this[Ability.Stat.Attack];
        //Debug.Log(this[Ability.Stat.Attack] + " * (1 + " + this[Ability.Stat.Strength] + " * 0.01f)");


        return value;
    }

    public float AttackedBy(AbilityStatus attacker, float skillScale)      // returns reduced HP value
    {
        if (attacker[Ability.Stat.Accuracy] - this[Ability.Stat.Dodge] < Random.Range(0, 1))   // if dodge success, damage == 0
            return 0;

        float finalDamage = attacker.TotalDamage() * skillScale;

        if (Random.Range(0.0f, 1.0f) < attacker[Ability.Stat.CriticalChance])
            finalDamage *= attacker[Ability.Stat.CriticalScale];

        finalDamage *= (1 + (attacker[Ability.Stat.IncreaseDamage] - this[Ability.Stat.ReduceDamage]));

        finalDamage *= (1 - this[Ability.Stat.Defense] / (300 + this[Ability.Stat.Defense]));     // defense adjust


        _HP -= finalDamage;

        return finalDamage;
    }

    public void ConsumeMP(float usedMP)
    {
        _MP -= usedMP;
    }

    public float getStat(Ability.Stat stat)
    {
        return this[stat];
    }

}
