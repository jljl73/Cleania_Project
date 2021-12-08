using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatus : MonoBehaviour
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
    }

    private void Start()
    {
        FullHP();
        FullMP();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            FullHP(); FullMP();
        }
    }


    /// <summary>
    /// Orders <para></para>
    /// 1 - Stats <para></para>
    /// 1.1 - Stats : Vitality relate MaxHP <para></para>
    /// 2 - Equipment absolute <para></para>
    /// 2.1 - Equipment options except additional <para></para>
    /// 2.2 - Stats : Strength relate Atk <para></para>
    /// 3 - Buff <para></para>
    /// 4 - Equipment additional <para></para>
    /// 5 - refresh relative <para></para>
    /// </summary>
    /// <param name="stat"></param>
    /// <returns></returns>
    virtual protected float RefreshStat(Ability.Stat stat)
    {
        if (status == null)
            return -1;
        if ((int)stat >= _stats.Length)
            return -1;

        // step 1
        _stats[(int)stat] = status[stat];

        // step 1.1
        switch (stat)
        {
            case Ability.Stat.MaxHP:
                _stats[(int)Ability.Stat.MaxHP] += _stats[(int)Ability.Stat.Vitality] * 100;
                break;

            default:
                break;
        }

        // step 2
        if (equipments != null)
        {
            float equipmentsStat = equipments[stat];

            if (!float.IsNaN(equipmentsStat))
                _stats[(int)stat] += equipmentsStat;  // equipments stat

            // step 2.1
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

        // step 2.2
        switch (stat)
        {
            case Ability.Stat.Attack:
                _stats[(int)Ability.Stat.Attack] *= 1 + _stats[(int)Ability.Stat.Strength] * 0.01f;
                break;

            default:
                break;
        }

        // step 3
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
                case Ability.Stat.Accuracy:
                    //if (buffs[Ability.Buff.Defense_Buff] != 0)
                    _stats[(int)stat] *= buffs[Ability.Buff.Accuracy_Buff];
                    break;
                case Ability.Stat.CriticalChance:
                    //if (buffs[Ability.Buff.Defense_Buff] != 0)
                    _stats[(int)stat] *= buffs[Ability.Buff.CriticalChance_Buff];
                    break;

                default:
                    break;
            }
        }

        // step 4
        if (equipments != null)
        {
            float equipmentsAddition = equipments[stat, Ability.Enhance.Addition];
            if (!float.IsNaN(equipmentsAddition))
                _stats[(int)stat] += equipmentsAddition;
        }

        // step 5
        switch(stat)
        {
            case Ability.Stat.Strength:
                RefreshStat(Ability.Stat.Attack);
                break;

            case Ability.Stat.Vitality:
                RefreshStat(Ability.Stat.MaxHP);
                break;

            case Ability.Stat.MaxHP:
                if (_HP > _stats[(int)Ability.Stat.MaxHP])
                    _HP = _stats[(int)Ability.Stat.MaxHP];
                break;

            case Ability.Stat.MaxMP:
                if (_MP > _stats[(int)Ability.Stat.MaxMP])
                    _MP = _stats[(int)Ability.Stat.MaxMP];
                break;
        }

        // return
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


    virtual public float DPS()
    {
        float tot = this[Ability.Stat.Attack];

        tot *= this[Ability.Stat.IncreaseDamage];
        tot *= this[Ability.Stat.AttackSpeed];

        tot *= (this[Ability.Stat.CriticalChance] <= 1 ? this[Ability.Stat.CriticalChance]+1 : 2) * this[Ability.Stat.CriticalScale];

        return tot;
    }

    virtual public float Toughness()
    {
        float tot = this[Ability.Stat.MaxHP];

        tot /= 1 - this[Ability.Stat.Defense] / (300 + this[Ability.Stat.Defense]);

        tot *= this[Ability.Stat.ReduceDamage];

        return tot;
    }



    virtual public Ability.AffectResult AttackedBy(AbilityStatus attacker, float skillScale)      // returns reduced HP value
    {
        Ability.AffectResult ret = new Ability.AffectResult();

        if (attacker[Ability.Stat.Accuracy] - this[Ability.Stat.Dodge] < Random.Range(0.0f, 1.0f))
        {
            ret.Dodged = true;
            return ret;
        }

        ret.Value = attacker[Ability.Stat.Attack] * skillScale;

        if (Random.Range(0.0f, 1.0f) < attacker[Ability.Stat.CriticalChance])
        {
            ret.Value *= attacker[Ability.Stat.CriticalScale];
            ret.Critical = true;
        }

        ret.Value *= 1 + (attacker[Ability.Stat.IncreaseDamage] - this[Ability.Stat.ReduceDamage]);

        ret.Value *= 1 - this[Ability.Stat.Defense] / (300 + this[Ability.Stat.Defense]);     // defense adjust

        //

        if (ret.Value < 0)
            ret.Heal = true;

        if (_HP > ret.Value)
            _HP -= ret.Value;
        else
            _HP = 0;

        if (transform.CompareTag("Player"))
        {
            ret.Enemy = true;
            Camera.main.GetComponent<CinemachineVirtualCameraManager>().CameraShakeBegin(0.6f);
            GetComponent<Animator>()?.SetTrigger("Hurt");
        }

        if (UserSetting.OnDamage)
        {
            GameObject damage = Resources.Load("Prefabs/TextDamage") as GameObject;
            damage.GetComponent<TextDamage>().SetDamageText(ret);
            Instantiate(damage, transform.position, transform.rotation);
        }

        return ret;
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
                return $"{System.Math.Round(this[stat], 2)}";

            case Ability.Stat.Accuracy:
            case Ability.Stat.CriticalChance:
            case Ability.Stat.CriticalScale:
            case Ability.Stat.Dodge:
            case Ability.Stat.MoveSpeed:
            case Ability.Stat.Tenacity:
                return $"{System.Math.Round(this[stat] * 100, 2)} %";

            case Ability.Stat.SkillCooldown:
                return $"{System.Math.Round((1.0f - this[stat]) * 100, 2)} %";

            case Ability.Stat.ReduceDamage:
            case Ability.Stat.IncreaseDamage:
                return $"{System.Math.Round((this[stat] - 1.0f) * 100, 2)} %";

            default:
                return "error";
        }
    }
}
