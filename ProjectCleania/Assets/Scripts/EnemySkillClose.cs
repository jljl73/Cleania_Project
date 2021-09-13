using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c : Skill
{
    public Animator EnemyAnimator;
    public Collider Collider;
    public EnemyStateMachine enemyStateMachine;

    public override void Activate()
    {
        Collider.enabled = true;
    }

    public override void AnimationActivate()
    {
        EnemyAnimator.SetBool("OnAttack", true);
        enemyStateMachine.Transition(EnemyStateMachine.EnumState.Attacking);
    }

    public override void AnimationDeactivate()
    {
        Collider.enabled = false;
        EnemyAnimator.SetBool("OnAttack", false);
        enemyStateMachine.Transition(EnemyStateMachine.EnumState.Idle);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Give damage
        print("AnimationActivate collider entered");
    }
}
