using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stat atk
//      weapon atk
//      subweapon atk
//      buff atk
//      strength
//      option atk
//      extra atk

// crit atk
//      

public class VulnerableStatus : MonoBehaviour
{
    EquipmentSlot equipmentSlot;


    public int level = 1;

    float _strength = 1;
    public float strength { get { return _strength; } }

    float _vitality = 1;
    public float vitality { get { return _vitality; } }


    float _currentHP = 100;
    public float currentHP { get { return _currentHP; } }

    float _currentMP = 100;
    public float currentMP { get { return _currentMP; } }

    float _maxHP = 100;
    public float maxHP { get { return _maxHP; } }

    float _maxMP = 100;
    public float maxMP { get { return _maxMP; } }

    float _atk = 0;
    float _criticalChance = 0.1f;
    float _accuracy = 0.9f;
    float _givingDamage = 1.0f;

    float _def = 0;
    float _gettingDamage = 1.0f;
    float _dodge = 0.01f;

    float _cooldown = 1.0f;

    private void Awake()
    {
        equipmentSlot = GetComponent<EquipmentSlot>();
    }

    void RefreshAtk()
    {
        _atk = (equipmentSlot.atk /*+ abs atk buff*/) *
            (1 + (strength * 0.01f)) *
            (1 + (equipmentSlot.options[(int)StatusOption.Option.Attack_Percent])) *
            (1 + (/* buff atk * */ 0.01f))
            /* + additional atk*/;
    }

    void RefreshDef()
    {
        _def = (equipmentSlot.def + strength) * /*(1 + (equipmentSlot[(int)StatusOption.Option.D])*/);
    }

    public void Attack(VulnerableStatus other, float skillDamageScale)
    {
        other._currentHP -= 100 / (100 + other._def) * _atk;
    }
}
