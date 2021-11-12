using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDehydration : PlayerSkill
{
    public PlayerSkillDehydrationSO SkillData;

    public float skillScale = 1.0f;

    Collider attackArea;

    // "내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)"
    float damageRate = 3.0f;
    public float GetDamageRate() { return damageRate; }

    public float damageRange = 1.0f;
    public float GetDamageRange() { return damageRange; }

    public override int ID { get { return SkillData.ID; } protected set { id = value; } }
    private void Awake()
    {
        attackArea = GetComponent<Collider>();

        UpdateSkillData();
    }

    new void Start()
    {
        GameManager.Instance.player.OnLevelUp += UpdateSkillData;

        base.Start();
        animator.SetFloat("Dehydration multiplier", SpeedMultiplier);

        effectController[0].Scale = damageRange * 0.5f;
    }

    public void UpdateSkillData()
    {
        ID = SkillData.ID;
        SkillName = SkillData.GetSkillName();
        SkillDetails = SkillData.GetSkillDetails();
        CoolTime = SkillData.GetCoolTime();
        CreatedMP = SkillData.GetCreatedMP();
        ConsumMP = SkillData.GetConsumMP();
        SpeedMultiplier = SkillData.GetSpeedMultiplier();

        SkillSlotDependency = SkillData.GetTriggerKey();

        damageRate = SkillData.GetDamageRate();
        damageRange = SkillData.GetDamageRange();
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        // animator.SetInteger("Skill", 6);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillR", true);
        animator.SetTrigger("Dehydration");

        return true;
    }

    public override void StopSkill()
    {
        //Deactivate();

        animator.SetTrigger("DehydrationEnd");
        animator.SetBool("OnSkillR", false);
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;

        effectController[0].StopSKillEffect();

    }

    public override void Activate()
    {
        attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        //animator.SetTrigger("DehydrationEnd");
        //animator.SetBool("OnSkillR", false);
        //animator.SetBool("OnSkill", false);
        //attackArea.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().abilityStatus.AttackedBy(OwnerAbilityStatus, skillScale * Time.deltaTime);
        }
    }
}
