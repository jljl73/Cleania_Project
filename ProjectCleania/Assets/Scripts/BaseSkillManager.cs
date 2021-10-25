using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkillManager : MonoBehaviour
{
    // public StateMachine stateMachine;
    public AbilityStatus abilityStatus;
    public Animator animator;
    public Skill[] skills;              // �ν����Ϳ��� �Ҵ�
    // Skill skill;

    protected float[] coolTimePassed;
    protected bool[] skillAvailable;
    protected float[] CoolTimePassedRatio;
    public float GetCoolTimePassedRatio(int idx) { return CoolTimePassedRatio[idx]; }

    protected virtual void Awake()
    {
        // abilityStatus = GetComponent<AbilityStatus>();
        coolTimePassed = new float[skills.Length];
        skillAvailable = new bool[skills.Length];
        CoolTimePassedRatio = new float[skills.Length];
    }

    protected void Start()
    {
        // ���� �ʱ�ȭ
        for (int i = 0; i < skills.Length; i++)
        {
            initializeSkillSetting(i);
        }
    }

    protected void Update()
    {
        updateCoolTime();
    }

    #region
    //    Skill InputHandler()
    //    {

    //        // Ű����
    //        if (Input.GetKeyDown(KeyCode.Alpha1) && isSkillAvailable(0))
    //        {
    //            initializeSkillSetting(0);
    //            abilityStatus.ConsumeMP(skills[0].ConsumMP);
    //            return skills[0];
    //        }
    //        if (Input.GetKeyDown(KeyCode.Alpha2) && isSkillAvailable(1))
    //        {
    //            initializeSkillSetting(1);
    //            abilityStatus.ConsumeMP(skills[1].ConsumMP);
    //            return skills[1];
    //        }
    //        if (Input.GetKeyDown(KeyCode.Alpha3) && isSkillAvailable(2))
    //        {
    //            initializeSkillSetting(2);
    //            abilityStatus.ConsumeMP(skills[2].ConsumMP);
    //            return skills[2];
    //        }
    //        if (Input.GetKeyDown(KeyCode.Alpha4) && isSkillAvailable(3))
    //        {
    //            initializeSkillSetting(3);
    //            abilityStatus.ConsumeMP(skills[3].ConsumMP);
    //            return skills[3];
    //        }

    //        // ���콺
    //        if (Input.GetKeyDown(KeyCode.C) && isSkillAvailable(4))
    //        {
    //            initializeSkillSetting(4);
    //            abilityStatus.ConsumeMP(skills[4].ConsumMP);
    //            return skills[4];
    //        }
    //        if (Input.GetMouseButtonDown(1) && (isSkillAvailable(5) ||
    //            stateMachine.State == StateMachine.enumState.MoveAttack))
    //        {
    //#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
    //            if (!EventSystem.current.IsPointerOverGameObject())
    //            {
    //                initializeSkillSetting(5);
    //                abilityStatus.ConsumeMP(skills[5].ConsumMP);
    //                return skills[5];
    //            }
    //#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
    //            if (!EventSystem.current.IsPointerOverGameObject(0))
    //            {
    //                initializeSkillSetting(5);
    //                playerAbilityStatus.ConsumeMP(skills[5].ConsumMP);
    //                return skills[5];
    //            }
    //#endif
    //        }

    //        return null;
    //    }
    #endregion

    protected void updateCoolTime()
    {
        float cooldownSpeed = abilityStatus[Ability.Stat.SkillCooldown];

        for (int i = 0; i < coolTimePassed.Length; i++)
        {
            // ��Ÿ�� ������Ʈ
            if (skills[i] == null)
            {
                print("coolTimePassed.Length: " + coolTimePassed.Length);
                print("Skill " + i + " is null!");
            }
            coolTimePassed[i] += Time.deltaTime;
            if (skills[i].GetCoolTime() < 0.01f)
                CoolTimePassedRatio[i] = 1f;
            else
                CoolTimePassedRatio[i] = coolTimePassed[i] / (skills[i].GetCoolTime() * cooldownSpeed);

            // ������Ʈ�� ������ ��ų ���� ����
            if (CoolTimePassedRatio[i] >= 1f)
            {
                CoolTimePassedRatio[i] = 1f;
                skillAvailable[i] = true;
            }
        }
    }

    //public virtual void ForcePlaySkill(int index)
    //{
    //    // ��� �������� ��ų �� �� IDle�� ��ȯ
    //    DeactivateAllSkill();
    //    animator.SetBool("OnSkill", false);

    //    // ��ų ����
    //    skills[index].AnimationActivate();
    //    ResetSkill(index);
    //}

    public virtual void PlaySkill(int index)
    {
        print("0");
        if (!isSkillAvailable()) return;
        print("1");
        if (!skillAvailable[index]) return;
        print("2");
        skills[index].AnimationActivate();
        ResetSkill(index);
    }

    public virtual void ActivateSkill(int type)
    {
        skills[type].Activate();
    }

    public virtual void DeactivateSkill(int type)
    {
        skills[type].Deactivate();
    }

    protected void DeactivateAllSkill()
    {
        foreach (Skill skill in skills)
        {
            skill.Deactivate();
            for (int i = 0; i < skill.effectController.Count; i++)
            {
                skill.StopEffects(i);
            }
        }
    }

    public virtual void ActivateSkillEffect(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        // n�� ��ų ĭ�� �ִ� ��ų�� N��° ��ų ����Ʈ�� ���
        if (skillEffectIndexSet != null)
            skills[skillEffectIndexSet.GetSkillIndex()].PlayEffects(skillEffectIndexSet.GetEffectIndex());
        else
        {
            for (int i = 0; i < skills[myEvent.intParameter].effectController.Count; i++)
            {
                skills[myEvent.intParameter].PlayEffects(i);
            }
        }
    }

    public virtual void DeactivateSkillEffect(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        // n�� ��ų ĭ�� �ִ� ��ų�� N��° ��ų ����Ʈ�� ���
        if (skillEffectIndexSet != null)
            skills[skillEffectIndexSet.GetSkillIndex()].StopEffects(skillEffectIndexSet.GetEffectIndex());
        else
        {
            for (int i = 0; i < skills[myEvent.intParameter].effectController.Count; i++)
            {
                skills[myEvent.intParameter].StopEffects(i);
            }
        }
    }

    protected void initializeSkillSetting(int index)
    {
        coolTimePassed[index] = 0f;
        skillAvailable[index] = false;
    }

    public bool isSkillAvailable()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Run")) && !animator.IsInTransition(0))
            return true;
        else
            return false;
    }

    protected void ResetSkill(int index)
    {
        coolTimePassed[index] = 0f;
        skillAvailable[index] = false;
    }

    protected virtual void SetDefaultSkillSetting()
    {

    }
}
