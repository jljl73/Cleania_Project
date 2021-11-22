using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillPurifiedWater : PlayerSkill
{
    [SerializeField]
    PlayerSkillPurifiedWaterSO skillData;

    float HPRecoveryRate;
    float MPRecoveryRate;

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        UpdateskillData();
    }

    protected new void Start()
    {
        base.Start();
    }

    public void UpdateskillData()
    {
        base.UpdateSkillData(skillData);

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
