using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSkillManager : BaseSkillManager
{
    Player player;
    Dictionary<string, int> skillSlotDependencyDict = new Dictionary<string, int>();
    #region
    //public StateMachine stateMachine;
    //AbilityStatus abilityStatus;

    //Skill[] skills = new Skill[6];
    //float[] coolTimePassed = new float[6];
    //bool[] skillAvailable = new bool[6];

    //float[] CoolTimePassedRatio = new float[6];

    //public float GetCoolTimePassedRatio(int idx) { return CoolTimePassedRatio[idx]; }
    #endregion

    SkillStorage skillStorage;

    public delegate void delegateEvent(int index);
    public event delegateEvent SkillEvent;

    new void Awake()
    {
        base.Awake();
        player = transform.parent.GetComponent<Player>();
        // abilityStatus = player.abilityStatus;

        SkillEvent += PlaySkill;
    }

    new void Start()
    {
        base.Start();
        skillStorage = transform.parent.GetComponentInChildren<SkillStorage>();

        SetskillSlotDependencyDict();

        SetDefaultSkillSetting();

        for (int i = 0; i < skills.Length; i++)
        {
            coolTimePassed[i] = 1f;
            skillAvailable[i] = true;
        }

        player.OnDead += DeactivateAllSkill;
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
        skillSlotDependencyDict.Add("1", 0);
        skillSlotDependencyDict.Add("2", 1);
        skillSlotDependencyDict.Add("3", 2);
        skillSlotDependencyDict.Add("4", 3);
        skillSlotDependencyDict.Add("C", 4);
        skillSlotDependencyDict.Add("R", 5);
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
    public override void PlaySkill(int index)
    {
        if (!skillAvailable[index]) return;

        if (!isSkillAvailable()) return;

        // MP�� ������ ���� �Ұ�
        if (!abilityStatus.ConsumeMP(skills[index].GetConsumMP()))
            return;

        if (index != 3 && index != 5) player.stateMachine.Transition(StateMachine.enumState.Attacking);

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
            if (skillEffectIndex.GetSkillIndex() == 3) player.stateMachine.Transition(StateMachine.enumState.Attacking);

            skills[skillEffectIndex.GetSkillIndex()].Activate(skillEffectIndex.GetEffectIndex());
        }
        else
        {
            if (myEvent.intParameter == 3) player.stateMachine.Transition(StateMachine.enumState.Attacking);

            skills[myEvent.intParameter].Activate();
        }

        // R��ų ������ ������ ����������, ��ų ������ڸ��� �ϴ°� �����Ƿ� Obsolete
        // abilityStatus.ConsumeMP(skills[index].ConsumMP);
    }

    public override void DeactivateSkill(int index)
    {
        player.stateMachine.Transition(StateMachine.enumState.Idle);
        skills[index].Deactivate();
    }


    // ---------------------------------------------------------------------------------------------- //
    //                                             New Code
    // ---------------------------------------------------------------------------------------------- //

    public void ChangeSkill(string skillSlot, PlayerSkill.SkillID skillNameEnum)
    {
        PlayerSkill playerSkill = skillStorage.GetSkill(skillNameEnum);

        if (playerSkill.GetSkillSlotDependency() != skillSlot)
            return;

        skills[skillSlotDependencyDict[skillSlot]] = playerSkill;
    }

    protected override void SetDefaultSkillSetting()
    {
        ChangeSkill("1", PlayerSkill.SkillID.FairysWings);
        ChangeSkill("2", PlayerSkill.SkillID.Sweeping);
        ChangeSkill("3", PlayerSkill.SkillID.CleaningWind);
        ChangeSkill("4", PlayerSkill.SkillID.RefreshingLeapForward);
        ChangeSkill("C", PlayerSkill.SkillID.Dusting);
        ChangeSkill("R", PlayerSkill.SkillID.Dehydration);
    }
}
