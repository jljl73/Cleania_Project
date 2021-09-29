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
        for (int i = 0; i < nEquipment; ++i)
            equipmentOptions[i] = null;

        Debug.Log(GetSumOptions(EquipmentOption.Option.Attack));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            for(int i = 0;i < 20;++i)
            {
                Debug.Log(GetSumOptions((EquipmentOption.Option)i));
            }
        }
    }

    // >> Start
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
    // << End



    public void WearEquipment(ItemInventory.EquipmentType type, EquipmentOption eo)
    {
        equipmentOptions[((int)type) - 1] = eo;
    }

    public void TakeOffEquipment(ItemInventory.EquipmentType type)
    {
        equipmentOptions[((int)type) - 1] = null;
    }
}