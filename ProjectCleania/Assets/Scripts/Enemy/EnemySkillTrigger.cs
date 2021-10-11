using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillTrigger : MonoBehaviour
{
    public Skill skill;
    public Animator animator;

    WaitForSeconds waitForSeconds = new WaitForSeconds(3.0f);
    bool isCouroutineRunning = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        animator.SetBool("OnSkill", true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        animator.SetBool("OnSkill", false);
    }

}
