using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableStatus
{
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

    void RefreshStatus()
    {
        // need more details
        // beware of recursion of getter

        // considerables
        //      equipments
        //      buffs (contains debuff)
        //      level

        _maxHP = 100 + vitality * 100;
        _maxMP = 100;
        _atk = strength;
        _def = strength;
    }

    public void Atack(VulnerableStatus other)
    {
        other._currentHP -= 100 / (100 + other._def) * _atk;
    }
}
