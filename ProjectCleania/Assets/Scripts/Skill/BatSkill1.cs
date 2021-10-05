using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSkill1 : Skill
{
    public int damage = 10;
    public float bloodChance = 0.3f;
    public float bloodTime = 5.0f;

    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("Attack Bite", true);
        //animator.SetInteger("Skill", 1);
        stateMachine.Transition(StateMachine.enumState.Attacking);

        col.enabled = true;

        Invoke("AnimationDeactivate", 1.0f);
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
            Debug.Log("bat skill1 Hit");

            //if (Random.Range(0.0f, 1.0f) < 0.3f)
            {
                //other.GetComponent<BuffManager>().Blood(bloodTime);
            }

        }
    }

    void OffSkill()
    {
        col.enabled = false;
    }

    public override void AnimationDeactivate()
    {
        stateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("Attack Bite", false);
        OffSkill();
    }

}
