using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondDamage : MonoBehaviour
{
    public AwakeDamage awakeDamage;
    public ContactDamage contactDamage;

    public void SetProperty(AbilityStatus abil, float damageScale)
    {
        awakeDamage.OwnerAbility = abil;
        awakeDamage.DamageScale = damageScale;

        contactDamage.OwnerAbility = abil;
        contactDamage.DamageScale = damageScale;
    }
}
