using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public Animator animator;

    protected string SkillName;
    public string GetSkillName() { return SkillName; }
    [TextArea]
    protected string SkillDetails;
    public string GetSkillDetails() { return SkillDetails; }

    // public bool isAttacking;
    protected float CoolTime;  // 추후 private 처리
    public float GetCoolTime() { return CoolTime; }
    protected float CreatedMP = 0f;
    public float GetCreatedMP() { return CreatedMP; }
    protected float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    protected float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    // Start is called before the first frame update
    protected void Start()
    {
        // isAttacking = false;
    }

    public virtual void Activate() { }
    public virtual void Activate(int dependedEffectIdx = 0) { }

    public abstract void AnimationActivate();

    public abstract void Deactivate();

    public List<SkillEffectController> effectController;

    public void PlayEffects(int effectIdx)
    {
        effectController[effectIdx].PlaySkillEffect();
        //foreach (SkillEffectController skillEffect in effectController)
        //{
        //    skillEffect.PlaySkillEffect();
        //}
    }

    public void StopEffects(int effectIdx)
    {
        effectController[effectIdx].StopSKillEffect();
        //foreach (SkillEffectController skillEffect in effectController)
        //{
        //    skillEffect.StopSKillEffect();
        //}
    }
}
