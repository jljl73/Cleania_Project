using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighDustySkillTrigger : EnemySkillTrigger
{
    Collider[] overlappedColliders;

    public float TriggerRange = 5f;

    private void Update()
    {
        overlappedColliders = Physics.OverlapSphere(transform.position, TriggerRange);
        foreach (Collider collider in overlappedColliders)
        {
            if (collider.CompareTag("Player"))
            {
                // if (!IsSkillAvailable()) return;
                // enemySkillManager.PlaySkill(0);
                enemySkillManager.PlaySkill(0);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        // if (!IsSkillAvailable()) return;
    //        enemySkillManager.PlaySkill(0);
    //    }
    //}
}
