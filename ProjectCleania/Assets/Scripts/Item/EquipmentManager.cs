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

    void EquipmentSlotChange(ItemInventory.EquipmentType type, EquipmentOption eo = null)
    {
        int part = (int)type - 1;

        if (eo == null)
        {
            print("unequip");
            GameManager.Instance.SinglePlayer.GetComponent<Equipable>().Unequip((Equipment.Type)part);
            return;
        }
        print("equip");

        Equipment equip = new Equipment();

        equip.EquipmentType = (Equipment.Type)part;
    
        for(int i = eo.StaticOptionKeys.Count-1; i >= 0; i--)
        {
            var key = Ability.EquipmentOptionToAbility(eo.StaticOptionKeys[i]);
            equip[key.Key, key.Value] = Ability.EquipmentOptionToWeight(eo.StaticOptionKeys[i]) * eo.StaticOptionValues[i];
        }
        for (int i = eo.VariableOptionKeys.Count-1; i >= 0; i--)
        {
            var key = Ability.EquipmentOptionToAbility(eo.VariableOptionKeys[i]);
            equip[key.Key, key.Value] = Ability.EquipmentOptionToWeight(eo.VariableOptionKeys[i]) * eo.VariableOptionValues[i];
        }
        if (type == ItemInventory.EquipmentType.Weapon)
            equip[Ability.Stat.AttackSpeed, Ability.Enhance.Absolute] = 1.3f;

        GameManager.Instance.SinglePlayer.GetComponent<Equipable>().Equip(equip);
    }
    // << End



    public void WearEquipment(ItemInventory.EquipmentType type, EquipmentOption eo)
    {
        equipmentOptions[((int)type) - 1] = eo;
        EquipmentSlotChange(type, eo);
    }

    public void TakeOffEquipment(ItemInventory.EquipmentType type)
    {
        equipmentOptions[((int)type) - 1] = null;
        EquipmentSlotChange(type);
    }
}