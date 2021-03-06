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
    public EnemyMovement enemyMove;
    public EnemyStateMachine enemyStateMachine;
    public EnemyChase enemyChase;

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

        if (enemyChase == null)
            throw new System.Exception("Enemy doesnt have enemyChase");
    }

    //private void OnEnable()
    //{
    //    //StartCoroutine("InvincibleFor", 2f);
    //    Revive();
    //}

    //private void OnDisable()
    //{
    //    CancelInvoke();
    //    ReturnToObjectPool();
    //}

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
            case EnemyStateMachine.MonsterType.WildInti:
                ObjectPool.ReturnObject(ObjectPool.enumPoolObject.WildInti, this.gameObject);
                break;
            case EnemyStateMachine.MonsterType.HighDusty:
                ObjectPool.ReturnObject(ObjectPool.enumPoolObject.HighDusty, this.gameObject);
                break;
            case EnemyStateMachine.MonsterType.SummonerDusty:
                ObjectPool.ReturnObject(ObjectPool.enumPoolObject.SummonerDusty, this.gameObject);
                break;
            case EnemyStateMachine.MonsterType.Dusty:
                ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Dusty, this.gameObject);
                break;
            case EnemyStateMachine.MonsterType.TheDusty:
                ObjectPool.ReturnObject(ObjectPool.enumPoolObject.TheDusty, this.gameObject);
                break;
            default:
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

        // enemyChase off
        enemyChase.enabled = false;

        // enemyMove off
        enemyMove.enabled = false;

        // ??????????????? Off
        navMeshAgent.enabled = false;

        // ????????? ??????
        ActivateColliders(false);

        // ?????? ???????????? ??????
        enemyStateMachine.Transition(EnemyStateMachine.enumState.Dead);

        // ?????? ??????????????? ??????
        animator.SetTrigger("Die");

        // 3??? ?????? ?????? ??????
        Invoke("DeactivateSkin", 3.0f);

        // 10??? ?????? ??????
        // Invoke("DeactivateDelay", 10.0f);
        Destroy(gameObject, 10.0f);
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    public void Revive()
    {
        abilityStatus.FullHP();

        // enemyChase off
        enemyChase.enabled = true;

        // enemyMove On
        enemyMove.enabled = true;

        // ??????????????? On
        navMeshAgent.enabled = true;

        // ????????? ??????
        ActivateColliders(true);

        // ?????? ???????????? ??????
        enemyStateMachine.ResetState();

        // ?????? ??????????????? ??????
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
        // ?????? ??????
        skinnedMeshRenderer.enabled = true;
    }

    void DeactivateSkin()
    {
        // ?????? ??????
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
                return "?????????";
            case 5002:
                return "?????? ??????";
            case 5003:
                return "?????? ?????????";
            case 5004:
                return "????????? ?????????";
            case 6001:
                return "(??????)?????????";
            case 6002:
                return "(??????)?????? ??????";
            case 6003:
                return "(??????)?????? ?????????";
            case 6004:
                return "(??????)????????? ?????????";
            case 7001:
                return "??? ?????????";
        }
        return "";
    }

}
