using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill4 : Skill
{
    public StateMachine playerStateMachine;
    public Animator animator;
    Ray ray;
    RaycastHit hit;
    Collider attackArea;

    void Start()
    {   
        attackArea = GetComponent<Collider>();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetInteger("Skill", 4);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Debug.Log("4 Hit");
    }


}
