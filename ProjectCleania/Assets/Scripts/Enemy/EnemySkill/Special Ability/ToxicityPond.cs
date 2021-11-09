using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityPond : ToxicityDamage
{
    [SerializeField]
    SkillEffectController effectController;

    private void Start()
    {
        effectController.Scale = damageRange;
        GiveDamageOnRange(damageRange);
    }

    public void SetUp(AbilityStatus abil, float damageScale, float damageRange)
    {
        base.SetUp(abil, damageScale);
        this.damageRange = damageRange;
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
