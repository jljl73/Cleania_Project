using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    // �̺�Ʈ
    public UnityEvent OnPlaySkill;                      // ��ų ���� ��(AnimationActivate) �߻�
    public UnityEvent[] OnSkillActivateEvents;          // ��ų �� ActivateSkill���� �߻�
    public UnityEvent[] OnSkillDeactivateEvents;        // ��ų �� DeactivateSkill���� �߻�
    public UnityEvent<bool, int> OnEnemyTriggerZone;    // ���� ��ų ���� ���� ���� ���� ������ ����
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


    // ������Ʈ
    public Animator animator;
    public AbilityStatus OwnerAbilityStatus;

    // �нú� ��ų�̸� ���� �Ǵ� ���� ��ų �߻�
    protected bool isPassiveSkill = false;
    public virtual bool IsPassiveSkill { get { return isPassiveSkill; } }

    // ��ų ����
    protected int id;
    public virtual int ID { get { return id; } protected set { id = value; } }

    protected string SkillName;
    public virtual string GetSkillName() { return SkillName; }
    [TextArea]
    protected string SkillDetails;
    public virtual string GetSkillDetails() { return SkillDetails; }

    protected float CoolTime;  // ���� private ó��
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

    // ���� �������� Ȯ��
    public virtual bool IsAvailable()
    {
        return true;
    }

    // ���� �������� Ȯ��������, IsAvailable�� �ٸ��� ��Ÿ�� ������Ʈ ���� ���� ����
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

    public virtual void StopSkill() {}

    public List<SkillEffectController> effectController;

    public virtual void ActivateSound(int index) {}

    public virtual void DeactivateSound(int index)
    {
        GameManager.Instance.playerSoundPlayer.StopSound();
    }

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
