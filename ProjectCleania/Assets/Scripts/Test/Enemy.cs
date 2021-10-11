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

    void Awake()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
    }



    public void Die()
    {
        if (stateMachine.CompareState(StateMachine.enumState.Dead)) return;

        GetComponent<NavMeshAgent>().enabled = false;
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
