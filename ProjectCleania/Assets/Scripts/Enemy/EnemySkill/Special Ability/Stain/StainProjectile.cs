using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainProjectile : DamagingProperty
{
    float stopTime;         // 총 stop 시간
    float stopStartTime;    // stop 시작 시간

    Rigidbody stainRigidbody;

    SphereCollider hitCollider;
    float destroyAttackScale = 2f;
    float destroyAttackRange = 2f;

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

    private void OnEnable()
    {
        if (!isSetUp) return;

        ResetSetting();
        Start();
    }

    private void OnDisable()
    {
        CancelInvoke();
        ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Stain, this.gameObject);
    }
    private void Start()
    {
        projectileBodyController.Scale = damageRange * 1.2f;
        hitCollider.radius = damageRange;

        bombEffectController.Scale = damageRange * 0.5f;

        Invoke("StopAtTop", stopStartTime);
    }

    void ResetSetting()
    {
        isDestroying = false;
        projectileBodyController.gameObject.SetActive(true);
        projectileBodyController.PlaySkillEffect();
        stainRigidbody.useGravity = true;
    }

    void StopAtTop()
    {
        StartCoroutine("Stop", stopTime);
        EnableCollider();
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    void EnableCollider()
    {
        hitCollider.enabled = true;
    }

    IEnumerator Stop(float time)
    {
        Vector3 tempVel = stainRigidbody.velocity;
        stainRigidbody.velocity = Vector3.zero;
        stainRigidbody.useGravity = false;

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
        bombEffectController.Scale = destroyAttackRange * 0.5f;
        hitCollider.radius = destroyAttackRange;                    // 디버깅 위해 존재
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
        //Destroy(gameObject, 2f);
        Invoke("DeactivateDelay", 2f);
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
            Invoke("DeactivateDelay", 2f);
            //Destroy(gameObject, 2f);
        }
    }

    
}
