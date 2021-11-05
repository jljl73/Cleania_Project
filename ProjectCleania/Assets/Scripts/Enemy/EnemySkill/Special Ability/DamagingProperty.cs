using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingProperty : MonoBehaviour
{
    protected AbilityStatus ownerAbility;
    protected float damageScale;
    protected bool isSetup = false;
    public void SetUp(AbilityStatus abil, float damageScale)
    {
        ownerAbility = abil;
        this.damageScale = damageScale;
        isSetup = true;
    }
}
