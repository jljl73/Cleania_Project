using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheDustyGroundHit : EnemySkill
{
    [SerializeField]
    GroundHitSO skillData;

    float attackPoseFromTrigger = 2f;
    float damageScale;
    float damageRadius;
    float stunnedTime;
    float indirectDamageRate;
    float indirectDamageRadius;
    float triggerProbability;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
    }

    private new void Start()
    {
        base.Start();

        UpdateSkillData();
        //effectController[0].Scale = damageRadius * 0.3333f;
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("TheDustyDustStorm no skillData");

        base.UpdateSkillData(skillData);
        damageScale = skillData.GetDamageRate();
        damageRadius = skillData.GetDamageRadius();
        attackPoseFromTrigger = skillData.GetAttackPoseFromTrigger();
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

    public override void Activate()
    {
        base.Activate();

        ObjectPool.SpawnFromPool<GroundHit>(ObjectPool.enumPoolObject.GroundHit, transform.position + transform.forward * attackPoseFromTrigger, transform.rotation);

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
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * attackPoseFromTrigger, damageRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                Player player = colliders[i].GetComponent<Player>();
                player.abilityStatus.AttackedBy(OwnerAbilityStatus, damageScale);
                player.OnStunned(true, stunnedTime);
            }
        }
    }

    void IndirectAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * attackPoseFromTrigger, indirectDamageRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                Player player = colliders[i].GetComponent<Player>();
                player.abilityStatus.AttackedBy(OwnerAbilityStatus, indirectDamageRate);

                print("슬로우 상태!");
                //player.OnStunned(true, stunnedTime);
            }
        }
    }
}
