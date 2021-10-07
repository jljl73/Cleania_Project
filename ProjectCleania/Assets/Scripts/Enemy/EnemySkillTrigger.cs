using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillTrigger : MonoBehaviour
{
    public Skill skill;
    public StateMachine enemyStateMachine;

    WaitForSeconds waitForSeconds = new WaitForSeconds(3.0f);
    bool isCouroutineRunning = false;

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
            skill.AnimationActivate();
            yield return waitForSeconds;
        }
        isCouroutineRunning = false;
    }
}
