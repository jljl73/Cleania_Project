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
                //if (enemySkillManager.PlaySkill(2907))
                //    return;

                //if (enemySkillManager.PlayRandomSpecialSkill())
                //    return;

                // ���� ��ô
                if (enemySkillManager.PlaySkill(2301))
                    return;
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
