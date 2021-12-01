using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityFastFeet : EnemySkill
{
    float speedIncreaseRate = 10;

    [SerializeField]
    SpecialAbilityFastFeetSO skillData;

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
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

        speedIncreaseRate = skillData.GetSpeedIncreaseRate();
    }

    public override bool AnimationActivate()
    {
        // 체력 증가
        print("공속 & 이속 증가!");
        //enemy.buff

        return true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        return;
    }
}
