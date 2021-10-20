using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSweeping : PlayerSkill
{
    public PlayerSkillSweepingSO SkillData;
    public AbilityStatus abilityStatus;
    float skillScale = 0.0f;

    CapsuleCollider col;

    // "경직 시간"
    float stunTime = 2;
    public float GetStunTime() { return stunTime; }

    // "쓸어담기 범위"
    float sweepRange = 2f;
    public float GetSweepRange() { return sweepRange; }

    private void Awake()
    {
        col = GetComponent<CapsuleCollider>();
        UpdateSkillData();
    }

    protected new void Start()
    {
        base.Start();
        GameManager.Instance.player.OnLevelUp += UpdateSkillData;
        animator.SetFloat("Sweeping multiplier", SpeedMultiplier);
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

        stunTime = SkillData.GetStunTime();
        sweepRange = SkillData.GetSweepRange();
        col.radius = sweepRange;
    }

    public override void AnimationActivate()
    {
        //animator.SetInteger("Skill", 2);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill2", true);
        animator.SetTrigger("Sweeping");
    }

    override public void Activate()
    {
        col.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // 기절
            other.GetComponent<Enemy>().Stunned(true, stunTime);

            if (other.GetComponent<Enemy>().abilityStatus.AttackedBy(abilityStatus, skillScale) == 0)
                other.GetComponent<Enemy>().Die();
            else
                other.GetComponent<Enemy>().enemyMove.WarpToPosition(transform.position + transform.forward);
        }
    }

    void OffSkill()
    {
        col.enabled = false;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill2", false);
        animator.SetBool("OnSkill", false);
        OffSkill();
    }
}
