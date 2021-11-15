using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillL : PlayerSkill
{
    //public GameObject player;
    public PlayerMovement playerMovement;
    Collider attackArea;

    new void Start()
    {
        attackArea = GetComponent<Collider>();
        //initialNavAgentR = navMeshAgent.radius;
        base.Start();
        animator.SetFloat("Dusting multiplier", SpeedMultiplier);
    }

    // Update is called once per frame

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillC", true);
        animator.SetTrigger("Dusting");
        // animator.SetInteger("Skill", 5);

        return true;
    }

    public override void Activate()
    {
        attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkillC", false);
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("L Hit");

        }
    }

}
