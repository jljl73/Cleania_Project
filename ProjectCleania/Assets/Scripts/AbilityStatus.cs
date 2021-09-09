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
    public float HP;
    float _MP = 100;
    public float MP;

    private void Awake()
    {
        status = GetComponent<Status>();
        equipments = GetComponent<EquipmentSlot>();
        buffs = GetComponent<BuffManager>();

        //RefreshAll();
        for(Ability.Stat i = 0; i < Ability.Stat.EnumTotal; ++i)
        {
            RefreshStat(i);
        }
    }



    float RefreshStat(Ability.Stat stat)
    {
        if (status == null)
            return -1;

        _stats[(int)stat] = status[stat];       // default status

        if(equipments != null)                  // equipments stat & magic adjust
        {
            for(Ability.Enhance i = 0; i < Ability.Enhance.EnumTotal; ++i)
            {
                switch(i)
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

                    case Ability.Enhance.Addition:
                        if (buffs != null)
                        {
                            switch (stat)
                            {
                                case Ability.Stat.MoveSpeed:
                                    _stats[(int)stat] *= buffs[Ability.Buff.MoveSpeed_Buff];
                                    break;
                                case Ability.Stat.AttackSpeed:
                                    _stats[(int)stat] *= buffs[Ability.Buff.AttackSpeed_Buff];
                                    break;
                                case Ability.Stat.Attack:
                                    _stats[(int)stat] *= buffs[Ability.Buff.Attack_Buff];
                                    break;
                                case Ability.Stat.Defense:
                                    _stats[(int)stat] *= buffs[Ability.Buff.Defense_Buff];
                                    break;

                                default:
                                    break;
                            }
                        }
                        _stats[(int)stat] += equipments[stat, i];
                        break;

                    default:
                        // error code
                        break;
                }
            }
        }

        return _stats[(int)stat];
    }

    public float TotalDamage()
    {
        float value = this[Ability.Stat.Attack] * (1 + this[Ability.Stat.Strength] * 0.01f);

        if (Random.Range(0, 1) < this[Ability.Stat.CriticalChance])
            value *= this[Ability.Stat.CriticalScale];

        return value;
    }

    public float AttackedBy(AbilityStatus other, float skillScale)      // returns reduced HP value
    {
        float value = other.TotalDamage() * skillScale;

        value *= (1 - this[Ability.Stat.Defense] / (300 + this[Ability.Stat.Defense]));     // defense adjust

        if (other[Ability.Stat.Accuracy] - this[Ability.Stat.Dodge] < Random.Range(0, 1))   // if dodge success, damage == 0
            return 0;

        _HP -= value;

        return value;
    }

    public void ConsumeMP(float usedMP)
    {
        _MP -= usedMP;
    }

    public float getStat(Ability.Stat stat)
    {
        return this[stat];
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //        Debug.Log(TotalDamage());
    //}
}
