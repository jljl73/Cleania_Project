using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    Equipment[] _equipments = new Equipment[(int)Equipment.Type.EnumTotal];

    Dictionary<Ability.Stat, float> _stats
        = new Dictionary<Ability.Stat, float>();

    Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> _enchants
        = new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>();

    public Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> enchants   // enchants getter (used for foreach only)
    {
        get { return new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>(_enchants); }
    }   

    public float this[Ability.Stat stat, Ability.Enhance enhance]                   // enchant indexer
    {
        get
        {
            KeyValuePair<Ability.Stat, Ability.Enhance> key
                = new KeyValuePair<Ability.Stat, Ability.Enhance>(stat, enhance);

            _enchants.TryGetValue(key, out float value);

            return value;
        }
    }

    public float this[Ability.Stat stat]                                                  // stat indexer
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
        _enchants.Clear();

        foreach (var key_value in _stats)
            _stats[key_value.Key] = 0;


        // equipment status get
        for (int i = _equipments.Length - 1; i >= 0; --i)
        {
            if (_equipments[i] != null)
            {
                _stats[Ability.Stat.Attack] += _equipments[i].atk;
                _stats[Ability.Stat.AttackSpeed] += _equipments[i].atkPerSecond;
                _stats[Ability.Stat.Defense] += _equipments[i].def;
                _stats[Ability.Stat.Strength] += _equipments[i].strength;

                foreach (var key_value in _equipments[i].enchant)
                {
                    switch (key_value.Key.Value)
                    {
                        case Ability.Enhance.Addition:
                        case Ability.Enhance.Absolute:
                            {
                                if (!_enchants.ContainsKey(key_value.Key))
                                    _enchants[key_value.Key] = 0;

                                _enchants[key_value.Key] += key_value.Value;
                            }
                            break;


                        case Ability.Enhance.NegMulti_Percent:
                        case Ability.Enhance.PosMulti_Percent:
                        case Ability.Enhance.Addition_Percent:
                            {
                                if (!_enchants.ContainsKey(key_value.Key))
                                    _enchants[key_value.Key] = 1;

                                switch (key_value.Key.Value)
                                {
                                    case Ability.Enhance.NegMulti_Percent:
                                        _enchants[key_value.Key] *= (1 - key_value.Value);
                                        break;
                                    case Ability.Enhance.PosMulti_Percent:
                                        _enchants[key_value.Key] *= (1 + key_value.Value);
                                        break;
                                    case Ability.Enhance.Addition_Percent:
                                        _enchants[key_value.Key] += key_value.Value;
                                        break;
                                }
                            }
                            break;

                        default:
                            // error code
                            break;
                    }
                }
            }
        }
    }
}
