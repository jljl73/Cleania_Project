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

    //private void OnEnable()
    //{
    //    if (!isSetUp) return;
    //    Start();
    //}

    //private void OnDisable()
    //{
    //    CancelInvoke();
    //    ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Seal, this.gameObject);
    //}

    void Start()
    {
        if (!isSetUp) return;
        if (!isSealPondSetUp) return;

        Invoke("DoSilenceAttack", duration);
        Destroy(this.gameObject, duration + 0.5f);
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
                
                // µ¥¹ÌÁö
                abil.AttackedBy(ownerAbility, damageScale);

                // Ä§¹¬
                colliders[i].GetComponent<StatusAilment>()?.RestrictBehavior(StatusAilment.BehaviorRestrictionType.Silence, 7);
            }
        }

        this.gameObject.SetActive(false);
    }

    public void SetUp(float duration, float silenceTime, float skillRange)
    {
        this.duration = duration;
        this.silenceTime = silenceTime;
        isSealPondSetUp = true;

        effectController.Scale = skillRange * 0.6666f;
    }
}
