using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour
{
    //[System.NonSerialized]
    Equipment[] _equipments = new Equipment[(int)Equipment.Type.EnumTotal];

    Dictionary<Ability.Stat, float> _stats
        = new Dictionary<Ability.Stat, float>();
    Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> _enchants
        = new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>();

    //public Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> Enchants
    //{
    //    get { return new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>(_enchants); }
    //}


    public float this[Ability.Stat stat, Ability.Enhance enhance]   // enchant indexer
    {
        get
        {
            KeyValuePair<Ability.Stat, Ability.Enhance> key
                = new KeyValuePair<Ability.Stat, Ability.Enhance>(stat, enhance);

            if (_enchants.TryGetValue(key, out float value))
                return value;
            else
                return float.NaN;
        }
    }

    public float this[Ability.Stat stat]                            // stat indexer
    {
        get
        {
            if (_stats.TryGetValue(stat, out float value))
                return value;
            else
                return float.NaN;
        }
    }



    public Equipment Equip(Equipment newEquipment)
    {
        // Exception
        if (newEquipment == null)
            return null;

        int inType = (int)newEquipment.EquipmentType;

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
        // Exception
        if (offType < Equipment.Type.MainWeapon || offType >= Equipment.Type.EnumTotal)
            return null;

        int type = (int)offType;

        Equipment oldEquipment = _equipments[type];

        _equipments[type] = null;

        if (oldEquipment != null)
            Refresh();

        return oldEquipment;
    }


    void Refresh()
    {
        // reset
        _stats.Clear();
        _enchants.Clear();


        // getting equipment properties
        for (int i = _equipments.Length - 1; i >= 0; --i)
        {
            if (_equipments[i] != null)
            {
                // static properties
                foreach (var key_value in _equipments[i].StaticProperties) 
                {
                    if (!_stats.ContainsKey(key_value.Key))
                        _stats[key_value.Key] = 0;

                    _stats[key_value.Key] += key_value.Value;
                }

                // dynamic properties
                foreach (var key_value in _equipments[i].DynamicProperties)  
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


                        case Ability.Enhance.Chance_Percent:
                        case Ability.Enhance.NegMul_Percent:
                        case Ability.Enhance.PosMul_Percent:
                        case Ability.Enhance.Addition_Percent:
                            {
                                if (!_enchants.ContainsKey(key_value.Key))
                                    _enchants[key_value.Key] = 1;

                                switch (key_value.Key.Value)
                                {
                                    case Ability.Enhance.Chance_Percent:
                                    case Ability.Enhance.NegMul_Percent:
                                        _enchants[key_value.Key] *= 1-key_value.Value;
                                        break;
                                    case Ability.Enhance.PosMul_Percent:
                                        _enchants[key_value.Key] *= 1+key_value.Value;
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
