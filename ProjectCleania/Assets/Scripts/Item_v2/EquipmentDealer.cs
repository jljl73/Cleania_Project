using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDealer
{
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

}
