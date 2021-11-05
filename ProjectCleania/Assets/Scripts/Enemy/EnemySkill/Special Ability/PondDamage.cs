using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondDamage : DamagingProperty
{
    public float damageRange = 3;

    private void Start()
    {
        if (!isSetup) return; 

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
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

    private void Update()
    {
        if (!isSetup) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                print("중독 상태이상 부여!");
            }
        }
    }
}
