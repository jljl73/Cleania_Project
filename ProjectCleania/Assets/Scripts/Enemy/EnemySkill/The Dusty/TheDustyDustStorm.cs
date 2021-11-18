using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDustyDustStorm : EnemySkill
{
    [SerializeField]
    DustStormSO skillData;

    float damageScale;
    float damageRadius = 1;
    float pulledSpeed = 5;
    float stormDuration = 5;
    float stormDamageRate = 1;
    float stormDamageRadius = 1;
    float stormForce = 100f;
    float sightHindRange = 0.3f;
    float sightHindDuration = 4f;
    float triggerProbability = 0.3f;

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
        pulledSpeed = skillData.GetPulledSpeed();
        stormDuration = skillData.GetstormDuration();
        stormDamageRate = skillData.GetStormDamageRate();
        stormDamageRadius = skillData.GetStormDamageRadius();
        stormForce = skillData.GetStormForce();
        sightHindRange = skillData.GetSightHindRange();
        sightHindDuration = skillData.GetsightHindDuration();
        triggerProbability = skillData.GetTriggerProbability();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("DustStorm");
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
