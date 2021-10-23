using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillTrigger : MonoBehaviour
{
    //public Skill skill;
    public Animator animator;
    protected EnemySkillManager enemySkillManager;

    protected void Awake()
    {
        enemySkillManager = GetComponent<EnemySkillManager>();
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
