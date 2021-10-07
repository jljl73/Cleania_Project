using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill3 : Skill
{
    public float DamageScale = 10;

    Collider col;
    AbilityStatus myAbility;

    private void Start()
    {
        col = GetComponent<Collider>();
        myAbility = GetComponentInParent<AbilityStatus>();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("Attack Spin", true);
        //animator.SetInteger("Skill", 1);
        stateMachine.Transition(StateMachine.enumState.Attacking);

        col.enabled = true;

        Invoke("AnimationDeactivate", 1f);
    }

    override public void Activate()
    {
        col.enabled = true;
        Invoke("OffSkill", 1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("reaper skill3 Hit");

            GameManager.Instance.PlayerAbility.AttackedBy(myAbility, DamageScale);
        }
    }

    void OffSkill()
    {
        col.enabled = false;
    }

    public override void AnimationDeactivate()
    {
        stateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("Attack Spin", false);
        OffSkill();
    }
}
