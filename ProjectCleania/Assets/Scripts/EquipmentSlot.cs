using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot
{
    float _atk = 0;
    public float atk { get => _atk; }
    float _atkPerSecond = 1.0f;
    public float atkPerSecond { get => _atkPerSecond; }
    public float def = 0;
    public float strength = 0;

    Equipment[] _equipments = new Equipment[(int)Equipment.Type.EnumTotal];
    float[] _options = new float[(int)AbilityOption.Name.EquipmentOptionTotal];

    public float this[AbilityOption.Name index]            // indexer
    {
        get => _options[(int)index];
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
        for (int i = _options.Length - 1; i >= 0; --i)
            _options[i] = 0;

        _atk = 0;
        _atkPerSecond = 1.0f;
        def = 0;
        strength = 0;

        // equipment status get
        for (int i = _equipments.Length - 1; i >= 0; --i)
        {
            if (_equipments[i] != null)
            {
                _atk += _equipments[i].atk;
                _atkPerSecond += _equipments[i].atkPerSecond;
                def += _equipments[i].def;
                strength += _equipments[i].strength;

                // add or multiply option value if it exists
            }
        }
    }
}
