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

    private void Awake()
    {
        UpdateSkillData();
    }

    new void Start()
    {
        GameManager.Instance.player.OnLevelUp += UpdateSkillData;

        attackArea = GetComponent<Collider>();
        //initialNavAgentR = navMeshAgent.radius;
        base.Start();
        animator.SetFloat("Dehydration multiplier", SpeedMultiplier);
    }

    public void UpdateSkillData()
    {
        SkillName = SkillData.GetSkillName();
        SkillDetails = SkillData.GetSkillDetails();
        CoolTime = SkillData.GetCoolTime();
        CreatedMP = SkillData.GetCreatedMP();
        ConsumMP = SkillData.GetConsumMP();
        SpeedMultiplier = SkillData.GetSpeedMultiplier();

        SkillSlotDependency = SkillData.GetTriggerKey();

        damageRate = SkillData.GetDamageRate();
    }

    public override void AnimationActivate()
    {
        // animator.SetInteger("Skill", 6);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillR", true);
        animator.SetTrigger("Dehydration");
    }

    public override void Activate()
    {
        attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkillR", false);
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<Enemy>().abilityStatus.AttackedBy(OwnerAbilityStatus, skillScale) == 0)
                other.GetComponent<Enemy>().Die();
        }
    }
}
