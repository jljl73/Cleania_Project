using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatus : MonoBehaviour
{
    Status status;          // status is essential unlike equips or buffs
    EquipmentSlot equipments;   
    // buffs

    float _strength;
    float _vitality;
    float _atk;
    float _def;
    float _criticalChance;
    float _criticalScale;
    float _moveSpeed;
    float _attackSpeed;

    private void Awake()
    {
        status = GetComponent<Status>();
        equipments = GetComponent<EquipmentSlot>();
        // buffs = GetComponent<>();

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

    void RefreshStrength()
    {
        if(status == null)
            return;

        _strength = status.strength;

        if(equipments != null)
        {
            _strength += equipments.strength;
        }

        //if(buffs != null)
        //{
        //
        //}
    }

    void RefreshVitality()
    {
        if (status == null)
            return;

        _vitality = status.vitality;

        if (equipments != null)
        {
            _vitality += equipments[StatusOption.Option.Vitality_Abs];
        }

        //if(buffs != null)
        //{
        //
        //}
    }

    void RefreshAtk()
    {
        if (status == null)
            return;

        _atk = status.atk;

        if (equipments != null)
        {
            _atk *= (1 + equipments[StatusOption.Option.Attack_Percent]);
        }

        //if(buffs != null)
        //{
        //
        //}
    }

    void RefreshDef()
    {

    }

    void RefreshCriticalChance()
    {

    }

    void RefreshCriticalScale()
    {

    }

    void RefreshMoveSpeed ()
    {

    }

    void RefreshAttackSpeed ()
    {

    }

}
