using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingProperty : MonoBehaviour
{
    protected AbilityStatus ownerAbility;
    protected float damageScale;
    protected float damageRange;
    protected bool isSetUp = false;
    public void SetUp(AbilityStatus abil, float damageScale)
    {
        ownerAbility = abil;
        this.damageScale = damageScale;
        isSetUp = true;
    }

    public void Resize(float resizedValue)
    {
        damageRange = resizedValue;
    }
}
