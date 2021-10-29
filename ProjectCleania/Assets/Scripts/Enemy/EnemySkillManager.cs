using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemySkillManager : BaseSkillManager
{
    public EnemySkillStorage skillStorage;
    public EnemyMove enemyMove;
    public Enemy myEnemy;
    public NavMeshAgent nav;

    protected override void Awake()
    {
        //if (skillStorage != null)
        //{
        //    skills = new Skill[skillStorage.InherentSkillList.Count];
        //    coolTimePassed = new float[skills.Length];
        //    skillAvailable = new bool[skills.Length];
        //    CoolTimePassedRatio = new float[skills.Length];
        //}
        //else
        //{
        //    base.Awake();
        //}
        base.Awake();

        if (nav == null)
            throw new System.Exception("EnemySkillManager doesnt have nav");

        if (enemyMove == null)
            throw new System.Exception("EnemySkillManager doesnt have enemyMove");

        // abilityStatus = player.abilityStatus;
        // myEnemy = GetComponent<Enemy>();
    }

    new void Start()
    {
        base.Start();

        SetDefaultSkillSetting();

        for (int i = 0; i < skills.Length; i++)
        {
            coolTimePassed[i] = 1f;
            skillAvailable[i] = true;
        }


        myEnemy.OnDead += DeactivateAllSkill;
        myEnemy.OnStunned += Stunned;
    }

    private new void Update()
    {
        base.Update();
        animator.SetFloat("Speed", nav.velocity.sqrMagnitude);
    }

    protected override void SetDefaultSkillSetting()
    {
        if (skillStorage == null) return;

        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = skillStorage.GetInherentSkill(i);
        }

        base.SetDefaultSkillSetting();
    }

    protected override void SkillEventConnect()
    {
        throw new System.NotImplementedException();
    }

    #region
    // public EnemyStateMachine stateMachine;
    // private AbilityStatus playerAbilityStatus;

    // public Skill[] skills = new Skill[6];
    // Skill curSkill;
    // bool isCouroutineRunning;

    //public void PlaySkill(int index)
    //{
    //    skills[index].AnimationActivate();
    //}

    //public void ActivateSkill(int type)
    //{
    //    skills[type].Activate();
    //}

    //public void DeactivateSkill(int type)
    //{
    //    skills[type].Deactivate();
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


    //bool isSkillAvailable(int skillIndex)
    //{
    //    if (stateMachine.State == EnemyStateMachine.enumState.Idle ||
    //        stateMachine.State == EnemyStateMachine.enumState.Chasing)
    //        return true;
    //    else
    //        return false;
    //}
    #endregion
}
