using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheDustySkill_1 : EnemySkill
{
    public float DamageScale = 10;
    Collider col;
    public UnityEvent unityEvent;

    new void Start()
    {
        base.Start();
        col = GetComponent<Collider>();
    }

    public override void Activate()
    {
        col.enabled = true;
    }

    public override bool AnimationActivate()
    {
        return true;
    }

    public override void Deactivate()
    {
        animator.SetFloat("Probability", Random.Range(0, 1.0f));
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().abilityStatus.AttackedBy(enemy.abilityStatus, DamageScale);
        }
    }

}
