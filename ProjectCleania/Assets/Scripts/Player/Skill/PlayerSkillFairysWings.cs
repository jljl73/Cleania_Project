using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillFairysWings : PlayerSkill
{
    [SerializeField]
    PlayerSkillFairysWingsSO skillData;

    public Buffable buffManager;

    // "지속 시간"
    float duration = 0f;
    public float GetDuration() { return duration; }

    // "속도 상승률 (ex. 0.4 = 40% 증가)"
    float speedUpRate = 1.4f;
    public float GetSpeedUpRate() { return speedUpRate; }

   float HandsUpReadyMultiplier = 1.0f;
   float HandsUpAndDownMultiplier = 1.0f;
   float PostDelayMultiplier = 1.0f;

    bool bSkill = false;
    int nDeadEnemy = 0;

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        UpdateSkillData();
    }

    new void Start()
    {
        base.Start();
        GameManager.Instance.player.OnLevelUp.AddListener(UpdateSkillData);
        animator.SetFloat("FairysWings multiplier", SpeedMultiplier);

        animator.SetFloat("FairysWings_HandsUpReady multiplier", HandsUpReadyMultiplier);
        animator.SetFloat("FairysWings_HandsUpDown multiplier", HandsUpAndDownMultiplier);
        animator.SetFloat("FairysWings_PostDelay multiplier", PostDelayMultiplier);
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        HandsUpReadyMultiplier = skillData.GetHandsUpReadyMultiplier();
        HandsUpAndDownMultiplier = skillData.GetHandsUpAndDownMultiplier();
        PostDelayMultiplier = skillData.GetPostDelayMultiplier();

        duration = skillData.GetDuration();
        speedUpRate = skillData.GetSpeedUpRate();
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill1", true);
        //animator.SetInteger("Skill", 1);
        animator.SetTrigger("FairysWings");

        return true;
    }

    override public void Activate(int idx)
    {
        if (!bSkill)
        {
            StartCoroutine(SpeedUp(idx));
        }
    }

    IEnumerator SpeedUp(int effectIdx)
    {
        bSkill = true;
        nDeadEnemy = 0;

        base.PlayEffects(effectIdx);

        buffManager.AddBuff(speedUpRate, Ability.Buff.MoveSpeed_Buff, duration);

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

    public override void ActivateSound(int index)
    {
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.FairyWings);
    }
}
