using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealPond : DamagingProperty
{
    public float DamageRange = 1;
    Collider damageCollider;
    float duration;
    float silenceTime;
    bool isSealPondSetUp = false;

    [SerializeField]
    SkillEffectController effectController;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        if (damageCollider == null)
            throw new System.Exception("SealPond doesnt have Collider");
    }

    void Start()
    {
        if (!isSetUp) return;
        if (!isSealPondSetUp) return;

        Invoke("DoSilenceAttack", duration);
    }

    void DoSilenceAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                AbilityStatus abil = colliders[i].GetComponent<AbilityStatus>();
                if (abil == null) return;
                abil.AttackedBy(ownerAbility, damageScale);
                print("Ä§¹¬ ¾îÅÃ!");
            }
        }

        Destroy(this.gameObject);
    }

    public void SetUp(float duration, float silenceTime, float skillRange)
    {
        this.duration = duration;
        this.silenceTime = silenceTime;
        isSealPondSetUp = true;

        effectController.Scale = skillRange;
    }
}
