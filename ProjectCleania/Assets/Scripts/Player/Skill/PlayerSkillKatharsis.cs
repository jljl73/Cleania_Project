using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillKatharsis : PlayerSkill
{
    [SerializeField]
    PlayerSkillKatharsisSO skillData;

    // public Status status;
    bool bSkill = false;

    float triggerAvailablePercent = 0.5f;
    float consumMPPerSec = 0f;
    float attackSpeedUpRate = 1f;
    float movekSpeedUpRate = 1f;
    float resistanceIncreaseRate = 1f;
    float strikingPowerIncreaseRate = 1.0f;
    float defenceIncreaseRate = 1.0f;


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
            // 애니메이션 실행x
            OffSkill();
            return false;
        }
        // 애니메이션 실행

        return true;
    }

    public override void Activate(int idx)
    {
        base.Activate(idx);
        StartCoroutine(OnSkill(idx));
    }

    public override void Deactivate(int idx)
    {
        base.Deactivate(idx);
        if (bSkill)
        {
            // 애니메이션 실행x
            OffSkill();
        }
    }

    IEnumerator OnSkill(int effectIdx)
    {
        bSkill = true;
        // 스킬 On으로 인한 버프 발생
        print("스킬 On으로 인한 버프 발생");

        base.PlayEffects(effectIdx);

        while (bSkill)
        {
            if (OwnerAbilityStatus.MP == 0)
            {
                base.Deactivate();
            }

            OwnerAbilityStatus.ConsumeMP(consumMPPerSec);
            yield return new WaitForSeconds(1.0f);
        }

        base.StopEffects(effectIdx);
    }

    void OffSkill()
    {
        bSkill = false;
        // 스킬 Off으로 인한 디버프 발생
        print("스킬 Off으로 인한 디버프 발생");
    }
}
