using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationEvent : MonoBehaviour
{
    bool isDead = false;
    public EnemySkillManager skillManager;
    StateMachine state;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        state = GetComponent<StateMachine>();
    }

    
    //public override void ActivateSkill(int type)
    //{
    //    skillManager.ActivateSkill(type);
    //}

    //public override void DeactivateSkill(int type)
    //{
    //    skillManager.DeactivateSkill(type);
    //}

    public void Die()
    {
        if (isDead) return;

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;
        isDead = true;
        state.Transition(StateMachine.enumState.Dead);
        animator.SetTrigger("Die");
        Destroy(gameObject, 10.0f);
    }
}
