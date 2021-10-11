using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillL : Skill
{
    //public GameObject player;
    public PlayerMovement playerMovement;
    Collider attackArea;

    void Start()
    {
        attackArea = GetComponent<Collider>();
    }

    // Update is called once per frame

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetInteger("Skill", 5);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("L Hit");

        }
    }

}
