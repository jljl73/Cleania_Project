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

                //// ≈Õ∑ø
                //if (enemySkillManager.PlaySkill(2912))
                //    return;

                //// æÛ∑Ë
                //if (enemySkillManager.PlaySkill(2907))
                //    return;

                enemySkillManager.PlayRandomSpecialSkill();

                //// ∫Œ∆–
                //if (enemySkillManager.PlaySkill(2908))
                //    return;

                //if (enemySkillManager.PlayRandomSpecialSkill())
                //    return;

                //// µπ«≥
                //if (enemySkillManager.PlaySkill(2902))
                //    return;

                //// ¡ˆ∑⁄
                //if (enemySkillManager.PlaySkill(2910))
                //    return;

                //// ∫¿¿Œ
                //if (enemySkillManager.PlaySkill(2906))
                //    return;

                //// µ∂º∫
                //if (enemySkillManager.PlaySkill(2901))
                //    return;

                //// ∏’¡ˆ ≈ı√¥
                //if (enemySkillManager.PlaySkill(2301))
                //    return;
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
