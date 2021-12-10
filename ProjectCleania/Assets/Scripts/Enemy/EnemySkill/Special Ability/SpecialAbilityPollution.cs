using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityPollution : EnemySkill
{
    float pollutionDuration = 3;
    float damageScale;
    float damageRange;

    //bool abilityActivate = false;

    // [SerializeField]
    // GameObject pollutionPrefab;

    [SerializeField]
    SpecialAbilityPollutionSO skillData;

    PollutionGroup pollutionGroup;

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        pollutionGroup = GetComponentInChildren<PollutionGroup>();
    }

    private new void Start()
    {
        base.Start();

        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("SpecialAbilityIngrainedDirt no skillData");

        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
        damageRange = skillData.GetDamageRange();
        pollutionDuration = skillData.GetDuration();
    }

    public override bool AnimationActivate()
    {
        UpdateSkillData();

        pollutionGroup.SetUp(OwnerAbilityStatus, damageScale);
        pollutionGroup.Resize(damageRange);
        pollutionGroup.Activate(true);

        // 체력 증가
        //enemy.buff

        return true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        return;
    }
}
