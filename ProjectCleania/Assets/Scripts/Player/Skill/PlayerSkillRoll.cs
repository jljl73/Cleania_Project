using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillRoll : PlayerSkill
{
    // public Status status;
    bool bSkill = false;

    float avoidDistance;
    float avoidSpeedMultiplier;
    public float AvoidSpeedMultiplier { get => avoidSpeedMultiplier; }

    public PlayerSkillRollSO skillData;

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
        avoidDistance = skillData.GetAvoidDistance();
        avoidSpeedMultiplier = skillData.GetAvoidSpeedMultiplier();
    }

    //public override bool IsAvailable()
    //{
    //    return (OwnerAbilityStatus.MP > OwnerAbilityStatus.GetStat(Ability.Stat.MaxMP) * 0.5f) || bSkill;
    //}

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        //if (bSkill)
        //{
        //    OffSkill();
        //    return true;
        //}

        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillUltimate", true);
        animator.SetTrigger("Roll");

        return true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        effectController[0].PlaySkillEffect();
        animator.SetBool("OnSkillUltimate", false);
        animator.SetBool("OnSkill", false);
    }

    public override void Activate()
    {
        //StartCoroutine(OnSkill());
    }

    IEnumerator OnSkill()
    {
        bSkill = true;
        // 스킬 On으로 인한 버프 발생
        print("스킬 On으로 인한 버프 발생");

        while (bSkill)
        {
            if (OwnerAbilityStatus.MP == 0)
            {
                base.Deactivate();
            }

            //OwnerAbilityStatus.ConsumeMP(/*consumMPPerSec*/);
            yield return new WaitForSeconds(1.0f);
        }
    }

    void OffSkill()
    {
        bSkill = false;
        // 스킬 Off으로 인한 디버프 발생
        print("스킬 Off으로 인한 디버프 발생");
    }
}
