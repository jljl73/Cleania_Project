using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillRoll : PlayerSkill
{
    [SerializeField]
    PlayerSkillRollSO skillData;

    bool bSkill = false;

    float avoidDistance;
    float avoidSpeedMultiplier;
    public float AvoidSpeedMultiplier { get => avoidSpeedMultiplier; }

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        UpdateSkillData();
    }

    protected new void Start()
    {
        base.Start();
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);
        
        avoidDistance = skillData.GetAvoidDistance();
        avoidSpeedMultiplier = skillData.GetAvoidSpeedMultiplier();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        effectController[0].PlaySkillEffect();
    }
}
