using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public StateMachine stateMachine;
    public GameObject enemySpawner;

    [Header("Need Drag")]
    public AbilityStatus abilityStatus;
    public EnemySkillManager skillManager;
    public EnemyMove enemyMove;

    public delegate void DelegateVoid();
    public event DelegateVoid OnDead;

    NavMeshAgent navMeshAgent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        OnDead += Die;
    }

    private void Update()
    {
        if (abilityStatus.HP == 0)
        {
            Stunned(false, 0);
            OnDead();
        }
    }

    public void Stunned(bool isStunned, float stunnedTime)
    {
        if (isStunned)
        {
            StartCoroutine("StunnedFor", stunnedTime);
        }
        else
        {
            animator.speed = 1;
            navMeshAgent.enabled = true;
        }
    }

    IEnumerator StunnedFor(float time)
    {
        animator.speed = 0;
        navMeshAgent.enabled = false;
        yield return new WaitForSeconds(time);
        animator.speed = 1;
        navMeshAgent.enabled = true;
    }

    public void Die()
    {
        if (stateMachine.CompareState(StateMachine.enumState.Dead)) return;

        navMeshAgent.enabled = false;
        GetComponent<Collider>().enabled = false;
        stateMachine.Transition(StateMachine.enumState.Dead);
        animator.SetTrigger("Die");
        Destroy(gameObject, 10.0f);
    }

    public void SetTarget(GameObject target)
    {
        enemyMove.SetTarget(target);
    }

    public void ReleaseTarget()
    {
        enemyMove.ReleaseTarget();
    }



    // Listener
    public void ActivateSkill(int type)
    {
        skillManager.ActivateSkill(type);
    }

    public void DeactivateSkill(int type)
    {
        skillManager.DeactivateSkill(type);
    }

}
