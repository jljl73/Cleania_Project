using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicityPond : ToxicityDamage
{
    [SerializeField]
    SkillEffectController effectController;

    float duration;

    //private void OnEnable()
    //{
    //    if (!isSetUp) return;
    //    Start();
    //}

    //private void OnDisable()
    //{
    //    CancelInvoke();
    //    ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Toxicity, this.gameObject);
    //}

    private void Start()
    {
        effectController.Scale = damageRange;
        GiveDamageOnRange(damageRange);

        // Invoke("DeactivateDelay", duration);
        Destroy(this.gameObject, duration);
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    public void SetUp(float duration, AbilityStatus abil, float damageScale, float damageRange)
    {
        this.duration = duration;
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
