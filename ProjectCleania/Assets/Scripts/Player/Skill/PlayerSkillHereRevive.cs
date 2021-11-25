using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillHereRevive : PlayerSkill
{
    [SerializeField]
    PlayerSkillReviveSO skillData;

    float timeCost;
    float consumedXPRate = 0.1f;
    float consumedDurabilityRate = 0.1f;
    int consumedClean = 1000;

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        UpdateSkillData();
    }

    protected new void Start()
    {
        base.Start();
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);
        consumedXPRate = skillData.GetConsumedXPRate();
        consumedDurabilityRate = skillData.GetConsumedDurabilityRate();
        consumedClean = skillData.GetConsumedClean();
    }

    public override bool IsAvailable()
    {
        // if ���� Ŭ�� 1000�̻� & ��� ��� ������ 1�̻��̸�
            // if ��� ������ 10% �̸�
                // ��� ������ = 0
            // else
                // ������ 10% ���

            // if ���� ����ġ < ���� ���� ��ü ����ġ * 0.1
                // ���� ����ġ = 0
            // else
                // ���� ����ġ -= ���� ���� ��ü ����ġ * 0.05

            // Ŭ�� 1000 ���
            // return true;
        // else
            // return false;
        return true;
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        animator.SetTrigger("Revive");
        return true;
    }

    public override void Activate()
    {
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
