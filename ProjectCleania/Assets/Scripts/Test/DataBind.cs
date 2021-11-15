using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Specialized;

public interface INotifyCollectionChanged
{
    event Action<string> collectionChanged;
}

public class DataBind
{
    public event Action<string> contextChanged = delegate { };

    IDictionary<string, object> activeBinds = new Dictionary<string, object>();

    public bool Contains(string key)
    {
        return activeBinds.ContainsKey(key);
    }

    public object this[string key]
    {
        get
        {
            return activeBinds[key];
        }
        set
        {
            if (value == null) return;

            if (value is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)value).collectionChanged += contextChanged;
            }

            activeBinds[key] = value;
            contextChanged(key);
        }
    }

}
