using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityDamage : DamagingProperty
{
    public float damageRange = 3;

    protected virtual void Update()
    {
        if (!isSetup) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                print("�ߵ� �����̻� �ο�!");
            }
        }
    }
}
