using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityPond : ToxicityDamage
{
    private void Start()
    {
        GiveDamageOnRange(damageRange);
    }

    void GiveDamageOnRange(float range)
    {
        if (!isSetUp) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                AbilityStatus abil = colliders[i].GetComponent<AbilityStatus>();
                if (abil == null) return;
                abil.AttackedBy(ownerAbility, damageScale);
            }
        }
    }
}
