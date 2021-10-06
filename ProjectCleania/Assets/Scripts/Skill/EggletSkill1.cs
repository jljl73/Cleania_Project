using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggletSkill1 : Skill
{
    public int damage = 10;

    Collider col;
    AbilityStatus myAbility;

    private void Start()
    {
        col = GetComponent<Collider>();
        myAbility = GetComponentInParent<AbilityStatus>();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("Attack BodyBlow", true);
        //animator.SetInteger("Skill", 1);
        stateMachine.Transition(StateMachine.enumState.Attacking);

        col.enabled = true;

        Invoke("AnimationDeactivate", 0.5f);
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
            Debug.Log("egglet skill1 Hit");

            GameManager.Instance.PlayerAbility.AttackedBy(myAbility, damage);
        }
    }

    void OffSkill()
    {
        col.enabled = false;
    }

    public override void AnimationDeactivate()
    {
        stateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("Attack BodyBlow", false);
        OffSkill();
    }

}
