using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySkillManager : MonoBehaviour
{
    public EnemyAnimationEvent enemyAnimationEvent;
    public StateMachine enemyStateMachine;
    WaitForSeconds waitForSeconds = new WaitForSeconds(3.0f);
    //private AbilityStatus playerAbilityStatus;

    public Skill[] skills = new Skill[6];
    Skill curSkill;
    bool isCouroutineRunning;

    
    Skill InputHandler()
    {
        if (enemyStateMachine.State == StateMachine.enumState.Attacked) return null;
        else if (Random.Range(0, 10) < 7) return skills[0];
        else return skills[1];
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        enemyStateMachine.Transition(StateMachine.enumState.Attacking);
        if (!isCouroutineRunning) StartCoroutine(ActivateSkill());
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        enemyStateMachine.Transition(StateMachine.enumState.Idle);
    }

    IEnumerator ActivateSkill()
    {
        isCouroutineRunning = true;
        while (enemyStateMachine.State == StateMachine.enumState.Attacking)
        {
            curSkill = InputHandler();
            curSkill.AnimationActivate();
            yield return waitForSeconds;
        }
        isCouroutineRunning = false;
    }


    public void ActivateSkill(int type)
    {
        skills[type].Activate();
    }

    public void AnimationDeactivate(int type)
    {
        skills[type].AnimationDeactivate();
        //playerStateMachine.Transition(StateMachine.enumState.Idle);
    }


    bool isSkillAvailable(int skillIndex)
    {
        if (enemyStateMachine.State == StateMachine.enumState.Idle || 
            enemyStateMachine.State == StateMachine.enumState.Chasing)
            return true;
        else
            return false;
    }

}
