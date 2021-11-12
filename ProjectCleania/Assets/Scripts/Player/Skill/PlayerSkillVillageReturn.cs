using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillVillageReturn : PlayerSkill
{
    public PlayerSkillVillageReturnSO skillData;

    float timeCost;
    Vector3 returnPosition;

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private void Awake()
    {
        UpdateskillData();
    }

    protected new void Start()
    {
        base.Start();
    }

    public void UpdateskillData()
    {
        ID = skillData.ID;
        SkillName = skillData.GetSkillName();
        SkillDetails = skillData.GetSkillDetails();
        CoolTime = skillData.GetCoolTime();
        SpeedMultiplier = skillData.GetSpeedMultiplier();

        SkillSlotDependency = skillData.GetTriggerKey();
        timeCost = skillData.GetTimeCost();
        returnPosition = skillData.GetReturnPosition();
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillEtc", true);
        animator.SetTrigger("VillageReturn");

        return true;
    }

    public override void Activate()
    {
        //StartCoroutine(OnSkill());
        print("∏∂¿ª ±Õ»Ø!");
    }

    public override void Deactivate()
    {
        base.Deactivate();
        //effectController[0].PlaySkillEffect();
        animator.SetBool("OnSkillEtc", false);
        animator.SetBool("OnSkill", false);
    }
}
