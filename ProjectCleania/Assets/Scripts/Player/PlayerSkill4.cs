using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSkill4 : Skill
{
    public AbilityStatus abilityStatus;
    public float skillScale = 1.0f;

    public TestPlayerMove playerMovement;
    public float jumpDistance = 7f;

    Ray ray;
    RaycastHit hit;
    Collider attackArea;

    void Start()
    {   
        attackArea = GetComponent<Collider>();
        //initialNavAgentR = navMeshAgent.radius;
    }

    public override void AnimationActivate()
    {
        animator.SetInteger("Skill", 4);
        animator.SetBool("OnSkill", true);
        Physics.IgnoreLayerCollision(3, 6);
        playerMovement.JumpForward(jumpDistance);
    }

    override public void Activate()
    {
        attackArea.enabled = true;
    }
    
    public override void Deactivate()
    {
        Physics.IgnoreLayerCollision(3, 6, false);
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
