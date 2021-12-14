using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemySkillManager : BaseSkillManager
{
    //public EnemySkillStorage skillStorage;
    public EnemyMovement enemyMove;
    public Enemy myEnemy;
    public NavMeshAgent nav;

    EnemyStateMachine enemyStateMachine;
    EnemySkillStorage enemySkillStorage;

    Dictionary<int, bool> selectedSpecialSkillID = new Dictionary<int, bool>();

    [SerializeField]
    protected List<int> skillRunWaitingList = new List<int>();

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
        //UploadSpecialSkill();

        // skillData로 부터 쿨타임 관련 Dictionary 추가 및 초기화 & 스킬 animator 설정
        UpdateOtherDictBySkillDict();

        // 스킬 내 이벤트 연결
        SkillEventConnect();

        // 초기화
        skillRunWaitingList.Clear();
    }

    new void Update()
    {
        base.Update();
        if (myEnemy.enemyMove.TargetObject?.GetComponent<AbilityStatus>().HP == 0)
        {
            foreach (int id in skillDict.Keys)
            {
                EnrollAvailableSkill(false, id);
            }
        }
    }

    public int GetSkillRunWaitingListCount()
    {
        return skillRunWaitingList.Count;
    }

    public void PlaySkillRunWaitingListSkill()
    {
        // 실행 됬으면 Pop Front
        if (PlaySkill(skillRunWaitingList[0]))
        {
            int tempId = skillRunWaitingList[0];
            skillRunWaitingList.RemoveAt(0);
            skillRunWaitingList.Add(tempId);
        }
    }

    void EnrollAvailableSkill(bool value ,int id)
    {
        if (value)
        {
            if (enemyMove.TargetObject == null)
                return;

            //// 쿨타임 됬는지 확인
            //if (!IsSpecificSkillAvailable(id))
            //    return;

            // 이미 갖고있는지 확인
            if (skillRunWaitingList.Contains(id))
                return;
            else
                skillRunWaitingList.Add(id);
        }
        else
        {
            if (skillRunWaitingList.Contains(id))
                skillRunWaitingList.Remove(id);
        }
    }

    public override bool PlaySkill(int skillID)
    {
        if (!IsSkillAvailable()) return false;
        if (!IsSpecificSkillAvailable(skillID)) return false;

        // 더스티 자폭 스킬은 상태전환x
        if (skillDict[skillID].AnimationActivate() && skillID != 2102)
            enemyStateMachine.Transition(StateMachine.enumState.UnmovableAttacking);

        ResetSkill(skillID);

        return true;
    }

    // Deprecated
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

    //void UploadSpecialSkill()
    //{
    //    for (int i = 0; i < enemySkillStorage.SpecialSkillCandidates.Count; i++)
    //    {
    //        skillDict.Add(enemySkillStorage.SpecialSkillCandidates[i].ID, enemySkillStorage.SpecialSkillCandidates[i]);
    //    }
    //}

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
        if (!skillDict.ContainsKey(id))
        {
            skillDict.Add(id, enemySkillStorage.GetSpeacialSkill(id));
        }
    }

    protected override void SkillEventConnect()
    {
        // 등록된 스킬이 EnrollAvailableSkill 함수에 접근 할 수 있게 이벤트 설정
        foreach (KeyValuePair<int, Skill> skillPair in skillDict)
        {
            skillPair.Value.OnEnemyTriggerZone.AddListener(EnrollAvailableSkill);
        }
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
