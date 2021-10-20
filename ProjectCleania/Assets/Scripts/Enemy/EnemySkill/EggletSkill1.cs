using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggletSkill1 : EnemySkill
{
    public float DamageScale = 10;

    Collider col;
    // Enemy enemy;

    private new void Start()
    {
        base.Start();

        col = GetComponent<Collider>();
        // enemy = transform.parent.parent.GetComponent<Enemy>();
    }

    public override void AnimationActivate()
    {
        // col.enabled = true;
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
