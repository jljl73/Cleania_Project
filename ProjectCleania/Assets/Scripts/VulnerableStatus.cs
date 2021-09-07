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
    float _def = 0;

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

    public void Attack(VulnerableStatus other)
    {
        other._currentHP -= 100 / (100 + other._def) * _atk;
    }
}
