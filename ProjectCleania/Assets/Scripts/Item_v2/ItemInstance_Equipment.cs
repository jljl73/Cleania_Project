using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance_Equipment : ItemInstance, iSavedData
{
    public enum Type
    {
        MainWeapon,
        SubWeapon,
        Hat,
        Top,
        Pants,
        Gloves,
        Shoes,
        EnumTotal
    }

    // used for unityengine only
    private ItemInstance_Equipment() : base(null)
    { }

    protected ItemInstance_Equipment(ItemSO itemSO, int level = 1) : base(itemSO)
    {
        Level = level;
        _statics = new Dictionary<Ability.Stat, float>();
    }

    /// <summary>
    /// Used instead of Constructor.<para></para>
    /// Generate equipment data with its ScriptableObject.<para></para>
    /// returns 'ItemInstance_Equipment' or null.
    /// </summary>
    /// <param name="itemSO"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    new static public ItemInstance_Equipment Instantiate(ItemSO itemSO, int level = 1)
    {
        if (itemSO.OptionTable != null && itemSO.MainCategory == ItemSO.enumMainCategory.Equipment)
            return new ItemInstance_Equipment(itemSO, level);
        else
            return null;
    }
    /// <summary>
    /// Used instead of Constructor.<para></para>
    /// Generate equipment data with its ID.<para></para>
    /// returns 'ItemInstance_Equipment' or null.
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    new static public ItemInstance_Equipment Instantiate(int itemID, int level = 1)
    {
        ItemSO itemSO = ItemSO.Load(itemID);

        if (itemSO == null)
            return null;
        else
            return Instantiate(itemSO, level); // delegate to overload
    }


    public Type EquipmentType = Type.MainWeapon;
    [Range(1, 50)]
    public int Level;
    public int Xp;
    public int NextXP;
    public float Durability;

    Dictionary<Ability.Stat, float> _statics
        = new Dictionary<Ability.Stat, float>();
    Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> _dynamics
        = new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>();

    /// <summary>
    ///  You can't change _stats with this accessor.<para></para>
    ///  use this[stat] to modify stats.<para></para>
    ///  * created for foreach access
    /// </summary>
    public Dictionary<Ability.Stat, float> StaticProperties
    {
        get { return new Dictionary<Ability.Stat, float>(_statics); }
    }
    
    /// <summary>
    ///  You can't change _enchants with this accessor.<para></para>
    ///  use this[stat, enhance] to modify enchants.<para></para>
    ///  * created for foreach access
    /// </summary>
    public Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> DynamicProperties
    {
        get { return new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>(_dynamics); }
    }

    
    /// <summary>
    /// returns equipment's stat value.<para></para>
    /// set as float.NaN to remove stat.
    /// </summary>
    /// <param name="stat"></param>
    /// <returns></returns>
    public float this[Ability.Stat stat]                                                   // stat indexer
    {
        get
        {
            if (_statics.TryGetValue(stat, out float value))
                return value * (Level / 50);
            else
                return float.NaN;
        }
        set
        {
            if (float.IsNaN(value))                         // set value NaN to remove property
                if (_statics.ContainsKey(stat))
                    _statics.Remove(stat);
                else
                    _statics[stat] = value;
        }
    }

    /// <summary>
    /// returns equipment's option value.<para></para>
    /// set as float.NaN to remove option.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="enhance"></param>
    /// <returns></returns>
    public float this[Ability.Stat stat, Ability.Enhance enhance]                    // enchant indexer
    {
        get
        {
            KeyValuePair<Ability.Stat, Ability.Enhance> key
                = new KeyValuePair<Ability.Stat, Ability.Enhance>(stat, enhance);

            if (_dynamics.TryGetValue(key, out float value))
                return value;
            else
                return float.NaN;
        }
        set
        {
            KeyValuePair<Ability.Stat, Ability.Enhance> key
                = new KeyValuePair<Ability.Stat, Ability.Enhance>(stat, enhance);

            if (float.IsNaN(value))                         // set value NaN to remove property
                if (_dynamics.ContainsKey(key))
                    _dynamics.Remove(key);
                else
                    _dynamics[key] = value;
        }
    }

    
    public List<string> StaticProperties_ToString()
    {
        List<string> string_list = new List<string>();

        foreach(var key_value in _statics)
        {
            string_list.Add($"{key_value.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{key_value.Value}");
        }

        return string_list;
    }

    public List<string> DynamicProperties_ToString()
    {
        List<string> string_list = new List<string>();

        foreach (var key_value in _dynamics)
        {
            switch (key_value.Key.Value)
            {
                case Ability.Enhance.Absolute:
                    string_list.Add($"{ key_value.Key.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{key_value.Value}");
                    break;
                case Ability.Enhance.Chance_Percent:
                    string_list.Add($"More Chance of { key_value.Key.Key.ToString()} {key_value.Value*100}%");
                    break;
                case Ability.Enhance.NegMul_Percent:
                    string_list.Add($"Reduce { key_value.Key.Key.ToString()} by {key_value.Value*100}%");
                    break;
                case Ability.Enhance.PosMul_Percent:
                    string_list.Add($"Increase { key_value.Key.Key.ToString()} by {key_value.Value*100 + 100}%");
                    break;
                case Ability.Enhance.Addition_Percent:
                    string_list.Add($"{ key_value.Key.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{key_value.Value*100}%");
                    break;
                case Ability.Enhance.Addition:
                    string_list.Add($"Additional { key_value.Key.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{key_value.Value}");
                    break;
            }
        }

        return string_list;
    }



    //public IEnumerator GetEnumerator()
    //{
    //    return (IEnumerator)this;
    //}

    ////public IEnumerator<Ability.Enchant> GetEnumerator()
    ////{
    ////    return (IEnumerator<Ability.Enchant>)this;
    ////}

    //public bool MoveNext()
    //{
    //    return true;
    //}

    //public void Reset()
    //{

    //}

    //public object Current
    //    { get
    //    {
    //        return 0;
    //    }
    //    }




        // SAVE DATA IMPLEMENTATION

    [SerializeField]
    List<Ability.StaticOption> SD_staticOption = new List<Ability.StaticOption>();
    [SerializeField]
    List<Ability.DynamicOption> SD_dynamicOption = new List<Ability.DynamicOption>();

    void iSavedData.AfterLoad()
    {
        so = ItemSO.Load(id);

        foreach(var en in SD_staticOption)
        {
            _statics[en.Stat] = en.Value;
        }
        foreach (var en in SD_dynamicOption)
        {
            _dynamics[en.Key] = en.Value;
        }

        //jsonStatic.Clear();
        //jsonDynamic.Clear();
    }

    void iSavedData.BeforeSave()
    {
        id = so.ID;

        SD_staticOption.Clear();
        SD_dynamicOption.Clear();

        foreach(var kv in _statics)
        {
            SD_staticOption.Add(new Ability.StaticOption(kv.Value, kv.Key));
        }
        foreach(var kv in _dynamics)
        {
            SD_dynamicOption.Add(new Ability.DynamicOption(kv.Value, kv.Key));
        }
    }


}
