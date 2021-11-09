using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDusting : PlayerSkill
{
    public PlayerSkillDustingSO SkillData;

    Collider attackArea;

    // "����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)"
    float damageRate = 5.4f;
    public float GetDamageRate() { return damageRate; }

    public override int ID { get { return SkillData.ID; } protected set { id = value; } }

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
        ID = SkillData.ID;
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

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillC", true);
        animator.SetTrigger("Dusting");
        // animator.SetInteger("Skill", 5);

        return true;
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
            AbilityStatus enemyAbil = other.GetComponent<Enemy>().abilityStatus;

            if (enemyAbil.HP != 0)
            {
                enemyAbil.AttackedBy(OwnerAbilityStatus, damageRate);
                OwnerAbilityStatus.ConsumeMP(-CreatedMP);
            }
        }
    }
}
