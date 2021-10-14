using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill3 : PlayerSkill
{
    public GameObject hurricanePrefabs;
    public AbilityStatus abilityStatus;
    GameObject newProjectile;

    protected new void Start()
    {
        base.Start();
        animator.SetFloat("CleaningWind multiplier", SpeedMultiplier);
    }

    public override void AnimationActivate()
    {
        //animator.SetInteger("Skill", 3);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill3", true);
        animator.SetTrigger("CleaningWind");
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill3", false);
        animator.SetBool("OnSkill", false);
    }

    override public void Activate()
    {

        Quaternion left = transform.rotation;
        Quaternion right = transform.rotation;

        left *= Quaternion.Euler(0, 30.0f, 0.0f);
        right *= Quaternion.Euler(0, -30.0f, 0.0f);

        //newProjectile = Instantiate(hurricanePrefabs, transform.position, left);
        //newProjectile.GetComponent<Projectile>().abilityStatus = abilityStatus;

        //newProjectile = Instantiate(hurricanePrefabs, transform.position, transform.rotation);
        //newProjectile.GetComponent<Projectile>().abilityStatus = abilityStatus;

        //newProjectile = Instantiate(hurricanePrefabs, transform.position, right);
        //newProjectile.GetComponent<Projectile>().abilityStatus = abilityStatus;
    }

}
