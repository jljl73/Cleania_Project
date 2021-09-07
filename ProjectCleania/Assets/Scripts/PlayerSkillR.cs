using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillR : Skill
{
    public Animator animator;

    void Start()
    {
        
    }

    public override void Activate()
    {
        animator.SetInteger("Skill", 0);
    }

    public override void AnimationActivate()
    {
        animator.SetInteger("Skill", 5);
    }

}