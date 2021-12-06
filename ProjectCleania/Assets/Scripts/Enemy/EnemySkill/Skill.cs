using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    // 이벤트
    public UnityEvent OnPlaySkill;                      // 스킬 시작 시(AnimationActivate) 발생
    public UnityEvent[] OnSkillActivateEvents;          // 스킬 내 ActivateSkill에서 발생
    public UnityEvent[] OnSkillDeactivateEvents;        // 스킬 내 DeactivateSkill에서 발생
    public UnityEvent<bool, int> OnEnemyTriggerZone;    // 적이 스킬 시전 가능 범위 내에 있으면 시전

    protected float passedCoolTime;
    protected bool isCoolTimePassed = true;

    protected virtual void Update()
    {
        if (isCoolTimePassed)
            return;

        passedCoolTime += Time.deltaTime;
        if (passedCoolTime >= CoolTime)
            isCoolTimePassed = true;
    }

    public void InitializeOnSkillActivateEvents(int count)
    {
        OnSkillActivateEvents = new UnityEvent[count];
        for (int i = 0; i < count; i++)
            OnSkillActivateEvents[i] = new UnityEvent();
    }
    public void InitializeOnSkillDeactivateEvents(int count)
    {
        OnSkillDeactivateEvents = new UnityEvent[count];
        for (int i = 0; i < count; i++)
            OnSkillDeactivateEvents[i] = new UnityEvent();
    }


    // 컴포넌트
    public Animator animator;
    public AbilityStatus OwnerAbilityStatus;

    // 패시브 스킬이면 장착 되는 순간 스킬 발생
    protected bool isPassiveSkill = false;
    public virtual bool IsPassiveSkill { get { return isPassiveSkill; } }

    // 스킬 정보
    protected int id;
    public virtual int ID { get { return id; } protected set { id = value; } }

    protected string SkillName;
    public virtual string GetSkillName() { return SkillName; }
    [TextArea]
    protected string SkillDetails;
    public virtual string GetSkillDetails() { return SkillDetails; }

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

    protected Vector3 triggerPosition = Vector3.zero;
    public virtual Vector3 GetTriggerPosition() { return triggerPosition; }

    protected float triggerRange = 1.0f;
    public virtual float GetTriggerRange() { return triggerRange; }

    protected void Awake() {}
    protected void Start() {}

    public virtual void Activate()
    {
        if (OnSkillActivateEvents.Length != 0 && OnSkillActivateEvents[0] != null)
            OnSkillActivateEvents[0].Invoke();
    }
    public virtual void Activate(int idx = 0)
    {
        if (OnSkillActivateEvents.Length != 0 && OnSkillActivateEvents[idx] != null)
            OnSkillActivateEvents[idx].Invoke();
    }

    // 실행 가능한지 확인
    public virtual bool IsAvailable()
    {
        return true;
    }

    // 실행 가능한지 확인하지만, IsAvailable과 다르게 쿨타임 업데이트 여부 설정 가능, 스킬 실행전 체크됨 ex)PlayerController에서 실행
    public virtual bool AnimationActivate()
    {
        if (OnPlaySkill != null)
            OnPlaySkill.Invoke();

        return true;
    }

    public virtual void Deactivate()
    {
        if (OnSkillDeactivateEvents.Length != 0 && OnSkillDeactivateEvents[0] != null)
            OnSkillDeactivateEvents[0].Invoke();
    }

    public virtual void Deactivate(int idx = 0)
    {
        if (OnSkillDeactivateEvents.Length != 0 && OnSkillDeactivateEvents[idx] != null)
            OnSkillDeactivateEvents[idx].Invoke();
    }

    public virtual void StopSkill()
    {
        Deactivate();
        for (int j = 0; j < effectController.Count; j++)
        {
            StopEffects(j);
        }
        DeactivateSound(-1);
    }

    public List<SkillEffectController> effectController;

    public virtual void ActivateSound(int index) {}

    // index = -1 == 모두 종료
    public virtual void DeactivateSound(int index) {}

    public virtual void PlayEffects() {}

    public virtual void StopEffects() {}

    public void PlayEffects(int effectIdx)
    {
        effectController[effectIdx].PlaySkillEffect();
    }

    public void StopEffects(int effectIdx)
    {
        effectController[effectIdx].StopSKillEffect();
    }
}
