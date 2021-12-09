using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheDustyGroundHit : EnemySkill
{
    [SerializeField]
    GroundHitSO skillData;

    float damageScale;
    float damageRadius;
    float stunnedTime;
    float indirectDamageRate;
    float indirectDamageRadius;
    float triggerProbability;

    SphereCollider triggerCollider;

    [SerializeField]
    GameObject groundHitEffect;

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        triggerCollider = GetComponent<SphereCollider>();
    }

    private new void Start()
    {
        base.Start();

        UpdateSkillData();
        triggerCollider.center = triggerPosition;
        triggerCollider.radius = triggerRange;
        //effectController[0].Scale = damageRadius * 0.3333f;
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("TheDustyDustStorm no skillData");

        base.UpdateSkillData(skillData);
        damageScale = skillData.GetDamageRate();
        damageRadius = skillData.GetDamageRadius();
        stunnedTime = skillData.GetStunnedTime();
        indirectDamageRate = skillData.GetIndirectDamageRate();
        indirectDamageRadius = skillData.GetIndirectDamageRadius();
        triggerProbability = skillData.GetTriggerProbability();
    }

    public override bool AnimationActivate()
    {
        if (!(Random.Range(0.0f, 1.0f) <= triggerProbability))
            return false;

        animator.SetBool("OnSkill", true);
        animator.SetTrigger("GroundHit");
        return true;
    }

    public override void PlayEffects()
    {
        base.PlayEffects();
        //ObjectPool.SpawnFromPool<GroundHit>(ObjectPool.enumPoolObject.GroundHit,
        //                                    GetWorldTriggerPosition(triggerPosition),
        //                                    transform.rotation);
        Instantiate(groundHitEffect, GetWorldTriggerPosition(triggerPosition), transform.rotation);
    }

    public override void Activate()
    {
        base.Activate();

        DirectAttack();
        IndirectAttack();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        animator.SetBool("OnSkill", false);
        return;
    }

    void DirectAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(GetWorldTriggerPosition(triggerPosition), damageRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                AbilityStatus abil = colliders[i].GetComponent<AbilityStatus>();
                abil.AttackedBy(OwnerAbilityStatus, damageScale);

                PlayerController player = colliders[i].GetComponent<PlayerController>();
                player.Stunned(true, stunnedTime);
            }
        }
    }

    void IndirectAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(GetWorldTriggerPosition(triggerPosition), indirectDamageRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                AbilityStatus abil = colliders[i].GetComponent<AbilityStatus>();
                abil.AttackedBy(OwnerAbilityStatus, indirectDamageRate);

                Buffable playerBuffable = colliders[i].GetComponent<Buffable>();
                playerBuffable.AddBuff(-0.15f, Ability.Buff.MoveSpeed_Buff, 5);
            }
        }
    }

    //protected new void OnTriggerStay(Collider other)
    //{
    //    base.OnTriggerStay(other);
    //    print("OnTriggerStay Ground Hit");
    //}

    //protected new void OnTriggerExit(Collider other)
    //{
    //    base.OnTriggerExit(other);
    //    print("OnTriggerExit Ground Hit");
    //}
}
