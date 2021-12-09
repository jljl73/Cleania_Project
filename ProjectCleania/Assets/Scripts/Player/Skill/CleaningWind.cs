using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningWind : ContactStayDamage
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

    //private void OnEnable()
    //{
    //    Invoke("DeactivateDelay", duration);
    //    Start();
    //}

    //private void OnDisable()
    //{
    //    CancelInvoke();
    //    ObjectPool.ReturnObject(ObjectPool.enumPoolObject.CleaningWind, this.gameObject);
    //}

    void Start()
    {
        // 리사이징
        //this.gameObject.transform.localScale = new Vector3(damageRange, damageRange, damageRange);
        skillEffectController.Scale = damageRange * 0.6666f;
        Destroy(this.gameObject, duration);
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    public void SetUp(float maxHitPerSameObject, float speed, float duration, AbilityStatus abil, float skillScale)
    {
        this.maxHitPerSameObject = maxHitPerSameObject;
        moveSpeed = speed;
        this.duration = duration;

        base.SetUp(abil, skillScale);
    }

    private void FixedUpdate()
    {
        if (!isSetUp) return;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            AbilityStatus abil = other.gameObject.GetComponent<AbilityStatus>();
            if (abil != null)
                abil.AttackedBy(ownerAbility, damageScale);
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            timePassed += Time.deltaTime;

            if (timePassed < 1f)
                return;
            else
                timePassed = 0f;

            AbilityStatus abil = other.gameObject.GetComponent<AbilityStatus>();
            if (abil != null)
                abil.AttackedBy(ownerAbility, damageScale);
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
