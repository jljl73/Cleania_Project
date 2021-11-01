using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemySkillManager : BaseSkillManager
{
    //public EnemySkillStorage skillStorage;
    public EnemyMove enemyMove;
    public Enemy myEnemy;
    public NavMeshAgent nav;

    protected override void Awake()
    {
        base.Awake();
        
        if (nav == null)
            throw new System.Exception("EnemySkillManager doesnt have nav");

        if (enemyMove == null)
            throw new System.Exception("EnemySkillManager doesnt have enemyMove");
    }

    new void Start()
    {
        // skillDict 추가
        UploadNormalSkill();
        UploadSpecialSkill();

        // skillData로 부터 쿨타임 관련 Dictionary 추가 및 초기화 & 스킬 animator 설정
        UpdateOtherDictBySkillDict();

        myEnemy.OnDead += DeactivateAllSkill;
        myEnemy.OnStunned += Stunned;
    }

    new void Update()
    {
        base.Update();
        
    }

    public override bool PlaySkill(int skillID)
    {
        if (!isSkillAvailable()) return false;
        if (!skillAvailableDict[skillID]) return false;
        
        skillDict[skillID].AnimationActivate();
        ResetSkill(skillID);

        return true;
    }

    void UploadSpecialSkill()
    {
        EnemySkillStorage enemySkillStorage = skillStorage as EnemySkillStorage;
        if (enemySkillStorage == null)
            throw new System.Exception("EnemySkillManager's skillStorage is not EnemySkillStorage");

        for (int i = 0; i < enemySkillStorage.SpecialSkillCandidates.Count; i++)
        {
            skillDict.Add(enemySkillStorage.SpecialSkillCandidates[i].ID, enemySkillStorage.SpecialSkillCandidates[i]);
        }
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
