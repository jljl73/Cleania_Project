using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkillManager : MonoBehaviour
{
    // public StateMachine stateMachine;
    public AbilityStatus abilityStatus;
    public Animator animator;
    protected Skill[] skills;              
    public int SkillSlotCount = 1;
    // Skill skill;

    protected float[] coolTimePassed;
    protected bool[] skillAvailable;
    protected float[] CoolTimePassedRatio;
    public float GetCoolTimePassedRatio(int idx) { return CoolTimePassedRatio[idx]; }

    protected virtual void Awake()
    {
        // abilityStatus = GetComponent<AbilityStatus>();
        skills = new Skill[SkillSlotCount];
        coolTimePassed = new float[SkillSlotCount];
        skillAvailable = new bool[SkillSlotCount];
        CoolTimePassedRatio = new float[SkillSlotCount];
    }

    protected void Start()
    {
        // 셋팅 초기화
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

    //        // 키보드
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

    //        // 마우스
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
            // 쿨타임 업데이트
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

            // 업데이트가 됬으면 스킬 가능 설정
            if (CoolTimePassedRatio[i] >= 1f)
            {
                CoolTimePassedRatio[i] = 1f;
                skillAvailable[i] = true;
            }
        }
    }

    //public virtual void ForcePlaySkill(int index)
    //{
    //    // 모든 진행중인 스킬 끈 후 IDle로 전환
    //    DeactivateAllSkill();
    //    animator.SetBool("OnSkill", false);

    //    // 스킬 실행
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

    public void DeactivateAllSkill()
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

        // n번 스킬 칸에 있는 스킬의 N번째 스킬 이팩트를 써라
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

        // n번 스킬 칸에 있는 스킬의 N번째 스킬 이팩트를 써라
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
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] == null)
                throw new System.Exception("SetDefaultSkillSetting (skills[i] == null");

            skills[i].OwnerAbilityStatus = abilityStatus;
            skills[i].animator = animator;
        }
    }
}
