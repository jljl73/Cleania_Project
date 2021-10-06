using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheDustySkill_1 : Skill
{
    Collider attackCollider;
    public UnityEvent unityEvent;

    void Start()
    {
        attackCollider = GetComponent<Collider>();
    }

    public override void Activate()
    {
        attackCollider.enabled = true;
    }

    public override void AnimationActivate()
    {
        animator.SetBool("RightSlash", true);
    }

    public override void AnimationDeactivate()
    {
        animator.SetBool("RightSlash", false);
        attackCollider.enabled = false;
    }

}
