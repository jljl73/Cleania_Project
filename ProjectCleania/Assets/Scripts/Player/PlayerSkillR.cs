using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillR : Skill
{
    public AbilityStatus abilityStatus;
    public float skillScale = 1.0f;

    Collider attackArea;

    void Start()
    {
        attackArea = GetComponent<Collider>();
    }

    public override void AnimationActivate()
    {
        animator.SetInteger("Skill", 6);
        animator.SetBool("OnSkill", true);
    }

    public override void Activate()
    {
        attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<Enemy>().abilityStatus.AttackedBy(abilityStatus, skillScale) == 0)
                other.GetComponent<Enemy>().Die();
        }
    }

}