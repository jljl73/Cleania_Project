using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityDamage : DamagingProperty
{
    //public float damageRange = 3;

    float timePassed = 0f;

    void Update()
    {
        if (!isSetUp) return;

        timePassed += Time.deltaTime;
        if (timePassed > 1f)
        {
            timePassed = 0f;
            GiveToxicity();
        }
    }

    void GiveToxicity()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                colliders[i].GetComponent<StatusAilment>()?.DamageContinuously(damageScale);
            }
        }
    }
}
