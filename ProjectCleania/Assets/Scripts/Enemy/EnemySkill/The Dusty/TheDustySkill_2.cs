using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDustySkill_2 : Skill
{
    public GameObject projectile;

    public override void Activate()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }

    public override bool AnimationActivate()
    {
        animator.SetTrigger("Projectile");

        return true;
    }

    public override void Deactivate()
    {
        animator.SetFloat("Probability", Random.Range(0, 1.0f));
    }

}
