using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillR : Skill
{
    public Animator animator;
    public PlayerMovement playerMovement;
    public StateMachine playerStateMachine;
    Collider attackArea;

    void Start()
    {
        attackArea = GetComponent<Collider>();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetInteger("Skill", 6);
        playerStateMachine.Transition(StateMachine.enumState.MoveAttack);

    }

    public override void Activate()
    {
        attackArea.enabled = true;
    }

    public override void AnimationDeactivate()
    {
        playerStateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Debug.Log("R Hit");
    }
 
}