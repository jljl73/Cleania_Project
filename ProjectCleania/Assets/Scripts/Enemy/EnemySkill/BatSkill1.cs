using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSkill1 : Skill
{
    public float DamageScale = 10;
    public float bloodChance = 0.3f;
    public float bloodTime = 5.0f;

    Collider col;
    Enemy enemy;

    private void Start()
    {
        enemy = transform.parent.parent.GetComponent<Enemy>();
        col = GetComponent<Collider>();
    }

    public override void AnimationActivate()
    {
        animator.SetTrigger("Attack Bite");
        //animator.SetInteger("Skill", 1);
    }

    override public void Activate()
    {
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().abilityStatus.AttackedBy(enemy.abilityStatus, DamageScale);
        }
    }

    public override void Deactivate()
    {
        col.enabled = false;
    }

}
