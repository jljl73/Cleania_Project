using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatus : MonoBehaviour
{
    Status status;          // status is essential unlike equips or buffs
    EquipmentSlot equipments;
    BuffManager buffs;

    //Dictionary<AbilityOption.Stat, float> _stats = new Dictionary<AbilityOption.Stat, float>();

    float _strength;
    public float strenght { get => RefreshStrength(); }
    float _vitality;
    public float vitality { get => RefreshVitality(); }
    float _atk;
    public float atk { get => RefreshAtk(); }
    float _def;
    public float def { get => RefreshDef(); }
    float _criticalChance;
    public float criticalChance { get => RefreshCriticalChance(); }
    float _criticalScale;
    public float criticalScale { get => RefreshCriticalScale(); }
    float _moveSpeed;
    public float moveSpeed { get => RefreshMoveSpeed(); }
    float _attackSpeed;
    public float attackSpeed { get => RefreshAttackSpeed(); }

    float _maxHP;
    public float maxHP { get => RefreshMaxHP(); }
    float _maxMP;
    public float maxMP { get => RefreshMaxMP(); }
    float _HP;
    public float HP { get => _HP; }
    float _MP;
    public float MP { get => _MP; }


    private void Awake()
    {
        status = GetComponent<Status>();
        equipments = GetComponent<EquipmentSlot>();
        buffs = GetComponent<BuffManager>();

        RefreshAll();
    }

    void RefreshAll()
    {
        RefreshStrength();
        RefreshVitality();
        RefreshAtk();
        RefreshDef();
        RefreshCriticalChance();
        RefreshCriticalScale();
        RefreshMoveSpeed();
        RefreshAttackSpeed();

        RefreshMaxHP();
        RefreshMaxMP();
        _HP = _maxHP;
        _MP = _maxMP;
    }

    float RefreshStrength()
    {
        if (status == null)
            return -1;

        _strength = status.strength;

        if (equipments != null)
        {
            _strength += equipments.strength;
            //_strength *= equipments[AbilityOption.Name.]
        }

        if (buffs != null)
        {
            _strength += buffs[AbilityOption.Buff.Attack_Buff];
        }

        return _strength;
    }

    float RefreshVitality()
    {
        if (status == null)
            return -1;

        _vitality = status.vitality;

        if (equipments != null)
        {
            _vitality += equipments[AbilityOption.Equipment.Vitality_Abs];
        }

        if(buffs != null)
        {
            //_vitality += buffs[AbilityOption.Name.Vitality_Buff]
        }

        return _vitality;
    }

    float RefreshAtk()
    {
        if (status == null)
            return -1;

        _atk = status.atk;

        if (equipments != null)
        {
            _atk += equipments.atk;
            _atk *= (1 + equipments[AbilityOption.Equipment.Attack_Percent]);
        }

        if(buffs != null)
        {
            _atk *= (1 + buffs[AbilityOption.Buff.Attack_Buff]);
        }

        return _atk;
    }

    float RefreshDef()
    {
        if (status == null)
            return -1;

        _def = status.def;

        if (equipments != null)
        {
            _def += equipments.def;
            _def += equipments[AbilityOption.Equipment.Defense_Abs];
        }

        if (buffs != null)
        {
            _def *= (1 + buffs[AbilityOption.Buff.Defense_Buff]);
        }
        
        return _def;
    }

    float RefreshCriticalChance()
    {
        if (status == null)
            return -1;

        _criticalChance = status.criticalChance;

        if (equipments != null)
        {
            _criticalChance *= (1 + equipments[AbilityOption.Equipment.CriticalChance_Percent]);
        }

        if (buffs != null)
        {
            //_criticalChance *= (1 + buffs[AbilityOption.Name.]);
        }

        return _criticalChance;
    }

    float RefreshCriticalScale()
    {
        if (status == null)
            return -1;

        _criticalScale = status.criticalScale;

        if (equipments != null)
        {
            _criticalScale += equipments[AbilityOption.Equipment.CriticalScale_Percent];
        }

        if (buffs != null)
        {
            //_criticalScale *= (1 + buffs[AbilityOption.Name.]);
        }

        return _criticalScale;
    }

    float RefreshMoveSpeed()
    {
        if (status == null)
            return -1;

        _moveSpeed = status.moveSpeed;

        if (equipments != null)
        {
            //_moveSpeed *= (1 + equipments[AbilityOption.Name]);
        }

        if (buffs != null)
        {
            _moveSpeed *= (1 + buffs[AbilityOption.Buff.MoveSpeed_Buff]);
        }

        return _moveSpeed;
    }

    float RefreshAttackSpeed()
    {
        if (status == null)
            return -1;

        _attackSpeed = 0;   // follow attack speed

        if (equipments != null)
        {
            _attackSpeed += equipments.atkPerSecond;
        }

        if (buffs != null)
        {
            _attackSpeed *= (1 + buffs[AbilityOption.Buff.AttackSpeed_Buff]);
        }

        return _attackSpeed;
    }



    float RefreshMaxHP()
    {
        if (status == null)
            return -1;

        _maxHP = vitality * 100;

        if(equipments != null)
        {
            _maxHP *= (1 + equipments[AbilityOption.Equipment.HP_Percent]);
        }

        if(buffs != null)
        {
            //buffs[AbilityOption.Name.HP];
        }

        return _maxHP;
    }

    float RefreshMaxMP()
    {
        if (status == null)
            return -1;

        _maxMP = 100;

        if (equipments != null)
        {
            _maxMP += equipments[AbilityOption.Equipment.MaxMP_Abs];
        }

        if(buffs != null)
        {
            //_maxMP += buffs[AbilityOption.Name.MPconsumeReduce_Percent blahbla];
        }

        return _maxMP;
    }


    //float RefreshStat(AbilityOption.Stat stat)
    //{
    //    if (status == null)
    //        return -1;

    //    if(status[stat] == null)
    //    _stats[stat] = 0;

    //}

    public float TotalDamage()
    {
        float value = atk * (1 + strenght * 0.01f);

        if (Random.Range(0, 1) < criticalChance)
            value *= criticalScale;

        return value;
    }

    public float AttackedBy(AbilityStatus other, float skillScale)
    {
        float value = other.TotalDamage() * skillScale;


        //_HP -= (other.TotalDamage() )

        return 0;
    }

    void ConsumeMP(float usedMP)
    {
        _MP -= usedMP;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //        Debug.Log(TotalDamage());
    //}
}
