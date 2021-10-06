using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillL : Skill
{
    //public GameObject player;
    public PlayerMovement playerMovement;
    Collider attackArea;

    void Start()
    {
        attackArea = GetComponent<Collider>();
    }

    // Update is called once per frame

    public override void Activate()
    {
        attackArea.enabled = true;
    }

    public override void AnimationActivate()
    {
        stateMachine.Transition(StateMachine.enumState.Attacking);
        animator.SetBool("OnSkill", true);

        animator.SetInteger("Skill", 5);
    }

    public void OffSkill()
    {
        attackArea.enabled = false;
    }

    public override void AnimationDeactivate()
    {
        stateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("OnSkill", false);
        OffSkill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("L Hit");

            EnemyAI enemyAI = other.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.Die();
            }
        }
    }

}
