using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheDustySkill_1 : Skill
{
    public float DamageScale = 10;
    Enemy enemy;
    Collider col;
    public UnityEvent unityEvent;

    void Start()
    {
        col = GetComponent<Collider>();
        enemy = transform.parent.parent.GetComponent<Enemy>();
    }

    public override void Activate()
    {
        col.enabled = true;
    }

    public override void AnimationActivate()
    {
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
