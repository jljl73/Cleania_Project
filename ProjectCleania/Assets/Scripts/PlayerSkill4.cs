using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSkill4 : Skill
{
    public NavMeshAgent navMeshAgent;
    public PlayerMovement playerMovement;
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

        if (passAvailable)
            navMeshAgent.radius = smallNaveAgentR;
        //playerMovement.MoveToPosition();
        //Invoke("OffSkill", 3.0f);
    }

    override public void Activate()
    {
        playerStateMachine.Transition(StateMachine.enumState.Attacking);

        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if(Physics.Raycast(ray, out hit))
        //{
        //    transform.position = new Vector3(
        //        hit.point.x,
        //        0.0f,
        //        hit.point.z);
        //}
        attackArea.enabled = true;
        Invoke("OffSkill", 3.0f);
    }

    void OffSkill()
    {
        attackArea.enabled = false;
    }

    public override void AnimationDeactivate()
    {
        playerStateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("OnSkill", false);

        if (passAvailable)
            navMeshAgent.radius = initialNavAgentR;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Debug.Log("4 Hit");
    }


}
