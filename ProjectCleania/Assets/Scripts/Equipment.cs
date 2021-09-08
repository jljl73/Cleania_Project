using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : IEnumerable, IEnumerator
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

    public float this[AbilityOption.Stat stat, AbilityOption.Enhance enhance]        // indexer
    {
        get
        {
            KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance> key
                = new KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance>(stat, enhance);

            _options.TryGetValue(key, out float value);

            return value;
        }
    }

    public float this[AbilityOption.Stat stat]        // indexer
    {
        get
        {
            switch (stat)
            {
                case AbilityOption.Stat.Strength:
                    return strength;
                case AbilityOption.Stat.Attack:
                    return atk;
                case AbilityOption.Stat.AttackSpeed:
                    return atkPerSecond;
                case AbilityOption.Stat.Defense:
                    return def;

                default:
                    return 0;
            }
        }
    }

    public Type equipmentType = Type.MainWeapon;
    public float strength = 0;
    public float atk = 0;
    public float atkPerSecond = 1.0f;
    public float def = 0;

    Dictionary<KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance>, float> _options
        = new Dictionary<KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance>, float>();

    public Dictionary<KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance>, float> options
    {
        get { return new Dictionary<KeyValuePair<AbilityOption.Stat, AbilityOption.Enhance>, float>(_options); }
    }
    
    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)this;
    }

    //public IEnumerator<AbilityOption.Enchant> GetEnumerator()
    //{
    //    return (IEnumerator<AbilityOption.Enchant>)this;
    //}

    public bool MoveNext()
    {
        return true;
    }

    public void Reset()
    {

    }

    public object Current
        { get
        {
            return 0;
        }
        }

}
