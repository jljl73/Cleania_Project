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

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        if (damageCollider == null)
            throw new System.Exception("SealPond doesnt have Collider");
    }

    void Start()
    {
        print("a");
        if (!isSetup) return;
        print("b");
        if (!isSealPondSetUp) return;
        print("c");

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
                print("Ä§¹¬ ¾îÅÃ!");
            }
        }

        Destroy(this.gameObject);
    }

    public void SetUp(float duration, float silenceTime)
    {
        this.duration = duration;
        this.silenceTime = silenceTime;
        isSealPondSetUp = true;
    }
}
