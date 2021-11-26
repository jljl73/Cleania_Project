using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSkill4 : PlayerSkill
{
    public AbilityStatus abilityStatus;
    public float skillScale = 1.0f;

    // public TestPlayerMove playerMovement;
    public float jumpDistance = 7f;

    Ray ray;
    RaycastHit hit;
    Collider attackArea;

    new void Start()
    {   
        attackArea = GetComponent<Collider>();
        //initialNavAgentR = navMeshAgent.radius;
        base.Start();
        animator.SetFloat("RefreshingLeapForward multiplier", SpeedMultiplier);
    }

    public override bool AnimationActivate()
    {
        //animator.SetInteger("Skill", 4);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill4", true);
        animator.SetTrigger("RefreshingLeapForward");
        Physics.IgnoreLayerCollision(3, 6);
        // playerMovement.JumpForward(jumpDistance);
        // PlaySkillEvent.Invoke();
        //if (OnPlaySkill != null)
        //    OnPlaySkill();

        return true;
    }

    override public void Activate()
    {
        attackArea.enabled = true;
    }
    
    public override void Deactivate()
    {
        Physics.IgnoreLayerCollision(3, 6, false);
        animator.SetBool("OnSkill4", false);
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
