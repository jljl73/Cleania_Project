using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : MonoBehaviour
{
    public virtual int Level
    { get => -1; set { } }

    public virtual float this[Ability.Stat index]
    { get => float.NaN; }
}
