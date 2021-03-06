using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkillManager : MonoBehaviour, IStunned
{
    public AbilityStatus abilityStatus;
    public Animator animator;

    public SkillStorage skillStorage;

    protected Dictionary<int, Skill> skillDict = new Dictionary<int, Skill>();                      // 모든 스킬
    protected Dictionary<int, Skill> availableSkillDict = new Dictionary<int, Skill>();             // 현재 사용 가능 스킬
    protected Dictionary<int, Skill> needCoolTimePassedSkillDict = new Dictionary<int, Skill>();    // 쿨타임 업데이트 중인 스킬
    protected Dictionary<int, float> coolTimePassedDict = new Dictionary<int, float>();             // ID의 쿨타임 저장
    protected Dictionary<int, float> CoolTimePassedRatioDict = new Dictionary<int, float>();        // UI에 쿨타임 표시용

    public float GetCoolTimePassedRatio(int id) { return CoolTimePassedRatioDict[id]; }

    protected virtual void Awake()
    {
        if (abilityStatus == null)
            throw new System.Exception("BaseSkillManager doesnt have abilityStatus");

        skillStorage = GetComponentInChildren<SkillStorage>();
        if (skillStorage == null)
            throw new System.Exception("BaseSkillManager doesnt have skillStorage");

    }

    protected void Start()
    {
        // 일반 스킬 업로드
        UploadNormalSkill();

        // 업로드된 스킬로 쿨타임 & 사용가능 스킬 자료구조 및 데이터 초괴화
        UpdateOtherDictBySkillDict();
        InitializeAllSkillSetting();
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

    protected void UploadNormalSkill()
    {
        skillStorage.CopyNormalSkillDictTo(skillDict);
    }

    protected void UpdateOtherDictBySkillDict()
    {
        foreach (int id in skillDict.Keys)
        {
            if (!availableSkillDict.ContainsKey(id))
                availableSkillDict.Add(id, skillDict[id]);
            if (!coolTimePassedDict.ContainsKey(id))
                coolTimePassedDict.Add(id, 0);
            if (!CoolTimePassedRatioDict.ContainsKey(id))
                CoolTimePassedRatioDict.Add(id, 1);
        }

        SetDefaultSkillSetting();
    }

    protected void updateCoolTime()
    {
        float cooldownSpeed = abilityStatus[Ability.Stat.SkillCooldown];

        foreach (int id in skillDict.Keys)
        {
            if (!needCoolTimePassedSkillDict.ContainsKey(id))
                continue;

            coolTimePassedDict[id] += Time.deltaTime;
            if (needCoolTimePassedSkillDict[id].GetCoolTime() < 0.01f)
                CoolTimePassedRatioDict[id] = 1.1f;
            else
                CoolTimePassedRatioDict[id] = coolTimePassedDict[id] / (needCoolTimePassedSkillDict[id].GetCoolTime() * cooldownSpeed);

            // 업데이트가 됬으면 스킬 가능 설정
            if (CoolTimePassedRatioDict[id] >= 1f)
            {
                CoolTimePassedRatioDict[id] = 1f;

                availableSkillDict.Add(id, needCoolTimePassedSkillDict[id]);
                needCoolTimePassedSkillDict.Remove(id);
            }
        }
    }

    protected abstract void SkillEventConnect();

    public virtual bool PlaySkill(int id)
    {
        if (!IsSkillAvailable()) return false;
        if (!IsSpecificSkillAvailable(id)) return false;
        skillDict[id].AnimationActivate();
        ResetSkill(id);

        return true;
    }

    public virtual void AnimationDeactivate()
    {
    }

    public virtual void ActivateSkill(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        if (skillEffectIndexSet != null)
            skillDict[skillEffectIndexSet.GetSkillID()].Activate(skillEffectIndexSet.GetEffectID());
        else
            skillDict[myEvent.intParameter].Activate();
    }

    public virtual void DeactivateSkill(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        if (skillEffectIndexSet != null)
            skillDict[skillEffectIndexSet.GetSkillID()].Deactivate(skillEffectIndexSet.GetEffectID());
        else
            skillDict[myEvent.intParameter].Deactivate();
    }

    public void DeactivateAllSkill()
    {
        foreach (int id in skillDict.Keys)
        {
            skillDict[id].Deactivate();
            for (int j = 0; j < skillDict[id].effectController.Count; j++)
            {
                skillDict[id].StopEffects(j);
            }
        }
    }

    public virtual void ActivateSkillEffect(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        if (skillEffectIndexSet != null)
            skillDict[skillEffectIndexSet.GetSkillID()].PlayEffects(skillEffectIndexSet.GetEffectID());
        else
        {
            // 이펙트가 시전자에게 종속되지 않은 경우
            if (skillDict[myEvent.intParameter].effectController.Count == 0)
                skillDict[myEvent.intParameter].PlayEffects();
            else
            {
                // 시전자에게 종속된 경우
                for (int i = 0; i < skillDict[myEvent.intParameter].effectController.Count; i++)
                {
                    skillDict[myEvent.intParameter].PlayEffects(i);
                }
            }
        }
    }

    public virtual void DeactivateSkillEffect(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        if (skillEffectIndexSet != null)
            skillDict[skillEffectIndexSet.GetSkillID()].StopEffects(skillEffectIndexSet.GetEffectID());
        else
        {
            if (skillDict[myEvent.intParameter].effectController.Count == 0)
                skillDict[myEvent.intParameter].StopEffects();
            else
            {
                for (int i = 0; i < skillDict[myEvent.intParameter].effectController.Count; i++)
                {
                    skillDict[myEvent.intParameter].StopEffects(i);
                }
            }
        }
    }

    public virtual void ActivateSound(int id)
    {
        skillDict[id].ActivateSound(0);

    }

    public virtual void DeactivateSound(int id)
    {
        skillDict[id].DeactivateSound(0);
    }

    public virtual void ActivateSound(AnimationEvent myEvent)
    {
        SkillSoundIndexSO skillSoundIndexSet = myEvent.objectReferenceParameter as SkillSoundIndexSO;

        if (skillSoundIndexSet != null)
            skillDict[skillSoundIndexSet.GetSkillID()].ActivateSound(skillSoundIndexSet.GetSoundIndex());
    }

    public virtual void DeactivateSound(AnimationEvent myEvent)
    {
        SkillSoundIndexSO skillSoundIndexSet = myEvent.objectReferenceParameter as SkillSoundIndexSO;

        if (skillSoundIndexSet != null)
            skillDict[skillSoundIndexSet.GetSkillID()].DeactivateSound(skillSoundIndexSet.GetSoundIndex());
    }

    protected void initializeSkillSetting(int id)
    {
        coolTimePassedDict[id] = 1f;
    }

    void InitializeAllSkillSetting()
    {
        foreach (int id in skillDict.Keys)
        {
            initializeSkillSetting(id);
        }
    }

    protected virtual bool IsSkillAvailable()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Run")) && !animator.IsInTransition(0))
            return true;
        else
            return false;
    }

    public virtual bool IsSpecificSkillAvailable(int id)
    {
        return availableSkillDict.ContainsKey(id) && skillDict[id].IsAvailable();
    }

    protected void ResetSkill(int skillID)
    {
        coolTimePassedDict[skillID] = 0f;

        needCoolTimePassedSkillDict.Add(skillID, availableSkillDict[skillID]);
        availableSkillDict.Remove(skillID);
    }

    protected virtual void SetDefaultSkillSetting()
    {
        foreach (int id in skillDict.Keys)
        {
            if (skillDict[id] == null)
                throw new System.Exception("SetDefaultSkillSetting (skills[i] == null");

            skillDict[id].OwnerAbilityStatus = abilityStatus;
            skillDict[id].animator = animator;
        }
    }

    public void StopSkill(int id)
    {
        skillDict[id].StopSkill();
    }

    public void Stunned(bool isStunned, float stunnedTime)
    {
        if (isStunned)
        {
            StartCoroutine(StunnedFor(stunnedTime));
        }
        else
        {
            animator.SetBool("Stun", false);
        }
    }

    public IEnumerator StunnedFor(float time)
    {
        DeactivateAllSkill();
        animator.SetBool("Stun", true);
        yield return new WaitForSeconds(time);
        animator.SetBool("Stun", false);
    }
}
