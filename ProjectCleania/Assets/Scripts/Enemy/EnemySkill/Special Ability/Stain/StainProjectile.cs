using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainProjectile : DamagingProperty
{
    float stopTime;         // 총 stop 시간
    float stopStartTime;    // stop 시작 시간
    bool didStop = false;
    float spentTimeSum = 0f;

    Rigidbody stainRigidbody;

    SphereCollider hitCollider;
    public float destroyAttackScale = 2f;
    public float destroyAttackRange = 2f;

    bool isDestroying = false;

    [SerializeField]
    SkillEffectController projectileBodyController;

    [SerializeField]
    SkillEffectController bombEffectController;

    private void Awake()
    {
        stainRigidbody = GetComponent<Rigidbody>();
        if (stainRigidbody == null)
            throw new System.Exception("StainProjectile doesnt have Rigidbody");

        hitCollider = GetComponent<SphereCollider>();
        if (hitCollider == null)
            throw new System.Exception("StainProjectile doesnt have Collider");
    }

    private void Start()
    {
        projectileBodyController.Scale = damageRange;
        hitCollider.radius = damageRange * 0.5f;
    }

    private void Update()
    {
        if (!isSetUp) return;

        spentTimeSum += Time.deltaTime;

        if (spentTimeSum > stopStartTime)
        {
            if (didStop) return;
            StartCoroutine("Stop", stopTime);
            EnableCollider();
        }
    }

    void EnableCollider()
    {
        hitCollider.enabled = true;
    }

    IEnumerator Stop(float time)
    {
        Vector3 tempVel = stainRigidbody.velocity;
        stainRigidbody.velocity = Vector3.zero;
        stainRigidbody.useGravity = false;
        didStop = true;

        yield return new WaitForSeconds(time);

        stainRigidbody.useGravity = true;
        stainRigidbody.velocity = tempVel;
    }

    public void SetUp(float stopTime, float stopStartTime, float destroyAttackScale, float destroyAttackRange, AbilityStatus abil, float damageScale)
    {
        this.stopTime = stopTime;
        this.stopStartTime = stopStartTime;
        this.destroyAttackRange = destroyAttackRange;
        this.destroyAttackScale = destroyAttackScale;

        base.SetUp(abil, damageScale);
    }

    void DestroyAttack()
    {
        bombEffectController.Scale = destroyAttackRange;
        bombEffectController.PlaySkillEffect();
        Collider[] colliders = Physics.OverlapSphere(transform.position, destroyAttackRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                AbilityStatus abil = colliders[i].GetComponent<AbilityStatus>();
                if (abil == null) return;
                abil.AttackedBy(ownerAbility, destroyAttackScale);
            }
        }
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroying) return;

        if (other.CompareTag("Player"))
        {
            AbilityStatus abil = other.gameObject.GetComponent<AbilityStatus>();
            if (abil != null)
                abil.AttackedBy(ownerAbility, damageScale);

            bombEffectController.PlaySkillEffect();
            projectileBodyController.StopSKillEffect();

            hitCollider.enabled = false;
            stainRigidbody.useGravity = false;
            stainRigidbody.velocity = Vector3.zero;
            isDestroying = true;
            Invoke("DestroyAttack", 2);
        }

        if (other.CompareTag("Ground"))
        {
            bombEffectController.PlaySkillEffect();
            projectileBodyController.StopSKillEffect();

            hitCollider.enabled = false;
            stainRigidbody.useGravity = false;
            stainRigidbody.velocity = Vector3.zero;
            isDestroying = true;
            Destroy(gameObject, 2f);
        }
    }
}
