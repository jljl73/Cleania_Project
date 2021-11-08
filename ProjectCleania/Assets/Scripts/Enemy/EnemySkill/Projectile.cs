using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamagingProperty
{
    Collider damageCollider;
    SkillEffectController skillEffectController;

    public float moveSpeed = 5.0f;

    // "같은 피격체에 대한 최대 피해 적용 횟수"
    float maxHitPerSameObject = 2;

    // "유지 시간"
    float duration = 2f;

    bool canTakeDamage = true;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        skillEffectController = GetComponent<SkillEffectController>();
    }

    void Start()
    {
        Destroy(gameObject, duration);

        // 리사이징
        //this.gameObject.transform.localScale = new Vector3(damageRange, damageRange, damageRange);
        skillEffectController.Scale = damageRange;
    }
    
    public void SetUp(float maxHitPerSameObject, float duration, AbilityStatus abil, float skillScale)
    {
        this.maxHitPerSameObject = maxHitPerSameObject;
        this.duration = duration;

        base.SetUp(abil, skillScale);
    }

    private void FixedUpdate()
    {
        if (!isSetUp) return;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isSetUp) return;

        if (other.tag == "Enemy")
        {
            AbilityStatus enemyAbil = other.GetComponent<Enemy>().abilityStatus;

            if (enemyAbil.HP != 0)
                enemyAbil.AttackedBy(ownerAbility, damageScale * Time.fixedDeltaTime);
        }
    }

    void TurnOnCollider()
    {
        damageCollider.enabled = true;
    }

    void TurnOffCollider()
    {
        damageCollider.enabled = false;
    }
}
