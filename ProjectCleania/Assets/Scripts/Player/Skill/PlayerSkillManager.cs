using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerSkillManager : BaseSkillManager
{
    public StateMachine playerStateMachine;
    public PlayerMovement playerMoveWithNav;
    public Player player;
    //public TestPlayerMove PlayerMoveWithoutNav;
    Collider objectCollider;

    //Dictionary<KeyCode, int> skillSlotDependencyDict = new Dictionary<KeyCode, int>();
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

        player = GetComponent<Player>();
        if (player == null)
            throw new System.Exception("PlayerSkillManager doesnt have player");

        playerStateMachine = GetComponent<StateMachine>();
        if (playerStateMachine == null)
            throw new System.Exception("PlayerSkillManager doesnt have playerStateMachine");

        playerMoveWithNav = GetComponent<PlayerMovement>();
        if (playerMoveWithNav == null)
            throw new System.Exception("PlayerSkillManager doesnt have playerMove");

        //PlayerMoveWithoutNav = GetComponent<TestPlayerMove>();
        //if (PlayerMoveWithoutNav == null)
        //    throw new System.Exception("PlayerSkillManager doesnt have TestPlayerMove");

        objectCollider = GetComponent<Collider>();
        if (objectCollider == null)
            throw new System.Exception("PlayerSkillManager doesnt have Collider");
    }

    new void Start()
    {
        base.Start();

        SkillEventConnect();
    }

    protected override void SkillEventConnect()
    {
        // 1106 = 상쾌한 도약
        skillStorage.GetNormalSkill(1106).OnPlaySkill += TransitionToAttack;
        //skillStorage.GetNormalSkill(1106).OnPlaySkill += PlayerMoveWithoutNav.LeapForwardSkillJumpForward;

        // 1199 = 카타르시스
        skillStorage.GetNormalSkill(1199).OnSkillDeactivate += playKatarsis;

        // 1198 = 구르기
        skillStorage.GetNormalSkill(1198).OnPlaySkill += playRoll;
        skillStorage.GetNormalSkill(1198).OnSkillDeactivate += EndRoll;

        // 부활
        skillStorage.GetNormalSkill(1194).OnPlaySkill += player.Revive;
        skillStorage.GetNormalSkill(1194).OnSkillDeactivate += abilityStatus.FullHP;

        skillStorage.GetNormalSkill(1195).OnPlaySkill += player.VillageRevive;
        skillStorage.GetNormalSkill(1195).OnPlaySkill += abilityStatus.FullHP;
    }

    void TransitionToAttack() => playerStateMachine.Transition(StateMachine.enumState.Attacking);

    void playKatarsis()
    {
        if (skillDict[1199].AnimationActivate())
            ResetSkill(1199);
    }

    void playRoll()
    {
        playerMoveWithNav.SpeedUp(10);
    }

    void EndRoll()
    {
        playerMoveWithNav.SpeedUp(6.8f);
    }

    protected override bool IsSkillAvailable()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || 
             animator.GetCurrentAnimatorStateInfo(0).IsName("Run")) 
                && !animator.IsInTransition(0))
            return true;
        else
            return false;
    }
    
    // 평타 캔슬, 미구현
    void CheckCancelAttackTime()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Post delay")))
        {
            animator.SetTrigger("SkillCancel");
        }
    }
    #region
    //void SetskillSlotDependencyDict()
    //{
    //    skillSlotDependencyDict.Add(KeyCode.Alpha1, 0);
    //    skillSlotDependencyDict.Add(KeyCode.Alpha2, 1);
    //    skillSlotDependencyDict.Add(KeyCode.Alpha3, 2);
    //    skillSlotDependencyDict.Add(KeyCode.Alpha4, 3);
    //    skillSlotDependencyDict.Add(KeyCode.C, 4);
    //    skillSlotDependencyDict.Add(KeyCode.Mouse1, 5);
    //}


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
    // 한줄 설명: id 스킬이 사용 가능한지 판단 후 사용.
    // 설명: MP 사용, StateMachine 변경, 스킬 시전 움직임 변경
    // Return: 스킬 사용 성공 여부
    public override bool PlaySkill(int id)
    {
        if (!IsSpecificSkillAvailable(id)) return false;

        //  아래 If문을 IsSpecificSkillAvailable로 옮길 수 있지 않을까?
        
        // 부활 스킬이 아니면, 스킬 쓸 수 있는 상태인지 확인
        if (id != 1194 && id != 1195)
            if (!IsSkillAvailable()) return false;

        // MP가 없으면 실행 불가
        if (!abilityStatus.ConsumeMP(skillDict[id].GetConsumMP()))
            return false;

        // 1102 = 탈수(mouse R), 1106 = 상쾌한 도약(4번), 1198 = 구르기(Space), 1197 = 마을귀환(T), 1196 = 정제수(Q), 1194, 1195 = 부활
        if (id != 1106 && id != 1102 && id != 1198 && id != 1196 && id != 1197 && id != 1194 && id != 1195) playerStateMachine.Transition(StateMachine.enumState.Attacking);

        if (playerMoveWithNav.enabled)
            playerMoveWithNav.ImmediateLookAtMouse();
        //else
        //    PlayerMoveWithoutNav.ImmediateLookAtMouse();

        // 스킬 사용
        if (skillDict[id].AnimationActivate())
        {
            // 스킬 정보 리셋
            ResetSkill(id);
        }
        return true;
    }

    public override void AnimationDeactivate()
    {
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

    //public override void ActivateSkill(AnimationEvent myEvent)
    //{
    //    SkillEffectIndexSO skillEffectIndex = myEvent.objectReferenceParameter as SkillEffectIndexSO;

    //    if (skillEffectIndex != null)
    //    {
    //        //// 1106 = 상쾌한 도약(4번 스킬) 
    //        //if (skillEffectIndex.GetSkillID() == 1106) playerStateMachine.Transition(StateMachine.enumState.Attacking);

    //        skillDict[skillEffectIndex.GetSkillID()].Activate(skillEffectIndex.GetEffectID());
    //    }
    //    else
    //    {
    //        //// 1106 = 상쾌한 도약(4번 스킬)
    //        //if (myEvent.intParameter == 1106) playerStateMachine.Transition(StateMachine.enumState.Attacking);

    //        skillDict[myEvent.intParameter].Activate();
    //    }
    //}

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
