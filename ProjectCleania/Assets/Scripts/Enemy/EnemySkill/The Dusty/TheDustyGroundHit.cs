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
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("GroundHit");
        return true;
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
        return;
    }
}
