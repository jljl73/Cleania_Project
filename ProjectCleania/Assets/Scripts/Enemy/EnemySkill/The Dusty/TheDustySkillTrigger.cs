using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDustySkillTrigger : EnemySkillTrigger
{
    public AbilityStatus ability;
    public EnemyStateMachine stateMachine;
    Collider[] overlappedColliders;

    public float TriggerRange = 5f;

    bool isHittingByBodySkillTriggered = false;

    private new void Awake()
    {
        base.Awake();

        if (ability == null)
        {
            throw new System.Exception("No ability on TheDustySkillTrigger");
        }

        stateMachine = GetComponent<EnemyStateMachine>();
        if (stateMachine == null)
            throw new System.Exception("TheDustySkillTrigger doesnt have stateMachine");
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
                //if (enemySkillManager.PlayRandomSpecialSkill())
                //    return;

                //if (enemySkillManager.PlaySkill(2501))
                //    return;

                if (enemySkillManager.PlaySkill(2502))
                    return;
            }
        }


        //if (!isHittingByBodySkillTriggered && (ability.HP < ability.GetStat(Ability.Stat.MaxHP) * 0.1f))
        //{
        //    if (!enemySkillManager.IsSkillAvailable())
        //        return;

        //    enemySkillManager.PlaySkill(2102);
        //    isHittingByBodySkillTriggered = true;
        //}
    }
}
