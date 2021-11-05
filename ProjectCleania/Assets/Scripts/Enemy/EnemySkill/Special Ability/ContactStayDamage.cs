using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactStayDamage : DamagingProperty
{
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AbilityStatus abil = other.gameObject.GetComponent<AbilityStatus>();
            if (abil != null)
                abil.AttackedBy(OwnerAbility, DamageScale * Time.deltaTime);
        }
    }
}
