using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDustySkillTrigger : EnemySkillTrigger
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // if (!IsSkillAvailable()) return;
            enemySkillManager.PlaySkill(0);
        }
    }
}
