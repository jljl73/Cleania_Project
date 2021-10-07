using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDustySkill1 : Skill
{
    public float DamageScale = 10;

    //Collider col
    AbilityStatus myAbility;
    public GameObject DustBall;

    private void Start()
    {
        //col = GetComponent<Collider>();
        myAbility = GetComponentInParent<AbilityStatus>();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("Attack ThrowDust", true);
        //animator.SetInteger("Skill", 1);
        stateMachine.Transition(StateMachine.enumState.Attacking);

        //col.enabled = true;
        Activate();

        Invoke("AnimationDeactivate", 0.5f);
    }

    override public void Activate()
    {
        //col.enabled = true;
        GameObject ball = Instantiate(DustBall, transform);
        ball.GetComponent<HighDusty_DustBall>().owner = gameObject;
        ball.GetComponent<HighDusty_DustBall>().DamageScale = DamageScale;
        ball.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up/2)*200.0f);
        //Invoke("OffSkill", 1.0f);
    }

    void OffSkill()
    {
        //col.enabled = false;
    }

    public override void AnimationDeactivate()
    {
        stateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("Attack ThrowDust", false);
        OffSkill();
    }

}
