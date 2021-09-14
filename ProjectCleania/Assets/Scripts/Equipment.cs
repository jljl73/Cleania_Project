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

    Dictionary<Ability.Stat, float> _stats
        = new Dictionary<Ability.Stat, float>();
    Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> _enchants
        = new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>();

    /// <summary>
    ///  You can't change _stats with this accessor.
    ///  use this[stat] to modify stats.
    ///  * created for foreach access
    /// </summary>
    public Dictionary<Ability.Stat, float> StaticProperties
    {
        get { return new Dictionary<Ability.Stat, float>(_stats); }
    }
    
    /// <summary>
    ///  You can't change _enchants with this accessor.
    ///  use this[stat, enhance] to modify enchants.
    ///  * created for foreach access
    /// </summary>
    public Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> DynamicProperties
    {
        get { return new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>(_enchants); }
    }

    public float this[Ability.Stat stat]                                                   // stat indexer
    {
        get
        {
            _stats.TryGetValue(stat, out float value);

            return value;
        }
        set
        {
            if (value == 0)                         // set value 0 to remove property
                if (_stats.ContainsKey(stat))
                    _stats.Remove(stat);
                else
                {
                    _stats[stat] = value;
                }
        }
    }

    public float this[Ability.Stat stat, Ability.Enhance enhance]                    // enchant indexer
    {
        get
        {
            KeyValuePair<Ability.Stat, Ability.Enhance> key
                = new KeyValuePair<Ability.Stat, Ability.Enhance>(stat, enhance);

            _enchants.TryGetValue(key, out float value);

            return value;
        }
        set
        {
            KeyValuePair<Ability.Stat, Ability.Enhance> key
                = new KeyValuePair<Ability.Stat, Ability.Enhance>(stat, enhance);

            if (value == 0)                         // set value 0 to remove property
                if (_enchants.ContainsKey(key))
                    _enchants.Remove(key);
                else
                {
                    _enchants[key] = value;
                }
        }
    }

    
    public List<string> StaticProperties_ToString()
    {
        List<string> string_list = new List<string>();

        foreach(var key_value in _stats)
        {
            string_list.Add($"{key_value.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{key_value.Value}");
        }

        return string_list;
    }

    public List<string> DynamicProperties_ToString()
    {
        List<string> string_list = new List<string>();

        foreach (var key_value in _enchants)
        {
            switch (key_value.Key.Value)
            {
                case Ability.Enhance.Absolute:
                    string_list.Add($"{ key_value.Key.Key.ToString()} {(key_value.Value < 0 ? "-" : "+")}{key_value.Value}");
                    break;
                case Ability.Enhance.NegMul_Percent:
                    string_list.Add($"Reduce { key_value.Key.Key.ToString()} by {key_value.Value*100}%");
                    break;
                case Ability.Enhance.PosMul_Percent:
                    string_list.Add($"Increase { key_value.Key.Key.ToString()} into {key_value.Value*100 + 100}%");
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
