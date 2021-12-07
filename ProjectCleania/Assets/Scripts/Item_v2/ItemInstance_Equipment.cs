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
        Chest,
        Pants,
        Hands,
        Boots,
        EnumTotal
    }

    #region constructors

    // used for unityengine only
    private ItemInstance_Equipment() : base(null)
    { }

    protected ItemInstance_Equipment(ItemSO itemSO, int level = 1) : base(itemSO)
    {
        Level = level;
        EquipmentType = _CategoryToType(itemSO.SubCategory);
        this.Durability = itemSO.Durability;
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
        {
            ItemInstance_Equipment instance = new ItemInstance_Equipment(itemSO, level);

            instance.ChangedOption = new Ability.DynamicOption(Ability.Stat.EnumTotal, Ability.Enhance.EnumTotal, float.NaN);

            EquipmentDealer.ShuffleStatics(instance);
            EquipmentDealer.ShuffleDynamics(instance);
            return instance;
        }
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
#endregion


    #region fields & getter setter

    public Type EquipmentType;
    [Range(1, 50)]
    public int Level;
    public int Xp;
    public int NextXP;
    public float Durability;
    [SerializeField]
    public Ability.DynamicOption ChangedOption
        = new Ability.DynamicOption(Ability.Stat.EnumTotal, Ability.Enhance.EnumTotal, float.NaN);

    Dictionary<Ability.Stat, float> _statics = new Dictionary<Ability.Stat, float>();

    Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> _dynamics
        = new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>();

    /// <summary>
    ///  You can't change _stats with this accessor.<para></para>
    ///  use this[stat] to modify stats.<para></para>
    ///  * created for foreach access
    /// </summary>
    public Dictionary<Ability.Stat, float> StaticDictionary
    {
        get { return new Dictionary<Ability.Stat, float>(_statics); }
    }
    
    /// <summary>
    ///  You can't change _enchants with this accessor.<para></para>
    ///  use this[stat, enhance] to modify enchants.<para></para>
    ///  * created for foreach access
    /// </summary>
    public Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> DynamicDictionary
    {
        get { return new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>(_dynamics); }
    }
    #endregion


    #region Indexers

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
                return _ScaleAdjust(stat, value);
            else
                return float.NaN;
        }
        set
        {
            if (float.IsNaN(value))                         // set value NaN to remove property
            {
                if (_statics.ContainsKey(stat))
                    _statics.Remove(stat);
            }
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
    public float this[KeyValuePair<Ability.Stat, Ability.Enhance> key]
    {
        get
        {
            if (_dynamics.TryGetValue(key, out float value))
                return _ScaleAdjust(key, value);
            else
                return float.NaN;
        }
        set
        {
            if (float.IsNaN(value))                         // set value NaN to remove property
            {
                if (_dynamics.ContainsKey(key))
                    _dynamics.Remove(key);
            }
            else
                _dynamics[key] = value;
        }
    }
    /// <summary>
    /// Warping indexer
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
            return this[key];
        }
        set
        {
            KeyValuePair<Ability.Stat, Ability.Enhance> key
               = new KeyValuePair<Ability.Stat, Ability.Enhance>(stat, enhance);
            this[key] = value;
        }
    }
    #endregion


    #region to tables

    public Ability.StaticOption[] StaticOptions
    {
        get
        {
            List<Ability.StaticOption> optionList = new List<Ability.StaticOption>();

            foreach(var i in _statics)
            {
                optionList.Add(new Ability.StaticOption(i.Key, i.Value));
            }

            return optionList.ToArray();
        }
    }

    public Ability.DynamicOption[] DynamicOptions
    {
        get
        {
            List<Ability.DynamicOption> optionList = new List<Ability.DynamicOption>();

            foreach(var i in _dynamics)
            {
                optionList.Add(new Ability.DynamicOption(i.Key.Key, i.Key.Value, i.Value));
            }

            return optionList.ToArray();
        }
    }
    #endregion


    #region to string list

    public List<string> StaticProperties_ToString()
    {
        List<string> string_list = new List<string>();

        foreach(var key_value in _statics)
        {
            string_list.Add($"{key_value.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{_ScaleAdjust(key_value)}");
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
                    string_list.Add($"{ key_value.Key.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{_ScaleAdjust(key_value)}");
                    break;
                case Ability.Enhance.Chance_Percent:
                    string_list.Add($"More Chance of { key_value.Key.Key.ToString()} {_ScaleAdjust(key_value)*100}%");
                    break;
                case Ability.Enhance.NegMul_Percent:
                    string_list.Add($"Reduce { key_value.Key.Key.ToString()} by {_ScaleAdjust(key_value) * 100}%");
                    break;
                case Ability.Enhance.PosMul_Percent:
                    string_list.Add($"Increase { key_value.Key.Key.ToString()} by {_ScaleAdjust(key_value) * 100 + 100}%");
                    break;
                case Ability.Enhance.Addition_Percent:
                    string_list.Add($"{ key_value.Key.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{_ScaleAdjust(key_value) * 100}%");
                    break;
                case Ability.Enhance.Addition:
                    string_list.Add($"Additional { key_value.Key.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{_ScaleAdjust(key_value)}");
                    break;
            }
        }

        return string_list;
    }
    #endregion


    #region internal functions

    Type _CategoryToType(ItemSO.enumSubCategory sub)
    {
        switch(sub)
        {
            case ItemSO.enumSubCategory.MainWeapon:
                return Type.MainWeapon;
            case ItemSO.enumSubCategory.SubWeapon:
                return Type.SubWeapon;
            case ItemSO.enumSubCategory.Hat:
                return Type.Hat;
            case ItemSO.enumSubCategory.Chest:
                return Type.Chest;
            case ItemSO.enumSubCategory.Pants:
                return Type.Pants;
            case ItemSO.enumSubCategory.Hands:
                return Type.Hands;
            case ItemSO.enumSubCategory.Boots:
                return Type.Boots;
            default:
                return Type.EnumTotal;
        }
    }

    float _ScaleAdjust(KeyValuePair<Ability.Stat, float> staticOption)
    { return _ScaleAdjust(staticOption.Key, staticOption.Value); }

    float _ScaleAdjust(Ability.Stat stat, float value)
    {
        switch (stat)
        {
            case Ability.Stat.CriticalChance:
            case Ability.Stat.AttackSpeed:
            case Ability.Stat.MoveSpeed:
                return value;

            case Ability.Stat.Accuracy:
            case Ability.Stat.CriticalScale:
            case Ability.Stat.Dodge:
            case Ability.Stat.IncreaseDamage:
            case Ability.Stat.ReduceDamage:
            case Ability.Stat.SkillCooldown:
            case Ability.Stat.Tenacity:
                return Mathf.Ceil(value * Level / 50 * 100) / 100;

            case Ability.Stat.Attack:
            case Ability.Stat.Defense:
            case Ability.Stat.MaxHP:
            case Ability.Stat.MaxMP:
            case Ability.Stat.Strength:
            case Ability.Stat.Vitality:
                return Mathf.Ceil(value * Level / 50);

            default:    // EnumTotal
                return float.NaN;
        }
    }

    float _ScaleAdjust(KeyValuePair<KeyValuePair<Ability.Stat, Ability.Enhance>, float> dynamicOption)
    { return _ScaleAdjust(dynamicOption.Key, dynamicOption.Value); }

    float _ScaleAdjust(KeyValuePair<Ability.Stat, Ability.Enhance> option, float value)
    {
        switch (option.Value)
        {
            case Ability.Enhance.Absolute:
            case Ability.Enhance.Addition:
                return Mathf.Ceil(value * Level / 50);

            case Ability.Enhance.Addition_Percent:
            case Ability.Enhance.Chance_Percent:
            case Ability.Enhance.NegMul_Percent:
            case Ability.Enhance.PosMul_Percent:
                return Mathf.Ceil(value * Level / 50 * 100) / 100;

            default:
                return float.NaN;
        }
    }
    #endregion


    #region SAVE DATA IMPLEMENTATION

    [SerializeField]
    List<Ability.StaticOption> SD_staticOption = new List<Ability.StaticOption>();
    [SerializeField]
    List<Ability.DynamicOption> SD_dynamicOption = new List<Ability.DynamicOption>();

    void iSavedData.AfterLoad()
    {
        //Debug.Log("Equipment al");
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
        //Debug.Log("Equipment bs");
        id = so.ID;

        SD_staticOption.Clear();
        SD_dynamicOption.Clear();

        foreach(var kv in _statics)
        {
            SD_staticOption.Add(new Ability.StaticOption(kv.Key, kv.Value));
        }
        foreach(var kv in _dynamics)
        {
            SD_dynamicOption.Add(new Ability.DynamicOption(kv.Key, kv.Value));
        }
    }
    #endregion


    #region deprecateds
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
    #endregion
}
