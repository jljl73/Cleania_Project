using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDusting : PlayerSkill
{
    public PlayerSkillDustingSO SkillData;

    Collider attackArea;

    // "내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)"
    float damageRate = 5.4f;
    public float GetDamageRate() { return damageRate; }

    private void Awake()
    {
        UpdateSkillData();
    }

    new void Start()
    {
        //initialNavAgentR = navMeshAgent.radius;
        base.Start();

        GameManager.Instance.player.OnLevelUp += UpdateSkillData;
        attackArea = GetComponent<Collider>();
        animator.SetFloat("Dusting multiplier", SpeedMultiplier);
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

    // Update is called once per frame

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillC", true);
        animator.SetTrigger("Dusting");
        // animator.SetInteger("Skill", 5);
    }

    public override void Activate()
    {
        attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkillC", false);
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("L Hit");
            AbilityStatus enemyAbil = other.GetComponent<Enemy>().abilityStatus;

            if (enemyAbil.HP != 0)
                enemyAbil.AttackedBy(OwnerAbilityStatus, damageRate);
        }
    }
}
