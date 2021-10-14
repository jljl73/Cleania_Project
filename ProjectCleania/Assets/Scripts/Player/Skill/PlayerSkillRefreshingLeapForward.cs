using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillRefreshingLeapForward : PlayerSkill
{
    public PlayerSkillRefreshingLeapForwardSO SkillData;

    public AbilityStatus abilityStatus;
    // public float skillScale = 1.0f;

    public TestPlayerMove playerMovement;

    float jumpDistance = 7f;
    public float GetJumpDistance() { return jumpDistance; }

    // "내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)"
    float smashDamageRate = 5.4f;
    public float GetSmashDamageRate() { return smashDamageRate; }

    // "내려치기 범위"
    float smashRange = 2f;
    public float GetSmashRange() { return smashRange; }

    // "경직 시간"
    float stunTime = 1.5f;
    public float GetStunTime() { return stunTime; }

    // "슬로우 시간"
    float slowTime = 2f;
    public float GetSlowTime() { return slowTime; }


    Ray ray;
    RaycastHit hit;
    CapsuleCollider attackArea;

    private void Awake()
    {
        UpdateSkillData();
    }

    new void Start()
    {
        //initialNavAgentR = navMeshAgent.radius;
        base.Start();

        GameManager.Instance.player.OnLevelUp += UpdateSkillData;
        attackArea = GetComponent<CapsuleCollider>();
        attackArea.radius = smashRange;
        animator.SetFloat("RefreshingLeapForward multiplier", SpeedMultiplier);
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

        jumpDistance = SkillData.GetJumpDistance();
        smashDamageRate = SkillData.GetSmashDamageRate();
        smashRange = SkillData.GetSmashRange();
        stunTime = SkillData.GetStunTime();
        slowTime = SkillData.GetSlowTime();
    }

    public override void AnimationActivate()
    {
        //animator.SetInteger("Skill", 4);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill4", true);
        animator.SetTrigger("RefreshingLeapForward");
        Physics.IgnoreLayerCollision(3, 6);
        playerMovement.JumpForward(jumpDistance);
    }

    override public void Activate()
    {
        attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        Physics.IgnoreLayerCollision(3, 6, false);
        animator.SetBool("OnSkill4", false);
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            AbilityStatus enemyAbil = other.GetComponent<Enemy>().abilityStatus;
            if (enemyAbil.HP != 0)
            {
                enemyAbil.AttackedBy(abilityStatus, smashDamageRate);
                enemy.Stunned(stunTime);
            }
        }
    }
}
