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
        // if 보유 클린 1000이상 & 모든 장비 내구도 1이상이면
            // if 장비 내구도 10% 미만
                // 장비 내구도 = 0
            // else
                // 내구도 10% 깎기

            // if 현재 경험치 < 현재 레벨 전체 경험치 * 0.1
                // 현재 경험치 = 0
            // else
                // 현재 경험치 -= 현재 레벨 전체 경험치 * 0.05

            // 클린 1000 사용
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
