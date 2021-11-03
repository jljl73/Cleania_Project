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

                //// �ͷ�
                //if (enemySkillManager.PlaySkill(2912))
                //    return;

                //// ���
                //if (enemySkillManager.PlaySkill(2907))
                //    return;

                enemySkillManager.PlayRandomSpecialSkill();

                //// ����
                //if (enemySkillManager.PlaySkill(2908))
                //    return;

                //if (enemySkillManager.PlayRandomSpecialSkill())
                //    return;

                //// ��ǳ
                //if (enemySkillManager.PlaySkill(2902))
                //    return;

                //// ����
                //if (enemySkillManager.PlaySkill(2910))
                //    return;

                //// ����
                //if (enemySkillManager.PlaySkill(2906))
                //    return;

                //// ����
                //if (enemySkillManager.PlaySkill(2901))
                //    return;

                //// ���� ��ô
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
