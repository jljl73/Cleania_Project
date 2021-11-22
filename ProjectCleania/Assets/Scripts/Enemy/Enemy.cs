using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    Animator animator;

    [Header("Need Drag")]
    public AbilityStatus abilityStatus;
    public EnemySkillManager skillManager;
    public EnemyMove enemyMove;
    public EnemyStateMachine enemyStateMachine;

    public delegate void DelegateVoid();
    public event DelegateVoid OnDead;

    public UnityAction<bool, float> OnStunned;

    NavMeshAgent navMeshAgent;
    SkinnedMeshRenderer skinnedMeshRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (abilityStatus == null)
            throw new System.Exception("Enemy doesnt have abilityStatus");

        if (skillManager == null)
            throw new System.Exception("Enemy doesnt have skillManager");

        if (enemyMove == null)
            throw new System.Exception("Enemy doesnt have enemyMove");

        if (enemyStateMachine == null)
            throw new System.Exception("Enemy doesnt have enemyStateMachine");
    }

    private void OnEnable()
    {
        //StartCoroutine("InvincibleFor", 2f);
        Revive();
    }

    private void OnDisable()
    {
        CancelInvoke();
        ReturnToObjectPool();
    }

    private void Start()
    {
        OnDead += Die;
        OnDead += skillManager.DeactivateAllSkill;

        OnStunned += enemyMove.Stunned;
        OnStunned += skillManager.Stunned;
    }

    private void Update()
    {
        if (abilityStatus.HP == 0 && !enemyStateMachine.CompareState(EnemyStateMachine.enumState.Dead))
        {
            // Stunned(false, 0);
            OnStunned(false, 0);
            OnDead();
        }
    }
    
    void ReturnToObjectPool()
    {
        switch (enemyStateMachine.GetMonsterType())
        {
            case EnemyStateMachine.MonsterType.HighDusty:
                ObjectPool.ReturnObject(ObjectPool.enumPoolObject.HighDusty, this.gameObject);
                break;
            case EnemyStateMachine.MonsterType.SummonerDusty:
                ObjectPool.ReturnObject(ObjectPool.enumPoolObject.SummonerDusty, this.gameObject);
                break;
            case EnemyStateMachine.MonsterType.Dusty:
                ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Dusty, this.gameObject);
                break;
            default:
                ObjectPool.ReturnObject(ObjectPool.enumPoolObject.WildInti, this.gameObject);
                break;
        }
    }

    IEnumerator InvincibleFor(float time)
    {
        ActivateColliders(false);
        yield return new WaitForSeconds(time);
        ActivateColliders(true);
    }


    public void Die()
    {
        if (enemyStateMachine.CompareState(EnemyStateMachine.enumState.Dead)) return;

        if (enemyStateMachine.CompareState(EnemyStateMachine.enumRank.Rare))
        {
            GameManager.Instance.uiManager.GetComponent<QuestManager>().Acheive(QuestNeed.TYPE.Monster, enemyStateMachine.ID);
            GameManager.Instance.uiManager.GetComponent<QuestManager>().Acheive(QuestNeed.TYPE.Monster, enemyStateMachine.ID - (int)EnemyStateMachine.enumRank.Rare);
        }
        else
            GameManager.Instance.uiManager.GetComponent<QuestManager>().Acheive(QuestNeed.TYPE.Monster, enemyStateMachine.ID);

        SetTarget(null);

        ExpManager.Acquire(100);
        // 네비게이션 Off
        navMeshAgent.enabled = false;

        // 충돌체 끄기
        ActivateColliders(false);

        // 상태 죽음으로 전환
        enemyStateMachine.Transition(EnemyStateMachine.enumState.Dead);

        // 죽음 애니메이션 발동
        animator.SetTrigger("Die");

        // 3초 후에 외형 끄기
        Invoke("DeactivateSkin", 3.0f);

        // 10초 후에 파괴
        Invoke("DeactivateDelay", 10.0f);
        // Destroy(gameObject, 10.0f);
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    public void Revive()
    {
        abilityStatus.FullHP();

        // 네비게이션 On
        navMeshAgent.enabled = true;

        // 충돌체 끄기
        ActivateColliders(true);

        // 상태 죽음으로 전환
        enemyStateMachine.ResetState();

        // 죽음 애니메이션 발동
        //animator.SetTrigger("Die");

        ActivateSkin();

        StartCoroutine("InvincibleFor", 2f);
    }

    void ActivateColliders(bool value = true)
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = value;
        }

        colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = value;
        }
    }

    void ActivateSkin()
    {
        // 외형 끄기
        skinnedMeshRenderer.enabled = true;
    }

    void DeactivateSkin()
    {
        // 외형 끄기
        skinnedMeshRenderer.enabled = false;
    }

    public void SetTarget(GameObject target)
    {
        if (target == null)
            navMeshAgent.enabled = false;
        else
            navMeshAgent.enabled = true;

        enemyMove.SetTarget(target);
    }

    public void ReleaseTarget()
    {
        enemyMove.ReleaseTarget();
    }

    public static string GetName(int ID)
    {
        switch(ID)
        {
            case 5001:
                return "더스티";
            case 5002:
                return "야생 인티";
            case 5003:
                return "상위 더스티";
            case 5004:
                return "소환사 더스티";
            case 6001:
                return "(희귀)더스티";
            case 6002:
                return "(희귀)야생 인티";
            case 6003:
                return "(희귀)상위 더스티";
            case 6004:
                return "(희귀)소환사 더스티";
            case 7001:
                return "더 더스티";
        }
        return "";
    }

}
