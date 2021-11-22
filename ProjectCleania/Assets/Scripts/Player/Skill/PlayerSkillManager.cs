using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerSkillManager : BaseSkillManager
{
    public StateMachine playerStateMachine;
    public PlayerMovement playerMoveWithNav;
    public TestPlayerMove PlayerMoveWithoutNav;
    Collider objectCollider;

    Dictionary<KeyCode, int> skillSlotDependencyDict = new Dictionary<KeyCode, int>();
    #region
    //public StateMachine stateMachine;
    //AbilityStatus abilityStatus;

    //Skill[] skills = new Skill[6];
    //float[] coolTimePassed = new float[6];
    //bool[] skillAvailable = new bool[6];

    //float[] CoolTimePassedRatio = new float[6];

    //public float GetCoolTimePassedRatio(int idx) { return CoolTimePassedRatio[idx]; }
    #endregion


    new void Awake()
    {
        base.Awake();

        playerStateMachine = GetComponent<StateMachine>();
        if (playerStateMachine == null)
            throw new System.Exception("PlayerSkillManager doesnt have playerStateMachine");

        playerMoveWithNav = GetComponent<PlayerMovement>();
        if (playerMoveWithNav == null)
            throw new System.Exception("PlayerSkillManager doesnt have playerMove");

        PlayerMoveWithoutNav = GetComponent<TestPlayerMove>();
        if (PlayerMoveWithoutNav == null)
            throw new System.Exception("PlayerSkillManager doesnt have TestPlayerMove");

        objectCollider = GetComponent<Collider>();
        if (objectCollider == null)
            throw new System.Exception("PlayerSkillManager doesnt have Collider");
    }

    new void Start()
    {
        base.Start();

        SetskillSlotDependencyDict();

        SkillEventConnect();
    }

    protected override void SkillEventConnect()
    {
        // 1106 = 상쾌한 도약
        skillStorage.GetNormalSkill(1106).OnPlaySkill += PlayerMoveWithoutNav.LeapForwardSkillJumpForward;

        // 1199 = 카타르시스
        skillStorage.GetNormalSkill(1199).OnSkillEnd += playKatarsis;

        // 1198 = 구르기
        skillStorage.GetNormalSkill(1198).OnPlaySkill += playRoll;
        skillStorage.GetNormalSkill(1198).OnSkillEnd += EndRoll;
    }

    void playKatarsis()
    {
        if (skillDict[1199].AnimationActivate())
            ResetSkill(1199);
    }

    void playRoll()
    {
        playerMoveWithNav.enabled = false;
        PlayerMoveWithoutNav.enabled = true;
        objectCollider.enabled = false;
    }

    void EndRoll()
    {
        playerMoveWithNav.enabled = true;
        PlayerMoveWithoutNav.enabled = false;
        objectCollider.enabled = true;
    }

    protected new bool IsSkillAvailable()
    {
        //  || animator.GetCurrentAnimatorStateInfo(0).IsName("SkillR Dehydration")
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || 
             animator.GetCurrentAnimatorStateInfo(0).IsName("Run")) 
                && !animator.IsInTransition(0))
            return true;
        else
            return false;
    }
    
    void CheckCancelAttackTime()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Post delay")))
        {
            animator.SetTrigger("SkillCancel");
        }
    }

    void SetskillSlotDependencyDict()
    {
        skillSlotDependencyDict.Add(KeyCode.Alpha1, 0);
        skillSlotDependencyDict.Add(KeyCode.Alpha2, 1);
        skillSlotDependencyDict.Add(KeyCode.Alpha3, 2);
        skillSlotDependencyDict.Add(KeyCode.Alpha4, 3);
        skillSlotDependencyDict.Add(KeyCode.C, 4);
        skillSlotDependencyDict.Add(KeyCode.Mouse1, 5);
    }

    #region
    //void Update()
    //{
    //    UpdateCoolTime();
    //}

    //void UpdateCoolTime()
    //{
    //    for (int i = 0; i < coolTimePassed.Length; i++)
    //    {
    //        // 쿨타임 업데이트
    //        coolTimePassed[i] += Time.deltaTime;

    //        if (skills[i].GetCoolTime() < 0.01f)
    //            CoolTimePassedRatio[i] = 1f;
    //        else
    //            CoolTimePassedRatio[i] = coolTimePassed[i] / skills[i].GetCoolTime();

    //        // 업데이트가 됬으면 스킬 가능 설정
    //        if (CoolTimePassedRatio[i] >= 1f)
    //        {
    //            CoolTimePassedRatio[i] = 1f;
    //            skillAvailable[i] = true;
    //        }
    //    }
    //}

    //void ResetSkill(int index)
    //{
    //    coolTimePassed[index] = 0f;
    //    skillAvailable[index] = false;
    //}
    #endregion
    public override bool PlaySkill(int id)
    {
        if (!IsSpecificSkillAvailable(id)) return false;

        // 부활 스킬이 아니면, 스킬 쓸 수 있는 상태인지 확인
        if (id != 1190)
            if (!IsSkillAvailable()) return false;

        // MP가 없으면 실행 불가
        if (!abilityStatus.ConsumeMP(skillDict[id].GetConsumMP()))
            return false;

        // 1102 = 탈수(mouse R), 1106 = 상쾌한 도약(4번), 1199 = 카타르시스(F), 1198 = 구르기(Space), 1197 = 마을귀환(T), 1196 = 정제수(Q), 1190 = 부활(R)
        if (id != 1106 && id != 1102 && id != 1199 && id != 1198 && id != 1196 && id != 1197 && id != 1190) playerStateMachine.Transition(StateMachine.enumState.Attacking);

        if (playerMoveWithNav.enabled)
            playerMoveWithNav.ImmediateLookAtMouse();
        else
            PlayerMoveWithoutNav.ImmediateLookAtMouse();

        if (skillDict[id].AnimationActivate())
            ResetSkill(id);
        return true;
    }

    public void StopSkill(int id)
    {
        skillDict[id].StopSkill();
    }

    public override void AnimationDeactivate()
    {
        //playerStateMachine.Transition(StateMachine.enumState.Idle);
        playerStateMachine.ResetState();
    }

    #region
    //public void ActivateSkillEffect(int index)
    //{
    //    for (int i = 0; i < skills[index].effectController.Count; i++)
    //    {
    //        skills[index].PlayEffects(i);
    //    }
    //}

    //public void ActivateSkillEffect(AnimationEvent myEvent)
    //{
    //    SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

    //    // n번 스킬 칸에 있는 스킬의 N번째 스킬 이팩트를 써라
    //    if (skillEffectIndexSet != null)
    //        skills[skillEffectIndexSet.GetSkillIndex()].PlayEffects(skillEffectIndexSet.GetEffectIndex());
    //    else
    //    {
    //        for (int i = 0; i < skills[myEvent.intParameter].effectController.Count; i++)
    //        {
    //            skills[myEvent.intParameter].PlayEffects(i);
    //        }
    //    }
    //}

    //public void DeactivateSkillEffect(AnimationEvent myEvent)
    //{
    //    SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

    //    // n번 스킬 칸에 있는 스킬의 N번째 스킬 이팩트를 써라
    //    if (skillEffectIndexSet != null)
    //        skills[skillEffectIndexSet.GetSkillIndex()].StopEffects(skillEffectIndexSet.GetEffectIndex());
    //    else
    //    {
    //        for (int i = 0; i < skills[myEvent.intParameter].effectController.Count; i++)
    //        {
    //            skills[myEvent.intParameter].StopEffects(i);
    //        }
    //    }

    //}

    //public void DeactivateSkillEffect(int index)
    //{
    //    for (int i = 0; i < skills[index].effectController.Count; i++)
    //    {
    //        skills[index].StopEffects(i);
    //    }
    //}
    #endregion

    public override void ActivateSkill(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndex = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        if (skillEffectIndex != null)
        {
            // 1106 = 상쾌한 도약(4번 스킬) 
            if (skillEffectIndex.GetSkillID() == 1106) playerStateMachine.Transition(StateMachine.enumState.Attacking);

            skillDict[skillEffectIndex.GetSkillID()].Activate(skillEffectIndex.GetEffectID());
        }
        else
        {
            // 1106 = 상쾌한 도약(4번 스킬)
            if (myEvent.intParameter == 1106) playerStateMachine.Transition(StateMachine.enumState.Attacking);

            skillDict[myEvent.intParameter].Activate();
        }

        // R스킬 때문인 것으로 추정되지만, 스킬 사용하자마자 하는게 좋으므로 Obsolete
        // abilityStatus.ConsumeMP(skills[index].ConsumMP);
    }

    //public override void DeactivateSkill(int id)
    //{
    //    //playerStateMachine.Transition(StateMachine.enumState.Idle);
    //    skillDict[id].Deactivate();
    //}


    #region
    // ---------------------------------------------------------------------------------------------- //
    //                                             New Code
    // ---------------------------------------------------------------------------------------------- //

    //public void ChangeSkill(KeyCode skillSlot, PlayerSkill.SkillID skillNameEnum)
    //{
    //    PlayerSkill playerSkill = skillStorage.GetSkill(skillNameEnum);

    //    if (playerSkill.GetSkillSlotDependency() != skillSlot)
    //        return;

    //    skillDict[skillSlotDependencyDict[skillSlot]] = playerSkill;
    //    // skills[skillSlotDependencyDict[skillSlot]].OwnerAbilityStatus = abilityStatus;
    //    // skills[skillSlotDependencyDict[skillSlot]].
    //}

    //protected override void SetDefaultSkillSetting()
    //{
    //    ChangeSkill(KeyCode.Alpha1, PlayerSkill.SkillID.FairysWings);
    //    ChangeSkill(KeyCode.Alpha2, PlayerSkill.SkillID.Sweeping);
    //    ChangeSkill(KeyCode.Alpha3, PlayerSkill.SkillID.CleaningWind);
    //    ChangeSkill(KeyCode.Alpha4, PlayerSkill.SkillID.RefreshingLeapForward);
    //    ChangeSkill(KeyCode.C, PlayerSkill.SkillID.Dusting);
    //    ChangeSkill(KeyCode.Mouse1, PlayerSkill.SkillID.Dehydration);

    //    base에 추가했음
    //    base.SetDefaultSkillSetting();
    //}
    #endregion
}
