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

    Equipment[] equipments;
    public float[] options;

    public EquipmentSlot()
    {
        equipments = new Equipment[(int)Equipment.Type.EnumTotal];
        options = new float[(int)StatusOption.Option.EnumTotal];
    }


    public Equipment Equip(Equipment newEquipment)
    {
        int inType = (int)newEquipment.equipmentType;

        if(equipments[inType] != null)
        {
            Equipment oldEquipment = equipments[inType];
            equipments[inType] = newEquipment;

            Refresh();

            return oldEquipment;
        }
        else
        {
            equipments[inType] = newEquipment;

            Refresh();

            return null;
        }
    }

    public Equipment Unequip(Equipment.Type offType)
    {
        int type = (int)offType;

        Equipment oldEquipment = equipments[type];

        equipments[type] = null;

        Refresh();

        return oldEquipment;
    }

    void Refresh()
    {
        // reset
        for(int i = options.Length-1; i >= 0; --i )
            options[i] = 0;

        _atk = 0;
        _atkPerSecond = 1.0f;
        def = 0;
        strength = 0;

        // equipment status get
        for(int i = equipments.Length-1; i >= 0; --i )
        {
            if(equipments[i] != null)
            {
                _atk += equipments[i].atk;
                _atkPerSecond += equipments[i].atkPerSecond;
                def += equipments[i].def;
                strength += equipments[i].strength;

                // add or multiply option value if it exists
            }
        }
    }
}
