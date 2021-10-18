using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillFairysWings : PlayerSkill
{
    public PlayerSkillFairysWingsSO SkillData;

    public Buffable buffManager;
    // public float speed = 10.0f;

    // "���� �ð�"
    float duration = 0f;
    public float GetDuration() { return duration; }

    // "�ӵ� ��·� (ex. 0.4 = 40% ����)"
    float speedUpRate = 1.4f;
    public float GetSpeedUpRate() { return speedUpRate; }

    bool bSkill = false;
    int nDeadEnemy = 0;

    private void Awake()
    {
        UpdateSkillData();
    }

    protected new void Start()
    {
        base.Start();
        GameManager.Instance.player.OnLevelUp += UpdateSkillData;
        animator.SetFloat("FairysWings multiplier", SpeedMultiplier);
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

        duration = SkillData.GetDuration();
        speedUpRate = SkillData.GetSpeedUpRate();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill1", true);
        //animator.SetInteger("Skill", 1);
        animator.SetTrigger("FairysWings");
    }

    override public void Activate(int dependedEffectIdx)
    {
        if (!bSkill)
        {
            StartCoroutine(SpeedUp(dependedEffectIdx));
        }
    }

    IEnumerator SpeedUp(int effectIdx)
    {
        bSkill = true;
        duration = 5.0f;
        nDeadEnemy = 0;

        base.PlayEffects(effectIdx);

        buffManager.AddBuff(speedUpRate, Ability.Buff.MoveSpeed_Buff, 5.0f);

        yield return new WaitForSeconds(duration);

        for (int i = 0; i < nDeadEnemy; ++i)
        {
            yield return new WaitForSeconds(1.0f);
        }

        base.StopEffects(effectIdx);

        bSkill = false;
    }

    public void AddDeadEnemy()
    {
        if (bSkill)
            ++nDeadEnemy;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill1", false);
        animator.SetBool("OnSkill", false);
    }
}
