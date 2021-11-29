using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillTrigger : MonoBehaviour
{
    //public Skill skill;
    public Animator animator;
    protected EnemyStateMachine stateMachine;
    protected EnemySkillManager enemySkillManager;

    protected virtual void Awake()
    {
        enemySkillManager = GetComponent<EnemySkillManager>();

        stateMachine = GetComponent<EnemyStateMachine>();
        if (stateMachine == null)
            throw new System.Exception("TheDustySkillTrigger doesnt have stateMachine");
    }

    protected virtual void Update()
    {
        if (stateMachine.CompareState(EnemyStateMachine.enumState.Dead))
            return;

        if (enemySkillManager.GetSkillRunWaitingListCount() == 0)
            return;

        enemySkillManager.PlaySkillRunWaitingListSkill();

    }

    //protected bool IsSkillAvailable()
    //{
    //    if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) && !animator.IsInTransition(0))
    //        return true;
    //    else
    //        return false;
    //}

    // WaitForSeconds waitForSeconds = new WaitForSeconds(3.0f);
    // bool isCouroutineRunning = false;

    //void OnTriggerEnter(Collider other)
    //{
    //    if (!other.CompareTag("Player")) return;

    //    animator.SetBool("OnSkill", true);
    //    print("Do spin skill!");
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (!other.CompareTag("Player")) return;

    //    animator.SetBool("OnSkill", false);
    //    print("Stop skill!");
    //}

}
