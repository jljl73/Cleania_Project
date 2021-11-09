using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillKatharsis : PlayerSkill
{
    // public Status status;
    bool bSkill = false;

    float triggerAvailablePercent = 0.5f;
    float consumMPPerSec = 0f;
    float attackSpeedUpRate = 1f;
    float movekSpeedUpRate = 1f;
    float resistanceIncreaseRate = 1f;
    float strikingPowerIncreaseRate = 1.0f;
    float defenceIncreaseRate = 1.0f;

    public PlayerSkillKatharsisSO skillData;

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

        triggerAvailablePercent = skillData.GetTriggerAvailablePercent();
        consumMPPerSec = skillData.GetConsumMPPerSec();
        attackSpeedUpRate = skillData.GetAttackSpeedUpRate();
        movekSpeedUpRate = skillData.GetMoveSpeedUpRate();
        resistanceIncreaseRate = skillData.GetResistanceIncreaseRate();
        strikingPowerIncreaseRate = skillData.GetStrikingPowerIncreaseRate();
        defenceIncreaseRate = skillData.GetDefenceIncreaseRate();
    }

    public override bool IsAvailable()
    {
        return (OwnerAbilityStatus.MP > OwnerAbilityStatus.GetStat(Ability.Stat.MaxMP) * 0.5f) || bSkill;
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        if (bSkill)
        {
            OffSkill();
            return true;
        }

        animator.SetBool("OnSkill", true);
        animator.SetTrigger("OnSkillUltimate");

        return false;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
    }

    public override void Activate()
    {
        StartCoroutine(OnSkill());
    }

    IEnumerator OnSkill()
    {
        bSkill = true;
        // ��ų On���� ���� ���� �߻�
        print("��ų On���� ���� ���� �߻�");

        while (bSkill)
        {
            if (OwnerAbilityStatus.MP == 0)
            {
                base.Deactivate();
            }

            OwnerAbilityStatus.ConsumeMP(consumMPPerSec);
            yield return new WaitForSeconds(1.0f);
        }
    }

    void OffSkill()
    {
        bSkill = false;
        // ��ų Off���� ���� ����� �߻�
        print("��ų Off���� ���� ����� �߻�");
    }
}
