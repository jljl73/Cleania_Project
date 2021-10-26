using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    // "부분 애니메이션 배속"
    float liftUpSpeedMultiplier = 1.0f;
    public float GetLiftUpSpeedMultiplier() { return liftUpSpeedMultiplier; }

    float swingDownSpeedMultiplier = 1.0f;
    public float GetSwingDownSpeedMultiplier() { return swingDownSpeedMultiplier; }

    float otherSpeedMultiplier = 1.0f;
    public float GetOtherSpeedMultiplier() { return otherSpeedMultiplier; }


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
        animator.SetFloat("RefreshingLeapForward mulitplier", SpeedMultiplier);
        animator.SetFloat("RefreshingLeapForward_LiftUp mulitplier", liftUpSpeedMultiplier);
        animator.SetFloat("RefreshingLeapForward_SwingDown mulitplier", swingDownSpeedMultiplier);
        animator.SetFloat("RefreshingLeapForward_Other mulitplier", otherSpeedMultiplier);
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

        liftUpSpeedMultiplier = SkillData.GetLiftUpSpeedMultiplier();
        swingDownSpeedMultiplier = SkillData.GetSwingDownSpeedMultiplier();
        otherSpeedMultiplier = SkillData.GetOtherSpeedMultiplier();

        print("liftUpSpeedMultiplier: " + liftUpSpeedMultiplier);
        print("swingDownSpeedMultiplier: " + swingDownSpeedMultiplier);
        print("otherSpeedMultiplier: " + otherSpeedMultiplier);

        // animationSplitCount = SkillData.GetAnimationSplitCount();
    }

    public override void AnimationActivate()
    {
        //animator.SetInteger("Skill", 4);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill4", true);
        animator.SetTrigger("RefreshingLeapForward");
        Physics.IgnoreLayerCollision(3, 6);

        // playerMovement.JumpForward(jumpDistance);
        PlaySkillEvent.Invoke();
    }

    override public void Activate()
    {
        // effectController.PlaySkill();
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
                enemy.Stunned(true, stunTime);
            }
        }
    }
}
