using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill2 : Skill
{
    public AbilityStatus abilityStatus;
    public float skillScale = 1.0f;

    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    public override void AnimationActivate()
    {
        animator.SetInteger("Skill", 2);
        animator.SetBool("OnSkill", true);
    }

    override public void Activate()
    {
        col.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if (other.GetComponent<Enemy>().abilityStatus.AttackedBy(abilityStatus, skillScale) == 0)
                other.GetComponent<Enemy>().Die();
            else
                other.GetComponent<Enemy>().enemyMove.WarpToPosition(transform.position + transform.forward);
        }
    }

    void OffSkill()
    {
        col.enabled = false;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
        OffSkill();
    }

}
