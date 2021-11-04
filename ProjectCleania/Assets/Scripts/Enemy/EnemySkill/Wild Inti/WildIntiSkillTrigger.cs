using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildIntiSkillTrigger : EnemySkillTrigger
{
    public EnemyStateMachine stateMachine;
    Collider[] overlappedColliders;

    public float TriggerRange = 2f;

    private new void Awake()
    {
        base.Awake();

        if (stateMachine == null)
            throw new System.Exception("WildIntiSkillTrigger doesnt have stateMachine");
    }

    private void Update()
    {
        if (stateMachine.CompareState(EnemyStateMachine.enumState.Dead))
            return;

        overlappedColliders = Physics.OverlapSphere(transform.position, TriggerRange);
        foreach (Collider collider in overlappedColliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (enemySkillManager.PlayRandomSpecialSkill())
                    return;

                if (enemySkillManager.PlaySkill(2201))
                    return;
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        print("Player in WildIntiSkillTrigger");
    //        enemySkillManager.PlaySkill(0);
    //    }
    //}
}
