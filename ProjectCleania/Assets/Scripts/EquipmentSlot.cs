using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    Equipment[] _equipments = new Equipment[(int)Equipment.Type.EnumTotal];

    Dictionary<AbilityOption.Stat, float> _stats
    = new Dictionary<AbilityOption.Stat, float>();

    Dictionary<KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance>, float> _options
    = new Dictionary<KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance>, float>();

    public float this[AbilityOption.Stat stat, AbilityOption.Enhance enhance]            // indexer
    {
        get
        {
            KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance> key 
                = new KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance>(stat, enhance);

            _options.TryGetValue(key, out float value);

            return value;
        }
    }

    public float this[AbilityOption.Stat stat]            // indexer
    {
        get
        {
            _stats.TryGetValue(stat, out float value);

            return value;
        }
    }


    public Equipment Equip(Equipment newEquipment)
    {
        int inType = (int)newEquipment.equipmentType;

        if (_equipments[inType] != null)
        {
            Equipment oldEquipment = _equipments[inType];
            _equipments[inType] = newEquipment;

            Refresh();

            return oldEquipment;
        }
        else
        {
            _equipments[inType] = newEquipment;

            Refresh();

            return null;
        }
    }

    public Equipment Unequip(Equipment.Type offType)
    {
        int type = (int)offType;

        Equipment oldEquipment = _equipments[type];

        _equipments[type] = null;

        Refresh();

        return oldEquipment;
    }

    void Refresh()
    {
        // reset
        //foreach (var key_value in _options)
        //    _options[key_value.Key] = 0;
        _options.Clear();

        foreach (var key_value in _stats)
            _stats[key_value.Key] = 0;


        // equipment status get
        for (int i = _equipments.Length - 1; i >= 0; --i)
        {
            if (_equipments[i] != null)
            {
                _stats[AbilityOption.Stat.Attack] += _equipments[i].atk;
                _stats[AbilityOption.Stat.AttackSpeed] += _equipments[i].atkPerSecond;
                _stats[AbilityOption.Stat.Defense] += _equipments[i].def;
                _stats[AbilityOption.Stat.Strength] += _equipments[i].strength;

                foreach (var key_value in _equipments[i].options)
                {
                    switch (key_value.Key.Value)
                    {
                        case AbilityOption.Enhance.Absolute:
                            if (_options.ContainsKey(key_value.Key))
                                _options[key_value.Key] = 0;
                            _options[key_value.Key] += key_value.Value;
                            break;
                        }
                }
            }
        }
    }
}
