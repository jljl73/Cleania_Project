using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkillManager : MonoBehaviour, IStunned
{
    public AbilityStatus abilityStatus;
    public Animator animator;

    public SkillStorage skillStorage;

    protected Dictionary<int, Skill> skillDict = new Dictionary<int, Skill>();
    protected Dictionary<int, float> coolTimePassedDict = new Dictionary<int, float>();
    protected Dictionary<int, bool> skillAvailableDict = new Dictionary<int, bool>();
    protected Dictionary<int, float> CoolTimePassedRatioDict = new Dictionary<int, float>();

    public float GetCoolTimePassedRatio(int id) { return CoolTimePassedRatioDict[id]; }

    protected virtual void Awake()
    {
        if (abilityStatus == null)
            throw new System.Exception("BaseSkillManager doesnt have abilityStatus");

        skillStorage = GetComponentInChildren<SkillStorage>();
        if (skillStorage == null)
            throw new System.Exception("PlayerSkillManager doesnt have skillStorage");
    }

    protected void Start()
    {
        UploadNormalSkill();
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
            skillAvailableDict.Add(id, true);
            coolTimePassedDict.Add(id, 0);
            CoolTimePassedRatioDict.Add(id, 1);
        }

        SetDefaultSkillSetting();
    }

    protected void updateCoolTime()
    {
        float cooldownSpeed = abilityStatus[Ability.Stat.SkillCooldown];

        #region
        //for (int i = 0; i < coolTimePassed.Length; i++)
        //{
        //    // 쿨타임 업데이트
        //    if (skills[i] == null)
        //    {
        //        print("coolTimePassed.Length: " + coolTimePassed.Length);
        //        print("Skill " + i + " is null!");
        //    }
        //    coolTimePassed[i] += Time.deltaTime;
        //    if (skills[i].GetCoolTime() < 0.01f)
        //        CoolTimePassedRatio[i] = 1f;
        //    else
        //        CoolTimePassedRatio[i] = coolTimePassed[i] / (skills[i].GetCoolTime() * cooldownSpeed);

        //    // 업데이트가 됬으면 스킬 가능 설정
        //    if (CoolTimePassedRatio[i] >= 1f)
        //    {
        //        CoolTimePassedRatio[i] = 1f;
        //        skillAvailable[i] = true;
        //    }
        //}
        #endregion

        foreach (int id in skillDict.Keys)
        {
            if (skillAvailableDict[id])
                continue;

            // 쿨타임 업데이트
            if (skillDict[id] == null)
            {
                print("coolTimePassedDict.Count: " + coolTimePassedDict.Count);
                print("Skill " + id + " is null!");
            }
            coolTimePassedDict[id] += Time.deltaTime;
            if (skillDict[id].GetCoolTime() < 0.01f)
                CoolTimePassedRatioDict[id] = 1f;
            else
                CoolTimePassedRatioDict[id] = coolTimePassedDict[id] / (skillDict[id].GetCoolTime() * cooldownSpeed);

            // 업데이트가 됬으면 스킬 가능 설정
            if (CoolTimePassedRatioDict[id] >= 1f)
            {
                CoolTimePassedRatioDict[id] = 1f;
                skillAvailableDict[id] = true;
            }
        }
    }

    protected abstract void SkillEventConnect();

    public virtual bool PlaySkill(int id)
    {
        if (!isSkillAvailable()) return false;
        if (!skillAvailableDict[id]) return false;
        skillDict[id].AnimationActivate();
        ResetSkill(id);

        return true;
    }

    public virtual void ActivateSkill(int id)
    {
        skillDict[id].Activate();
    }

    public virtual void DeactivateSkill(int id)
    {
        skillDict[id].Deactivate();
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

        // n번 스킬 칸에 있는 스킬의 N번째 스킬 이팩트를 써라
        if (skillEffectIndexSet != null)
            skillDict[skillEffectIndexSet.GetSkillID()].PlayEffects(skillEffectIndexSet.GetEffectID());
        else
        {
            for (int i = 0; i < skillDict[myEvent.intParameter].effectController.Count; i++)
            {
                skillDict[myEvent.intParameter].PlayEffects(i);
            }
        }
    }

    public virtual void DeactivateSkillEffect(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        // n번 스킬 칸에 있는 스킬의 N번째 스킬 이팩트를 써라
        if (skillEffectIndexSet != null)
            skillDict[skillEffectIndexSet.GetSkillID()].StopEffects(skillEffectIndexSet.GetEffectID());
        else
        {
            for (int i = 0; i < skillDict[myEvent.intParameter].effectController.Count; i++)
            {
                skillDict[myEvent.intParameter].StopEffects(i);
            }
        }
    }

    protected void initializeSkillSetting(int id)
    {
        coolTimePassedDict[id] = 1f;
        skillAvailableDict[id] = true;
    }

    void InitializeAllSkillSetting()
    {
        foreach (int id in skillDict.Keys)
        {
            initializeSkillSetting(id);
        }
    }

    public bool isSkillAvailable()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Run")) && !animator.IsInTransition(0))
            return true;
        else
            return false;
    }

    protected void ResetSkill(int skillID)
    {
        //coolTimePassed[index] = 0f;
        //skillAvailable[index] = false;
        coolTimePassedDict[skillID] = 0f;
        skillAvailableDict[skillID] = false;
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

    public void Stunned(bool isStunned, float stunnedTime)
    {
        if (isStunned)
        {
            StartCoroutine("StunnedFor", stunnedTime);
        }
        else
        {
            animator.speed = 1;
        }
    }

    IEnumerator IStunned.StunnedFor(float time)
    {
        animator.speed = 0;
        yield return new WaitForSeconds(time);
        animator.speed = 1;
    }
}
