using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildIntiSkillTrigger : EnemySkillTrigger
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player in WildIntiSkillTrigger");
            enemySkillManager.PlaySkill(0);
        }
    }
}
