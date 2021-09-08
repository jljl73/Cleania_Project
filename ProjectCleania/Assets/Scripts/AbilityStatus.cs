using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatus : MonoBehaviour
{
    Status status;          // status is essential unlike equips or buffs
    EquipmentSlot equipments;
    BuffManager buffs;

    float _strength;
    public float strenght { get => RefreshStrength();}
    float _vitality;
    public float vitality { get => RefreshVitality();}
    float _atk;
    public float atk { get => RefreshAtk();}
    float _def;
    public float def { get => RefreshDef();}
    float _criticalChance;
    public float criticalChance { get => RefreshCriticalChance();}
    float _criticalScale;
    public float criticalScale { get => RefreshCriticalScale();}
    float _moveSpeed;
    public float moveSpeed { get => RefreshMoveSpeed();}
    float _attackSpeed;
    public float attackSpeed { get => RefreshAttackSpeed();}

    float _HP;
    public float HP { get => _HP; }
    float _MP;
    public float MP { get => _MP; }
    float _maxHP;
    public float maxHP { get => RefreshMaxHP(); }
    float _maxMP;
    public float maxMP { get => RefreshMaxMP(); }


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
    }

    float RefreshStrength()
    {
        if(status == null)
            return -1;

        _strength = status.strength;

        if(equipments != null)
        {
            _strength += equipments.strength;
        }

        if(buffs != null)
        {
            //_strength += buffs[BuffManager.Option.Attack];
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
            _vitality += equipments[AbilityOption.Name.Vitality_Abs];
        }

        //if(buffs != null)
        //{
        //
        //}

        return _vitality;
    }

    float RefreshAtk()
    {
        if (status == null)
            return -1;

        _atk = status.atk;

        if (equipments != null)
        {
            _atk *= (1 + equipments[AbilityOption.Name.Attack_Percent]);
        }

        //if(buffs != null)
        //{
        //
        //}

        return _atk;
    }

    float RefreshDef()
    {

        return _def;
    }

    float RefreshCriticalChance()
    {

        return _criticalChance;
    }

    float RefreshCriticalScale()
    {

        return _criticalScale;
    }

    float RefreshMoveSpeed ()
    {

        return _moveSpeed;
    }

    float RefreshAttackSpeed ()
    {
        return _attackSpeed;
    }



    float RefreshMaxHP()
    {
        return _maxHP;
    }

    float RefreshMaxMP ()
    {
        return _maxMP ;
    }


    public float TotalDamage()
    {
        return 0;
    }
}
