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

    protected override void Update()
    {
        base.Update();

        if (!isHittingByBodySkillTriggered && (ability.HP < ability.GetStat(Ability.Stat.MaxHP) * 0.1f))
        {
            if (enemySkillManager.PlaySkill(2102))
                isHittingByBodySkillTriggered = true;
        }
    }
}
