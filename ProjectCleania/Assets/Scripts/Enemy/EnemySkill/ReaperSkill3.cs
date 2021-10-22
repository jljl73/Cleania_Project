using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill3 : EnemySkill
{
    public float DamageScale = 10;
    public float hitForce = 1;

    Collider col;

    private new void Start()
    {
        base.Start();

        col = GetComponent<Collider>();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("SpinAttack");
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
            other.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(other.transform.position - this.transform.position) * hitForce);
            //other.GetComponent<Player>()
        }
    }

    public override void Deactivate()
    {
        col.enabled = false;
        animator.SetBool("OnSkill", false);
    }
}
