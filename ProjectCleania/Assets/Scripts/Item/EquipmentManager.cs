using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    const int nEquipment = 7;

    static GameObject[] Equipments = new GameObject[nEquipment];
    public EquipmentOption[] equipmentOptions = new EquipmentOption[nEquipment];
    public Item[] items = new Item[nEquipment];

    void Start()
    {
        Debug.Log(GetSumOptions(EquipmentOption.Option.Attack));
    }

    public int GetSumOptions(EquipmentOption.Option option)
    {
        int sum = 0;
        foreach (var e in equipmentOptions)
        {
            if (e != null)
                sum += e[option];
        }
        return sum;
    }
}