using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerSkillManager : BaseSkillManager
{
    // Player player;

    public StateMachine playerStateMachine;
    public TestPlayerMove playerMove;
    public SkillStorage skillStorage;
    // public Buffable buffable;

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

        playerMove = GetComponent<TestPlayerMove>();
        if (playerMove == null)
            throw new System.Exception("PlayerSkillManager doesnt have playerMove");

        skillStorage = GetComponentInChildren<SkillStorage>();
        if (skillStorage == null)
            throw new System.Exception("PlayerSkillManager doesnt have skillStorage");
    }

    new void Start()
    {
        base.Start();

        SetskillSlotDependencyDict();

        SetDefaultSkillSetting();

        for (int i = 0; i < skills.Length; i++)
        {
            coolTimePassed[i] = 1f;
            skillAvailable[i] = true;
        }

        SkillEventConnect();
    }

    //void DeactivateAllSkill()
    //{
    //    foreach (Skill skill in skills)
    //    {
    //        skill.Deactivate();
    //        for (int i = 0; i < skill.effectController.Count; i++)
    //        {
    //            skill.StopEffects(i);
    //        }
    //    }
    //}

    void SkillEventConnect()
    {
        skillStorage.GetSkill(PlayerSkill.SkillID.RefreshingLeapForward).OnPlaySkill += playerMove.LeapForwardSkillJumpForward;
    }


    protected new bool isSkillAvailable()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run")) 
                && !animator.IsInTransition(0))
            return true;
        else
            return false;
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
    public override void PlaySkill(int index)
    {
        if (!skillAvailable[index]) return;

        if (!isSkillAvailable()) return;

        // MP가 없으면 실행 불가
        if (!abilityStatus.ConsumeMP(skills[index].GetConsumMP()))
            return;

        if (index != 3 && index != 5) playerStateMachine.Transition(StateMachine.enumState.Attacking);

        playerMove.ImmediateLookAtMouse();

        skills[index].AnimationActivate();
        ResetSkill(index);
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

    public void ActivateSkill(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndex = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        if (skillEffectIndex != null)
        {
            if (skillEffectIndex.GetSkillIndex() == 3) playerStateMachine.Transition(StateMachine.enumState.Attacking);

            skills[skillEffectIndex.GetSkillIndex()].Activate(skillEffectIndex.GetEffectIndex());
        }
        else
        {
            if (myEvent.intParameter == 3) playerStateMachine.Transition(StateMachine.enumState.Attacking);

            skills[myEvent.intParameter].Activate();
        }

        // R스킬 때문인 것으로 추정되지만, 스킬 사용하자마자 하는게 좋으므로 Obsolete
        // abilityStatus.ConsumeMP(skills[index].ConsumMP);
    }

    public override void DeactivateSkill(int index)
    {
        playerStateMachine.Transition(StateMachine.enumState.Idle);
        skills[index].Deactivate();
    }


    // ---------------------------------------------------------------------------------------------- //
    //                                             New Code
    // ---------------------------------------------------------------------------------------------- //

    public void ChangeSkill(KeyCode skillSlot, PlayerSkill.SkillID skillNameEnum)
    {
        PlayerSkill playerSkill = skillStorage.GetSkill(skillNameEnum);

        if (playerSkill.GetSkillSlotDependency() != skillSlot)
            return;

        skills[skillSlotDependencyDict[skillSlot]] = playerSkill;
        // skills[skillSlotDependencyDict[skillSlot]].OwnerAbilityStatus = abilityStatus;
        // skills[skillSlotDependencyDict[skillSlot]].
    }

    protected override void SetDefaultSkillSetting()
    {
        ChangeSkill(KeyCode.Alpha1, PlayerSkill.SkillID.FairysWings);
        ChangeSkill(KeyCode.Alpha2, PlayerSkill.SkillID.Sweeping);
        ChangeSkill(KeyCode.Alpha3, PlayerSkill.SkillID.CleaningWind);
        ChangeSkill(KeyCode.Alpha4, PlayerSkill.SkillID.RefreshingLeapForward);
        ChangeSkill(KeyCode.C, PlayerSkill.SkillID.Dusting);
        ChangeSkill(KeyCode.Mouse1, PlayerSkill.SkillID.Dehydration);

        base.SetDefaultSkillSetting();
    }
}
