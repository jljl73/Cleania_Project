using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondDamage : MonoBehaviour
{
    public AwakeDamage AwakeDamageCompo;
    public ContactStayDamage ContactStayDamageCompo;
    public DestroyDamage DestroyDamageCompo;

    public void SetProperty(AbilityStatus abil, float damageScale)
    {
        if (AwakeDamageCompo != null)
        {
            AwakeDamageCompo.OwnerAbility = abil;
            AwakeDamageCompo.DamageScale = damageScale;
        }

        if (ContactStayDamageCompo != null)
        {
            ContactStayDamageCompo.OwnerAbility = abil;
            ContactStayDamageCompo.DamageScale = damageScale;
        }

        if (DestroyDamageCompo != null)
        {
            DestroyDamageCompo.OwnerAbility = abil;
            DestroyDamageCompo.DamageScale = damageScale;
        }
    }
}
