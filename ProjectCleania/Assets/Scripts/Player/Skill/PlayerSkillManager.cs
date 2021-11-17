using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerSkillManager : BaseSkillManager
{
    public StateMachine playerStateMachine;
    public PlayerMovement playerMove;
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

        playerMove = GetComponent<PlayerMovement>();
        if (playerMove == null)
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
        // 1106 = ������ ����
        skillStorage.GetNormalSkill(1106).OnPlaySkill += playerMove.LeapForwardSkillJumpForward;

        // 1199 = īŸ���ý�
        skillStorage.GetNormalSkill(1199).OnSkillEnd += playKatarsis;

        // 1198 = ������
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
        playerMove.enabled = false;
        PlayerMoveWithoutNav.enabled = true;
        objectCollider.enabled = false;
    }

    void EndRoll()
    {
        playerMove.enabled = true;
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
    //        // ��Ÿ�� ������Ʈ
    //        coolTimePassed[i] += Time.deltaTime;

    //        if (skills[i].GetCoolTime() < 0.01f)
    //            CoolTimePassedRatio[i] = 1f;
    //        else
    //            CoolTimePassedRatio[i] = coolTimePassed[i] / skills[i].GetCoolTime();

    //        // ������Ʈ�� ������ ��ų ���� ����
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
        if (!IsSkillAvailable()) return false;

        // MP�� ������ ���� �Ұ�
        if (!abilityStatus.ConsumeMP(skillDict[id].GetConsumMP()))
            return false;

        // 1102 = Ż��(mouse R), 1106 = ������ ����(4��), 1199 = īŸ���ý�(F), 1198 = ������(Space), 1197 = ������ȯ(T), 1196 = ������(Q)
        if (id != 1106 && id != 1102 && id != 1199 && id != 1198 && id != 1196 && id != 1197) playerStateMachine.Transition(StateMachine.enumState.Attacking);

        if (playerMove.enabled)
            playerMove.ImmediateLookAtMouse();
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
        playerStateMachine.Transition(StateMachine.enumState.Idle);
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

    //    // n�� ��ų ĭ�� �ִ� ��ų�� N��° ��ų ����Ʈ�� ���
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

    //    // n�� ��ų ĭ�� �ִ� ��ų�� N��° ��ų ����Ʈ�� ���
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
            // 1106 = ������ ����(4�� ��ų) 
            if (skillEffectIndex.GetSkillID() == 1106) playerStateMachine.Transition(StateMachine.enumState.Attacking);

            skillDict[skillEffectIndex.GetSkillID()].Activate(skillEffectIndex.GetEffectID());
        }
        else
        {
            // 1106 = ������ ����(4�� ��ų)
            if (myEvent.intParameter == 1106) playerStateMachine.Transition(StateMachine.enumState.Attacking);

            skillDict[myEvent.intParameter].Activate();
        }

        // R��ų ������ ������ ����������, ��ų ������ڸ��� �ϴ°� �����Ƿ� Obsolete
        // abilityStatus.ConsumeMP(skills[index].ConsumMP);
    }

    public override void DeactivateSkill(int id)
    {
        //playerStateMachine.Transition(StateMachine.enumState.Idle);
        skillDict[id].Deactivate();
    }


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

    //    base�� �߰�����
    //    base.SetDefaultSkillSetting();
    //}
    #endregion
}
