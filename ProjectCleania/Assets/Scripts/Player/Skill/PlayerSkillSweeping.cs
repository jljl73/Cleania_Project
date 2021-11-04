using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSweeping : PlayerSkill
{
    public PlayerSkillSweepingSO SkillData;
    float skillScale = 0.0f;

    CapsuleCollider col;

    // "���� �ð�"
    float stunTime = 2;
    public float GetStunTime() { return stunTime; }

    // "������ ����"
    float sweepRange = 2f;
    public float GetSweepRange() { return sweepRange; }

    public override int ID { get { return SkillData.ID; } protected set { id = value; } }

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
        ID = SkillData.ID;
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
        base.AnimationActivate();

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
            // ����
            other.GetComponent<Enemy>().OnStunned(true, stunTime);

            if (other.GetComponent<Enemy>().abilityStatus.AttackedBy(OwnerAbilityStatus, skillScale) == 0)
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
