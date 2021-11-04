using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingProperty : MonoBehaviour
{
    public AbilityStatus OwnerAbility { get; set; }
    public float DamageScale { get; set; }
    public void SetUp(AbilityStatus abil, float damageScale)
    {
        OwnerAbility = abil;
        DamageScale = damageScale;
    }
}
