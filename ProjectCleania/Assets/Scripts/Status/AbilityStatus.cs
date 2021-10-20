using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityStatus : MonoBehaviour, iSavedData
{
    Status status;          // status is essential unlike equips or buffs
    Equipable equipments;
    Buffable buffs;

    protected float[] _stats = new float[(int)Ability.Stat.EnumTotal];

    virtual public float this[Ability.Stat stat]
    {
        get
        {
            return RefreshStat(stat);
        }
    }

    [SerializeField]
    float _HP = 100;
    public float HP
    { get => _HP; }
    [SerializeField]
    float _MP = 100;
    public float MP
    { get => _MP; }

    virtual protected void Awake()
    {
        status = GetComponent<Status>();
        equipments = GetComponent<Equipable>();
        buffs = GetComponent<Buffable>();

        //RefreshAll();
        for (Ability.Stat i = 0; i < Ability.Stat.EnumTotal; ++i)
        {
            RefreshStat(i);
        }
        FullHP();
        FullMP();
    }



    virtual protected float RefreshStat(Ability.Stat stat)
    {
        if (status == null)
            return -1;
        if ((int)stat >= _stats.Length)
            return -1;

        _stats[(int)stat] = status[stat];       // default status

        switch (stat)                            // special values
        {
            case Ability.Stat.Attack:
                _stats[(int)Ability.Stat.Attack] *= 1 + _stats[(int)Ability.Stat.Strength] * 0.01f;
                break;

            case Ability.Stat.MaxHP:
                _stats[(int)Ability.Stat.MaxHP] += _stats[(int)Ability.Stat.Vitality] * 100;
                break;

            //case Ability.Stat.Defense:
            //    _stats[(int)Ability.Stat.MaxHP] += this[Ability.Stat.Vitality] * 100;
            //    break;

            default:
                break;
        }


        if (equipments != null)                  // equipments stat & enchant adjust
        {
            float equipmentsStat = equipments[stat];

            if (!float.IsNaN(equipmentsStat))
                _stats[(int)stat] += equipmentsStat;  // equipments stat

            for (Ability.Enhance opt = (Ability.Enhance)0; opt < Ability.Enhance.EnumTotal; ++opt)
            {
                float equipmentsEnchant = equipments[stat, opt];
                if (!float.IsNaN(equipmentsEnchant))
                    switch (opt)
                    {
                        case Ability.Enhance.Absolute:
                            _stats[(int)stat] += equipmentsEnchant;
                            break;

                        case Ability.Enhance.Chance_Percent:
                            if (equipmentsEnchant > 0.0f && equipmentsEnchant < 1.0f)
                                _stats[(int)stat] += 1 - equipmentsEnchant;
                            break;

                        case Ability.Enhance.NegMul_Percent:
                        case Ability.Enhance.PosMul_Percent:
                        case Ability.Enhance.Addition_Percent:
                            if (equipmentsEnchant > 0)
                                _stats[(int)stat] *= equipmentsEnchant;
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
        {
            float equipmentsAddition = equipments[stat, Ability.Enhance.Addition];
            if (!float.IsNaN(equipmentsAddition))
                _stats[(int)stat] += equipmentsAddition;
        }

        switch(stat)
        {
            case Ability.Stat.Strength:
                RefreshStat(Ability.Stat.Attack);
                break;

            case Ability.Stat.Vitality:
                RefreshStat(Ability.Stat.MaxHP);
                break;
        }

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
    virtual public float DPS()
    {
        float tot = this[Ability.Stat.Attack];

        tot *= this[Ability.Stat.IncreaseDamage];
        tot *= this[Ability.Stat.AttackSpeed];

        return tot;
    }

    virtual public float AttackedBy(AbilityStatus attacker, float skillScale)      // returns reduced HP value
    {
        if (attacker[Ability.Stat.Accuracy] - this[Ability.Stat.Dodge] < Random.Range(0.0f, 1.0f))
            return 0;

        float finalDamage = attacker[Ability.Stat.Attack] * skillScale;

        if (Random.Range(0.0f, 1.0f) < attacker[Ability.Stat.CriticalChance])
            finalDamage *= attacker[Ability.Stat.CriticalScale];

        finalDamage *= 1 + (attacker[Ability.Stat.IncreaseDamage] - this[Ability.Stat.ReduceDamage]);

        finalDamage *= 1 - this[Ability.Stat.Defense] / (300 + this[Ability.Stat.Defense]);     // defense adjust

        //

        if (_HP > finalDamage)
            _HP -= finalDamage;
        else
            _HP = 0;

        return finalDamage;
    }

    public bool ConsumeMP(float usingMP)
    {
        if (_MP >= usingMP)
        {
            _MP -= usingMP;
            return true;
        }
        else return false;
    }

    public bool ConsumeHP(float usingHP)
    {
        if(_HP > usingHP)
        {
            _HP -= usingHP;
            return true;
        }
        else return false;
    }

    public float GetStat(Ability.Stat stat)
    {
        return this[stat];
    }

    virtual public string StatToString(Ability.Stat stat)
    {
        switch(stat)
        {
            case Ability.Stat.Attack:
            case Ability.Stat.AttackSpeed:
            case Ability.Stat.Defense:
            case Ability.Stat.MaxHP:
            case Ability.Stat.MaxMP:
            case Ability.Stat.Strength:
            case Ability.Stat.Vitality:
                return $"{this[stat]}";

            case Ability.Stat.Accuracy:
            case Ability.Stat.CriticalChance:
            case Ability.Stat.CriticalScale:
            case Ability.Stat.Dodge:
            case Ability.Stat.MoveSpeed:
            case Ability.Stat.Tenacity:
                return $"{this[stat] * 100} %";

            case Ability.Stat.ReduceDamage:
            case Ability.Stat.IncreaseDamage:
            case Ability.Stat.SkillCooldown:
                return $"{(1.0f - this[stat]) * 100} %";

            default:
                return "error";
        }
    }

    void iSavedData.AfterLoad()
    {
        print("abilitystatus al");
    }

    void iSavedData.BeforeSave()
    {
        print("abilitystatus bs");
    }
}
