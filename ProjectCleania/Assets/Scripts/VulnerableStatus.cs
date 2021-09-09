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
    public float strength { get => _strength; }
    float _vitality = 1;
    public float vitality { get => _vitality; }

    float _currentHP = 100;
    public float currentHP { get => _currentHP; }
    float _currentMP = 100;
    public float currentMP { get => _currentMP; }
    float _maxHP = 100;
    public float maxHP { get => _maxHP; }
    float _maxMP = 100;
    public float maxMP { get => _maxMP; }

    float _atk;
    float _criticalChance = 10; // %
    float _criticalScale = 200; // %
    float _accuracy = 100;      // %
    float _givingDamage = 1.0f;

    float _def;
    float _gettingDamage = 1.0f;
    float _dodge = 1;           // %

    float _cooldown = 1.0f;

    private void Awake()
    {
        equipmentSlot = GetComponent<EquipmentSlot>();
    }

    void RefreshAtk()
    {
        _atk = (equipmentSlot.atk /*+ abs atk buff*/) *
            (1 + (strength * 0.01f)) *
            (1 + (equipmentSlot[(int)AbilityOption.Equipment.Attack_Percent])) *
            (1 + (/* buff atk * */ 0.01f))
            /* + additional atk*/;
    }

    void RefreshDef()
    {
        //_def = (equipmentSlot.def + strength) * /*(1 + (equipmentSlot[(int)AbilityOption.Name.D])*/);
    }

    public void Inflict(VulnerableStatus other, float skillDamageScale)
    {
        float totalDamage = _atk * skillDamageScale * _givingDamage * (1 - (other._def / (other._def + 300))) * (1 - other._gettingDamage);

        if(Random.Range(0, 100) < (_accuracy - other._dodge))
        {
            if (Random.Range(0, 100) < _criticalChance)
                totalDamage *= _criticalScale * 0.01f;

            other._currentHP -= totalDamage;
        }
    }
}
