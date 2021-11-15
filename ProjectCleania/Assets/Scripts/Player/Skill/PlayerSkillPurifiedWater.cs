using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillPurifiedWater : PlayerSkill
{
    float HPRecoveryRate;
    float MPRecoveryRate;

    public PlayerSkillPurifiedWaterSO skillData;

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
        HPRecoveryRate = skillData.GetHPRecoveryRate();
        MPRecoveryRate = skillData.GetMPRecoveryRate();
    }

    public override bool IsAvailable()
    {
        return (int)OwnerAbilityStatus.HP != (int)OwnerAbilityStatus.GetStat(Ability.Stat.MaxHP);
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();
        // ü�� 100�̸� ��� �Ұ�
        print("��ü ü���� 60% ȸ��! ��ü �ڽ�Ʈ 30% ȸ��!");
        
        return true;
    }
}
