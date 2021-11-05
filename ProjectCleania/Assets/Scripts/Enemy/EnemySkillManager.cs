using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemySkillManager : BaseSkillManager
{
    //public EnemySkillStorage skillStorage;
    public EnemyMove enemyMove;
    public Enemy myEnemy;
    public NavMeshAgent nav;

    EnemyStateMachine enemyStateMachine;
    EnemySkillStorage enemySkillStorage;

    Dictionary<int, bool> selectedSpecialSkillID = new Dictionary<int, bool>();

    protected override void Awake()
    {
        base.Awake();
        
        if (nav == null)
            throw new System.Exception("EnemySkillManager doesnt have nav");

        if (enemyMove == null)
            throw new System.Exception("EnemySkillManager doesnt have enemyMove");

        enemySkillStorage = skillStorage as EnemySkillStorage;
        if (enemySkillStorage == null)
            throw new System.Exception("EnemySkillManager's skillStorage is not EnemySkillStorage");

        enemyStateMachine = GetComponent<EnemyStateMachine>();
        if (enemyStateMachine == null)
            throw new System.Exception("EnemySkillManager's skillStorage is not enemyStateMachine");
    }

    new void Start()
    {
        // skillDict 추가
        UploadNormalSkill();
        UploadSpecialSkill();

        // skillData로 부터 쿨타임 관련 Dictionary 추가 및 초기화 & 스킬 animator 설정
        UpdateOtherDictBySkillDict();
    }

    new void Update()
    {
        base.Update();
        
    }

    public override bool PlaySkill(int skillID)
    {
        if (!IsSkillAvailable()) return false;
        if (!IsSpecificSkillAvailable(skillID)) return false;

        skillDict[skillID].AnimationActivate();
        ResetSkill(skillID);

        return true;
    }

    public bool PlayRandomSpecialSkill()
    {
        if (!IsSkillAvailable()) return false;

        int availableSpecialSkillCount = 0;
        int selectedSpecialSkillCount = selectedSpecialSkillID.Count;

        // 현재 실행가능한 선택된 특수 능력이 있는지 확인
        for (int i = 0; i < selectedSpecialSkillCount; i++)
        {
            KeyValuePair<int, bool> pair = selectedSpecialSkillID.ElementAt(i);
            if (availableSkillDict.ContainsKey(pair.Key))
            {
                availableSpecialSkillCount++;
                break;
            }
        }

        if (availableSpecialSkillCount == 0)
            return false;

        while (true)
        {
            int index = Random.Range(0, selectedSpecialSkillCount);
            KeyValuePair<int, bool> pair = selectedSpecialSkillID.ElementAt(index);

            if (!IsSpecificSkillAvailable(pair.Key)) continue;

            availableSkillDict[pair.Key].AnimationActivate();
            ResetSkill(pair.Key);
            break;
        }

        return true;
    }

    void UploadSpecialSkill()
    {
        for (int i = 0; i < enemySkillStorage.SpecialSkillCandidates.Count; i++)
        {
            skillDict.Add(enemySkillStorage.SpecialSkillCandidates[i].ID, enemySkillStorage.SpecialSkillCandidates[i]);
        }
    }

    public void MakeSpecialSkillAvailable(int id)
    {
        enemyStateMachine.Transition(EnemyStateMachine.enumRank.Rare);

        // 패시브 속성이면 적용, 추가X
        Skill skill = enemySkillStorage.GetSpecialSkillFromList(id);
        if (skill == null) return;

        if (skill.IsPassiveSkill)
        {
            skill.AnimationActivate();
            return;
        }

        selectedSpecialSkillID.Add(id, true);
    }

    protected override void SkillEventConnect()
    {
        return;
    }

    public int GetRandomSpecialSkillAvailableID()
    {
        int idx = Random.Range(0, enemySkillStorage.SpecialSkillCandidates.Count);
        return enemySkillStorage.SpecialSkillCandidates[idx].ID;
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
