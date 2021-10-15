using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDusting : PlayerSkill
{
    //public GameObject player;
    public TestPlayerMove playerMovement;
    Collider attackArea;

    new void Start()
    {
        attackArea = GetComponent<Collider>();
        //initialNavAgentR = navMeshAgent.radius;
        base.Start();
        animator.SetFloat("Dusting multiplier", speedMultiplier);
    }

    // Update is called once per frame

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillC", true);
        animator.SetTrigger("Dusting");
        // animator.SetInteger("Skill", 5);
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
