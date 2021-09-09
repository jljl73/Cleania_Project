using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill3 : Skill
{
    public StateMachine playerStateMachine;
    public Animator animator;
    public GameObject hurricanePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        playerStateMachine.Transition(StateMachine.enumState.Attacking);
        animator.SetInteger("Skill", 3);
    }

    public override void AnimationDeactivate()
    {
        playerStateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("OnSkill", false);
    }

    override public void Activate()
    {

        Quaternion left = transform.rotation;
        Quaternion right = transform.rotation;

        left *= Quaternion.Euler(0, 30.0f, 0.0f);
        right *= Quaternion.Euler(0, -30.0f, 0.0f);

        Instantiate(hurricanePrefabs, transform.position, left);
        Instantiate(hurricanePrefabs, transform.position, transform.rotation);
        Instantiate(hurricanePrefabs, transform.position, right);
    }

}
