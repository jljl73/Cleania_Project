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
        // 1106 = ������ ����
        skillStorage.GetNormalSkill(1106).InitializeOnSkillActivateEvents(1);
        skillStorage.GetNormalSkill(1106).OnSkillActivateEvents[0].AddListener(TransitionToAttack);

        // 1199 = īŸ���ý�
        skillStorage.GetNormalSkill(1199).InitializeOnSkillActivateEvents(1);
        skillStorage.GetNormalSkill(1199).OnSkillActivateEvents[0].AddListener(playKatarsis);

        // 1198 = ������
        skillStorage.GetNormalSkill(1198).OnPlaySkill.AddListener(playRoll);

        skillStorage.GetNormalSkill(1198).InitializeOnSkillDeactivateEvents(1);
        skillStorage.GetNormalSkill(1198).OnSkillDeactivateEvents[0].AddListener(EndRoll);

        // ���ڸ� ��Ȱ
        skillStorage.GetNormalSkill(1194).OnPlaySkill.AddListener(ShowDiePanel);

        skillStorage.GetNormalSkill(1194).InitializeOnSkillDeactivateEvents(1);
        skillStorage.GetNormalSkill(1194).OnSkillDeactivateEvents[0].AddListener(abilityStatus.FullHP);

        // ���� ��Ȱ
        skillStorage.GetNormalSkill(1195).OnPlaySkill.AddListener(CloseDiePanel);
        skillStorage.GetNormalSkill(1195).OnPlaySkill.AddListener(abilityStatus.FullHP);
    }

    void TransitionToAttack() => playerStateMachine.Transition(StateMachine.enumState.UnmovableAttacking);

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

    void ShowDiePanel()
    {
        GameManager.Instance.uiManager.ShowDiePanel(true);
    }

    void CloseDiePanel()
    {
        GameManager.Instance.uiManager.ShowDiePanel(false);
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
    
    // ��Ÿ ĵ��, �̱���
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
    // ���� ����: id ��ų�� ��� �������� �Ǵ� �� ���.
    // ����: MP ���, StateMachine ����, ��ų ���� ������ ����
    // Return: ��ų ��� ���� ����
    public override bool PlaySkill(int id)
    {
        if (!IsSpecificSkillAvailable(id)) return false;

        //  �Ʒ� If���� IsSpecificSkillAvailable�� �ű� �� ���� ������?
        
        // ��Ȱ ��ų�� �ƴϸ�, ��ų �� �� �ִ� �������� Ȯ��
        if (id != 1194 && id != 1195)
            if (!IsSkillAvailable()) return false;

        // MP�� ������ ���� �Ұ�
        if (!abilityStatus.ConsumeMP(skillDict[id].GetConsumMP()))
            return false;

        // 1102 = Ż��(mouse R), 1106 = ������ ����(4��), 1198 = ������(Space), 1197 = ������ȯ(T), 1196 = ������(Q), 1194, 1195 = ��Ȱ
        if (id != 1106 && id != 1102 && id != 1198 && id != 1196 && id != 1197 && id != 1194 && id != 1195) playerStateMachine.Transition(StateMachine.enumState.UnmovableAttacking);

        if (playerMoveWithNav.enabled)
            playerMoveWithNav.ImmediateLookAtMouse();
        //else
        //    PlayerMoveWithoutNav.ImmediateLookAtMouse();

        // ��ų ���
        if (skillDict[id].AnimationActivate())
        {
            // ��ų ���� ����
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

    //public override void ActivateSkill(AnimationEvent myEvent)
    //{
    //    SkillEffectIndexSO skillEffectIndex = myEvent.objectReferenceParameter as SkillEffectIndexSO;

    //    if (skillEffectIndex != null)
    //    {
    //        //// 1106 = ������ ����(4�� ��ų) 
    //        //if (skillEffectIndex.GetSkillID() == 1106) playerStateMachine.Transition(StateMachine.enumState.Attacking);

    //        skillDict[skillEffectIndex.GetSkillID()].Activate(skillEffectIndex.GetEffectID());
    //    }
    //    else
    //    {
    //        //// 1106 = ������ ����(4�� ��ų)
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

    //    base�� �߰�����
    //    base.SetDefaultSkillSetting();
    //}
    #endregion
}
