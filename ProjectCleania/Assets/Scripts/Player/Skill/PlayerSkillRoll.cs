using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillRoll : PlayerSkill
{
    bool rolling = false;

    [SerializeField]
    PlayerSkillRollSO skillData;

    [SerializeField]
    Buffable buffable;

    float avoidSpeedMultiplier;
    public float AvoidSpeedMultiplier { get => avoidSpeedMultiplier; }

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
    }

    protected new void Start()
    {
        base.Start();
        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);
        
        avoidSpeedMultiplier = skillData.GetAvoidSpeedMultiplier();
    }

    public override void Activate()
    {
        base.Activate();

        if (rolling == false)
        {
            buffable.ForceAddBuff(avoidSpeedMultiplier, Ability.Buff.MoveSpeed_Buff);
            rolling = true;
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();

        if (rolling == true)
        {
            effectController[0].PlaySkillEffect();
            buffable.ForceOffBuff(avoidSpeedMultiplier, Ability.Buff.MoveSpeed_Buff);
            rolling = false;
        }
    }
}
