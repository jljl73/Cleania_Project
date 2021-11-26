using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillR : PlayerSkill
{
    public AbilityStatus abilityStatus;
    public float skillScale = 1.0f;

    Collider attackArea;

    new void Start()
    {
        attackArea = GetComponent<Collider>();
        //initialNavAgentR = navMeshAgent.radius;
        base.Start();
        animator.SetFloat("Dehydration multiplier", SpeedMultiplier);
    }

    public override bool AnimationActivate()
    {
        // animator.SetInteger("Skill", 6);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillR", true);
        animator.SetTrigger("Dehydration");

        return true;
    }

    public override void Activate()
    {
        attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkillR", false);
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().abilityStatus.AttackedBy(abilityStatus, skillScale);

            if (other.GetComponent<Enemy>().abilityStatus.HP == 0)
                other.GetComponent<Enemy>().Die();
        }
    }

}