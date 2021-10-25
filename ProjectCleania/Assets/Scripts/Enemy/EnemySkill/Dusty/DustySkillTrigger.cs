using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustySkillTrigger : EnemySkillTrigger
{
    public AbilityStatus ability;

    bool isHittingByBodySkillTriggered = false;

    private new void Awake()
    {
        base.Awake();

        if (ability == null)
        {
            throw new System.Exception("No ability on DustySkillTrigger");
        }
    }

    private void Update()
    {
        if (!isHittingByBodySkillTriggered && (ability.HP < ability.GetStat(Ability.Stat.MaxHP) * 0.1f))
        {
            if (!enemySkillManager.isSkillAvailable())
                return;

            print("Play suicide skill");
            enemySkillManager.PlaySkill(1);
            isHittingByBodySkillTriggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            enemySkillManager.PlaySkill(0);
    }
}
