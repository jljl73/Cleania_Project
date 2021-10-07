using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSkill4 : Skill
{
    public TestPlayerMove playerMovement;
    public float jumpDistance = 7f;

    private float initialNavAgentR;
    private float smallNaveAgentR = 0.01f;

    public bool passAvailable = false;

    Ray ray;
    RaycastHit hit;
    Collider attackArea;

    void Start()
    {   
        attackArea = GetComponent<Collider>();
        //initialNavAgentR = navMeshAgent.radius;
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetInteger("Skill", 4);
        playerMovement.JumpForward(jumpDistance);

    }

    override public void Activate()
    {
        stateMachine.Transition(StateMachine.enumState.Attacking);
        attackArea.enabled = true;
        Invoke("OffSkill", 3.0f);
    }

    void OffSkill()
    {
        attackArea.enabled = false;
    }

    public override void AnimationDeactivate()
    {
        stateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("OnSkill", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Debug.Log("4 Hit");
    }


}
