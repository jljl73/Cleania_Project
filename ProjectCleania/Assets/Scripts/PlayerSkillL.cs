using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillL : Skill
{
    public Animator animator;
    //public GameObject player;
    public PlayerMovement playerMovement;
    public StateMachine playerStateMachine;
    Collider attackArea;

    void Start()
    {
        attackArea = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        attackArea.enabled = true;
    }

    public override void AnimationActivate()
    {
        playerStateMachine.Transition(StateMachine.enumState.Attacking);
        animator.SetBool("OnSkill", true);

        animator.SetInteger("Skill", 5);
    }

    public void OffSkill()
    {
        attackArea.enabled = false;
    }

    public override void AnimationDeactivate()
    {
        playerStateMachine.Transition(StateMachine.enumState.Idle);
        animator.SetBool("OnSkill", false);
        OffSkill();
    }

    void GiveDamage(Collider other)
    {
        // 부딛힌 콜라이더에게 
        AbilityStatus hitObjStatus = other.GetComponent<AbilityStatus>();
        if (hitObjStatus == null)
        {
            Debug.Log("No AbilityStatus on hitObj");
            return;
        }
        // 적에게 데미지 입히기
        AbilityStatus parentStatus = GetComponentInParent<AbilityStatus>();
        if (parentStatus == null)
        {
            Debug.Log("No AbilityStatus on parent");
            return;
        }

        hitObjStatus.AttackedBy(parentStatus, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("L Hit");
            
            GiveDamage(other);
        }
    }
}
