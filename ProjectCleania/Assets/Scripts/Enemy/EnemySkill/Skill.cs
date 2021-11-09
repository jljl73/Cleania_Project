using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    // public UnityEvent PlaySkillEvent;
    // public UnityAction OnAnimationActivate;

    public delegate void DelegateVoid();
    public event DelegateVoid OnPlaySkill;
    public event DelegateVoid OnSkillEnd;

    public Animator animator;
    public AbilityStatus OwnerAbilityStatus;

    protected bool isPassiveSkill = false;
    public virtual bool IsPassiveSkill { get { return isPassiveSkill; } }

    protected int id;
    public virtual int ID { get { return id; } protected set { id = value; } }

    protected string SkillName;
    public virtual string GetSkillName() { return SkillName; }
    [TextArea]
    protected string SkillDetails;
    public virtual string GetSkillDetails() { return SkillDetails; }

    // public bool isAttacking;
    protected float CoolTime;  // 추후 private 처리
    public virtual float GetCoolTime() { return CoolTime; }
    protected float CreatedHP = 0f;
    public virtual float GetCreatedHP() { return CreatedHP; }

    protected float CreatedMP = 0f;
    public virtual float GetCreatedMP() { return CreatedMP; }
    protected float ConsumMP = 0f;
    public virtual float GetConsumMP() { return ConsumMP; }

    protected float SpeedMultiplier = 1.0f;
    public virtual float GetSpeedMultiplier() { return SpeedMultiplier; }

    public virtual void Activate() { }
    public virtual void Activate(int dependedEffectIdx = 0) { }

    public virtual bool IsAvailable()
    {
        return true;
    }

    // Return true if i have to update cooltime
    public virtual bool AnimationActivate()
    {
        if (OnPlaySkill != null)
            OnPlaySkill();

        return true;
    }

    public virtual void Deactivate()
    {
        if (OnSkillEnd != null)
            OnSkillEnd();
    }

    public List<SkillEffectController> effectController;

    protected void Start()
    {
    }

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
