using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDealer
{
    // instantiating ItemInstance_Equipment
    static public ItemInstance_Equipment ShuffleStatics (ItemInstance_Equipment equipment)
    {
        // remove all stats
        foreach (var keyval in equipment.StaticProperties)
            equipment[keyval.Key] = float.NaN;

        // add stats again
        foreach(var table in equipment.SO.OptionTable.StaticTable)
            equipment[table.Key] = Random.Range(table.Min, table.Max);

        return equipment;
    }

    static public ItemInstance_Equipment ShuffleDynamics(ItemInstance_Equipment equipment, int number = 3)
    {
        // remove all options
        foreach (var keyval in equipment.DynamicProperties)
            equipment[keyval.Key] = float.NaN;

        // add options again
        var optionTable = equipment.SO.OptionTable.DynamicTable;

        int count = 0;
        int limit = 100;
        while(count < number && limit > 0)
        {
            limit--;
            var option = optionTable[Random.Range(0, optionTable.Length)];

            if (!float.IsNaN(equipment[option.KeyStat, option.KeyHow]))
                continue;

            equipment[option.KeyStat, option.KeyHow] = Random.Range(option.Min, option.Max);
            count++;
        }

        return equipment;
    }


    // Enchant
    static public Ability.DynamicOption RandomDynamicOption(EquipmentOptionSO optionSO)
    {
        EquipmentOptionSO.DynamicOptionTable[] dynamicTable = optionSO.DynamicTable;
        EquipmentOptionSO.DynamicOptionTable table = dynamicTable[Random.Range(0, dynamicTable.Length)];

        return new Ability.DynamicOption(Random.Range(table.Min, table.Max), table.KeyStat, table.KeyHow);
    }

    static public Ability.DynamicOption CandidateDynamicOption(ItemInstance_Equipment equipment)
    {
        EquipmentOptionSO.DynamicOptionTable[] dynamicTable = equipment.SO.OptionTable.DynamicTable;

        while (true)
        {
            EquipmentOptionSO.DynamicOptionTable table = dynamicTable[Random.Range(0, dynamicTable.Length)];

            if (table.KeyStat == equipment.ChangedOption.Stat &&
                table.KeyHow == equipment.ChangedOption.How ||
                float.IsNaN(equipment[table.KeyStat, table.KeyHow]))
            return new Ability.DynamicOption(Random.Range(table.Min, table.Max), table.KeyStat, table.KeyHow);
        }
    }

    static public bool TryChangeDynamic(ItemInstance_Equipment equipment, Ability.DynamicOption option)
    {
        if (equipment.ChangedOption.Stat == Ability.Stat.EnumTotal ||
            equipment.ChangedOption.How == Ability.Enhance.EnumTotal)
            return false;

        equipment[equipment.ChangedOption.Stat, equipment.ChangedOption.How] = float.NaN;

        if (!float.IsNaN(equipment[option.Stat, option.How]))
        {
            equipment[equipment.ChangedOption.Stat, equipment.ChangedOption.How] = equipment.ChangedOption.Value;
            return false;
        }

        equipment[option.Stat, option.How] = option.Value;
        return true;
    }


    // Repair
    static public int GetRepairCost(ItemInstance_Equipment equipment)
    {
        return (equipment.SO.Durability - (int)equipment.Durability);
    }

    static public bool TryRepair(ItemInstance_Equipment equipment, Storage inventory)
    {
        if (inventory.Crystal >= GetRepairCost(equipment))
        {
            inventory.AddCrystal(-GetRepairCost(equipment));
            equipment.Durability = equipment.SO.Durability;
            return true;
        }
        else
            return false;
    }

}
