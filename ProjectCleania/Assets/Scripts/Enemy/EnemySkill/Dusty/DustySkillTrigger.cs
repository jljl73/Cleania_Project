using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustySkillTrigger : EnemySkillTrigger
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
            throw new System.Exception("No ability on DustySkillTrigger");
        }

        stateMachine = GetComponent<EnemyStateMachine>();
        if (stateMachine == null)
            throw new System.Exception("DustySkillTrigger doesnt have stateMachine");
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
                // if (!IsSkillAvailable()) return;
                enemySkillManager.PlaySkill(0);
            }
        }


        if (!isHittingByBodySkillTriggered && (ability.HP < ability.GetStat(Ability.Stat.MaxHP) * 0.1f))
        {
            if (!enemySkillManager.isSkillAvailable())
                return;

            enemySkillManager.PlaySkill(1);
            isHittingByBodySkillTriggered = true;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //        enemySkillManager.PlaySkill(0);
    //}
}
