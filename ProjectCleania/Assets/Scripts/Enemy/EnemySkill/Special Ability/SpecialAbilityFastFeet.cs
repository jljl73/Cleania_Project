using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityFastFeet : EnemySkill
{
    [SerializeField]
    Buffable buffable;

    [SerializeField]
    SpecialAbilityFastFeetSO skillData;

    float speedIncreaseRate = 10;

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
            throw new System.Exception("SpecialAbilityFastFeet no skillData");

        if (buffable == null)
            throw new System.Exception("SpecialAbilityFastFeet no buffable");

        base.UpdateSkillData(skillData);

        speedIncreaseRate = skillData.GetSpeedIncreaseRate();
    }

    public override bool AnimationActivate()
    {
        // "공속 & 이속 증가!"
        buffable.ForceAddBuff(speedIncreaseRate, Ability.Buff.AttackSpeed_Buff);
        buffable.ForceAddBuff(speedIncreaseRate, Ability.Buff.MoveSpeed_Buff);

        return true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        return;
    }
}
