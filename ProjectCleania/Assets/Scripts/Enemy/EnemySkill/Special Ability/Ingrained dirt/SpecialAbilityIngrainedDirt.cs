using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialAbilityIngrainedDirt : EnemySkill
{
    float hPIncreaseRate = 10;

    [SerializeField]
    SpecialAbilityIngrainedDirtSO skillData;

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
            throw new System.Exception("SpecialAbilityIngrainedDirt no skillData");

        base.UpdateSkillData(skillData);

        hPIncreaseRate = skillData.GetHPIncreaseRate();
        //stainRadius = skillData.GetStainRadius();
        //stainAvailableAreaRadius = skillData.GetCreationRadius();
        //stainCount = skillData.GetCount();
        //stopTime = skillData.GetStopTime();
        //projFlightTime = skillData.GetProjFlightTime();
    }

    public override bool AnimationActivate()
    {
        // 체력 증가
        print("체력 증가!");
        //enemy.buff
        return true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        return;
    }
}
