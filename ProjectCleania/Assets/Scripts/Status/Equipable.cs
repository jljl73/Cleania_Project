using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Equipable : MonoBehaviour
{
    public enum SyncType
    {
        NonSync,
        PlayerEquipment
    }
    [SerializeField]
    SyncType syncWith;


    ItemInstance_Equipment[] _equipments = new ItemInstance_Equipment[(int)ItemInstance_Equipment.Type.EnumTotal];
    /// <summary>
    /// Get & Set current equipments with this indexer.<para></para>
    /// setter call Equip when value is correct with type / Unequip when value is null.
    /// </summary>
    /// <param name="type">input euipment to access slot.</param>
    /// <returns>
    /// - getter returns current wearing equipment.<para></para>
    /// - setter returns void.
    /// </returns>
    public ItemInstance_Equipment this[ItemInstance_Equipment.Type type]
    {
        get => _equipments[(int)type];
        set
        {
            if (value == null)
                UnequipAsync(type);
            else if (value.EquipmentType == type)
                EquipAsync(value);
        }
    }


    Dictionary<Ability.Stat, float> _stats
        = new Dictionary<Ability.Stat, float>();
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

    Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> _enchants
        = new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>();
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


    private void Start()
    {
        switch(syncWith)
        {
            case SyncType.PlayerEquipment:
                SavedData.Instance.Item_Equipments.Subscribe(Synchronize, ItemInstance_Equipment.Type.EnumTotal);
                break;
        }

        GameManager.Instance.PlayerAbility?.FullHP();
        GameManager.Instance.PlayerAbility?.FullMP();
    }

    private void OnDestroy()
    {
        switch (syncWith)
        {
            case SyncType.PlayerEquipment:
                SavedData.Instance.Item_Equipments.QuitSubscribe(Synchronize);
                break;
        }
    }

    public ItemInstance_Equipment Equip(ItemInstance_Equipment newEquipment, bool sync = true)
    {
        if (sync)
            switch(syncWith)
            {
                case SyncType.PlayerEquipment:
                    ItemInstance item = Unequip(newEquipment.EquipmentType, sync);
                    SavedData.Instance.Item_Equipments.Add(newEquipment);
                    return (ItemInstance_Equipment)item;
            }

        return EquipAsync(newEquipment);
    }
    public ItemInstance_Equipment Unequip(ItemInstance_Equipment.Type offType, bool sync = true)
    {
        if (sync)
        switch (syncWith)
            {
                case SyncType.PlayerEquipment:
                    ItemInstance item = SavedData.Instance.Item_Equipments[offType];
                    if (item != null)
                        SavedData.Instance.Item_Equipments.Remove(item);
                    return (ItemInstance_Equipment)item;
            }

        return UnequipAsync(offType);
    }


}








public partial class Equipable
{
    ItemInstance_Equipment EquipAsync(ItemInstance_Equipment newEquipment)
    {
        // Exception
        if (newEquipment == null)
            return null;

        int inType = (int)newEquipment.EquipmentType;

        if (_equipments[inType] != null)
        {
            ItemInstance_Equipment oldEquipment = _equipments[inType];
            _equipments[inType] = newEquipment;
            //newEquipment.WoreBy = this;

            Refresh();

            return oldEquipment;
        }
        else
        {
            _equipments[inType] = newEquipment;
            //newEquipment.WoreBy = this;

            Refresh();

            return null;
        }
    }

    ItemInstance_Equipment UnequipAsync(ItemInstance_Equipment.Type offType)
    {
        // Exception
        if (offType < ItemInstance_Equipment.Type.MainWeapon || offType >= ItemInstance_Equipment.Type.EnumTotal)
            return null;

        int type = (int)offType;

        ItemInstance_Equipment oldEquipment = _equipments[type];

        _equipments[type] = null;

        if (oldEquipment != null)
        {
            //oldEquipment.WoreBy = null;
            Refresh();
        }

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
                foreach (var key_value in _equipments[i].StaticDictionary)
                {
                    if (!_stats.ContainsKey(key_value.Key))
                        _stats[key_value.Key] = 0;

                    if (key_value.Key == Ability.Stat.CriticalChance || key_value.Key == Ability.Stat.AttackSpeed || key_value.Key == Ability.Stat.MoveSpeed)
                        _stats[key_value.Key] += key_value.Value;
                    else
                        _stats[key_value.Key] += key_value.Value * _equipments[i].Level / 50;
                }

                // dynamic properties
                foreach (var key_value in _equipments[i].DynamicDictionary)
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
                                        _enchants[key_value.Key] *= 1 - key_value.Value;
                                        break;
                                    case Ability.Enhance.PosMul_Percent:
                                        _enchants[key_value.Key] *= 1 + key_value.Value;
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





    // Sync
    void Synchronize(iItemStorage sender, ItemStorage_Equipments.SyncOperator oper, ItemInstance_Equipment.Type index)
    {
        if (this == null)
        {
            ((ItemStorage_Equipments)sender).QuitSubscribe(Synchronize);
            return;
        }

        switch (oper)
        {
            case ItemStorage<ItemInstance_Equipment.Type>.SyncOperator.Add:
                {
                    ItemStorage_Equipments storage = (ItemStorage_Equipments)sender;
                    ItemInstance_Equipment item = (ItemInstance_Equipment)storage[index];
                    EquipAsync(item);
                    break;
                }
            case ItemStorage<ItemInstance_Equipment.Type>.SyncOperator.Remove:
                {
                    UnequipAsync(index);
                    break;
                }
            case ItemStorage<ItemInstance_Equipment.Type>.SyncOperator.Refresh:
                {
                    for(int i = 0; i < (_equipments.Length); i++)
                    {
                        _equipments[i] = null;
                    }

                    foreach(var i in ((ItemStorage_Equipments)sender).Items)
                    {
                        _equipments[(int)i.Value] = (ItemInstance_Equipment)i.Key;
                    }
                    Refresh();
                }
                break;

            default:
                Debug.LogError("Logic error in UI_ItemContainer : Synchronize");
                break;
        }
    }



}