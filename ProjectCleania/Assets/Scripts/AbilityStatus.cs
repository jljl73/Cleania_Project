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

        if (stat == Ability.Stat.Accuracy || stat == Ability.Stat.Dodge)
            _stats[(int)stat] = 1;
        else
            _stats[(int)stat] = status[stat];       // default status

        switch(stat)                            // special values
        {
            case Ability.Stat.Attack:
                _stats[(int)Ability.Stat.Attack] *= 1 + this[Ability.Stat.Strength] * 0.01f;
                break;

            case Ability.Stat.MaxHP:
                _stats[(int)Ability.Stat.MaxHP] += this[Ability.Stat.Vitality] * 100;
                break;

            case Ability.Stat.Defense:
                _stats[(int)Ability.Stat.MaxHP] += this[Ability.Stat.Vitality] * 100;
                break;

            default:
                break;
        }


        if (equipments != null)                  // equipments stat & enchant adjust
        {
            _stats[(int)stat] += equipments[stat];  // equipments stat

            for(Ability.Enhance opt = Ability.Enhance.Absolute; opt < Ability.Enhance.EnumTotal; ++opt)
            {
                switch (opt)
                {
                    case Ability.Enhance.Absolute:
                        _stats[(int)stat] += equipments[stat, opt];
                        break;

                    case Ability.Enhance.NegMul_Percent:
                    case Ability.Enhance.PosMul_Percent:
                    case Ability.Enhance.Addition_Percent:
                        if (equipments[stat, opt] != 0)
                            _stats[(int)stat] *= equipments[stat, opt];
                        break;

                    //case Ability.Enhance.Addition:
                    //    _stats[(int)stat] += equipments[stat, i];         // additional enchant will be adjusted after buff
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
                        _stats[(int)stat] *= buffs[Ability.Buff.MoveSpeed_Buff];
                    break;
                case Ability.Stat.AttackSpeed:
                    //if (buffs[Ability.Buff.AttackSpeed_Buff] != 0)
                        _stats[(int)stat] *= buffs[Ability.Buff.AttackSpeed_Buff];
                    break;
                case Ability.Stat.Attack:
                    //if (buffs[Ability.Buff.Attack_Buff] != 0)
                        _stats[(int)stat] *= buffs[Ability.Buff.Attack_Buff];
                    break;
                case Ability.Stat.Defense:
                    //if (buffs[Ability.Buff.Defense_Buff] != 0)
                        _stats[(int)stat] *= buffs[Ability.Buff.Defense_Buff];
                    break;

                default:
                    break;
            }
        }


        if (equipments != null)
            _stats[(int)stat] += equipments[stat, Ability.Enhance.Addition];


        if (stat == Ability.Stat.Accuracy || stat == Ability.Stat.Dodge)
            _stats[(int)stat] += status[stat] - 1;


        return _stats[(int)stat];
    }

    public void FullHP()
    {
        _HP = this[Ability.Stat.MaxHP];
    }
    public void FullMP()
    {
        _MP = this[Ability.Stat.MaxMP];
    }

    // deprecated function. use AttackedBy() or this[Ability.Stat.Attack].
    public float TotalDamage()
    {
        if (this[Ability.Stat.Accuracy] < Random.Range(0.0f, 1.0f))
            return 0;

        float tot = this[Ability.Stat.Attack];

        if (Random.Range(0.0f, 1.0f) < this[Ability.Stat.CriticalChance])
            tot *= this[Ability.Stat.CriticalScale];

        tot *= this[Ability.Stat.IncreaseDamage];

        return tot;
    }

    public float AttackedBy(AbilityStatus attacker, float skillScale)      // returns reduced HP value
    {
        if (attacker[Ability.Stat.Accuracy] - this[Ability.Stat.Dodge] < Random.Range(0.0f, 1.0f))   // if dodge success, damage == 0
            return 0;

        float finalDamage = attacker[Ability.Stat.Attack] * skillScale;

        if (Random.Range(0.0f, 1.0f) < attacker[Ability.Stat.CriticalChance])
            finalDamage *= attacker[Ability.Stat.CriticalScale];

        finalDamage *= 1 + (attacker[Ability.Stat.IncreaseDamage] - this[Ability.Stat.ReduceDamage]);

        finalDamage *= 1 - this[Ability.Stat.Defense] / (300 + this[Ability.Stat.Defense]);     // defense adjust

        if (_HP > finalDamage)
            _HP -= finalDamage;
        else
            _HP = 0;

        return finalDamage;
    }

    public bool ConsumeMP(float usingHP)
    {
        if (_MP >= usingHP)
        {
            _MP -= usingHP;
            return true;
        }
        else return false;
    }

    public bool ConsumeHP(float usingMP)
    {
        if(_HP > usingMP)
        {
            _HP -= usingMP;
            return true;
        }
        else return false;
    }

    public float getStat(Ability.Stat stat)
    {
        return this[stat];
    }

}
