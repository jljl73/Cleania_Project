using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityPollution : EnemySkill
{
    float pollutionDuration = 3;
    float damageScale;
    float damageRange;

    //bool abilityActivate = false;

    //[SerializeField]
    //GameObject pollutionPrefab;

    [SerializeField]
    SpecialAbilityPollutionSO skillData;

    PollutionGroup pollutionGroup;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
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

    private void FixedUpdate()
    {
        //if (!abilityActivate) return;
        //Pollution pollution = ObjectPool.SpawnFromPool<Pollution>(ObjectPool.enumPoolObject.Pollution, this.transform.position, this.transform.rotation);
        //pollution.SetUp(pollutionDuration, OwnerAbilityStatus, damageRate);
        //pollution.Resize(damageRange);
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

        // ü�� ����
        //enemy.buff

        return true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        return;
    }
}
