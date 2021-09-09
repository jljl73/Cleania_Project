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

    public Type equipmentType = Type.MainWeapon;
    public float strength = 0;
    public float atk = 0;
    public float atkPerSecond = 1.0f;
    public float def = 0;

    Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> _enchants
        = new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>();

    public Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float> enchant    // enchants getter (used for foreach only)
    {
        get { return new Dictionary<KeyValuePair<Ability.Stat, Ability.Enhance>, float>(_enchants); }
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
    }

    public float this[Ability.Stat stat]                                                   // stat indexer
    {
        get
        {
            switch (stat)
            {
                case Ability.Stat.Strength:
                    return strength;
                case Ability.Stat.Attack:
                    return atk;
                case Ability.Stat.AttackSpeed:
                    return atkPerSecond;
                case Ability.Stat.Defense:
                    return def;

                default:
                    return 0;
            }
        }
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
