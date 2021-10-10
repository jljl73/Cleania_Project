using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment //: IEnumerable, IEnumerator
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

    public Type EquipmentType = Type.MainWeapon;

    Dictionary<Ability.Stat, float> _staticOptions
        = new Dictionary<Ability.Stat, float>();
    Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> _dynamicOptions
        = new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>();

    /// <summary>
    ///  You can't change _stats with this accessor.
    ///  use this[stat] to modify stats.
    ///  * created for foreach access
    /// </summary>
    public Dictionary<Ability.Stat, float> StaticOptions
    {
        get { return new Dictionary<Ability.Stat, float>(_staticOptions); }
    }
    
    /// <summary>
    ///  You can't change _enchants with this accessor.
    ///  use this[stat, enhance] to modify enchants.
    ///  * created for foreach access
    /// </summary>
    public Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> DynamicOptions
    {
        get { return new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>(_dynamicOptions); }
    }

    public float this[Ability.Stat stat]                                                   // stat indexer
    {
        get
        {
            if (_staticOptions.TryGetValue(stat, out float value))
                return value;
            else
                return float.NaN;
        }
        set
        {
            if (float.IsNaN(value))                         // set value NaN to remove property
                if (_staticOptions.ContainsKey(stat))
                    _staticOptions.Remove(stat);
                else
                    _staticOptions[stat] = value;
        }
    }

    public float this[Ability.Stat stat, Ability.Enhance enhance]                    // enchant indexer
    {
        get
        {
            KeyValuePair<Ability.Stat, Ability.Enhance> key
                = new KeyValuePair<Ability.Stat, Ability.Enhance>(stat, enhance);

            if (_dynamicOptions.TryGetValue(key, out float value))
                return value;
            else
                return float.NaN;
        }
        set
        {
            KeyValuePair<Ability.Stat, Ability.Enhance> key
                = new KeyValuePair<Ability.Stat, Ability.Enhance>(stat, enhance);

            if (float.IsNaN(value))                         // set value NaN to remove property
                if (_dynamicOptions.ContainsKey(key))
                    _dynamicOptions.Remove(key);
                else
                    _dynamicOptions[key] = value;
        }
    }

    
    public List<string> StaticOptions_ToString()
    {
        List<string> string_list = new List<string>();

        foreach(var key_value in _staticOptions)
        {
            string_list.Add($"{key_value.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{key_value.Value}");
        }

        return string_list;
    }

    public List<string> DynamicOptions_ToString()
    {
        List<string> string_list = new List<string>();

        foreach (var key_value in _dynamicOptions)
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

}
