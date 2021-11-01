using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerDustySkillTrigger : EnemySkillTrigger
{
    // WaitForSeconds SummonWaitForSeconds;
    // WaitForSeconds SpinAttackWaitForSeconds;

    public EnemyMove enemyMove;
    public EnemyStateMachine stateMachine;
    Collider[] overlappedColliders;

    public float TriggerRange = 5f;

    #region
    //[Header("��ȯ �ֱ�")]
    //public float SummonTimeInterval = 10.0f;
    //bool isSummonCoolTime = false;

    //[Header("ȸ���� ���� �ֱ�")]
    //public float SpinAttackTimeInterval = 7.0f;
    //bool isSpinAttackCoolTime = false;
    #endregion

    void Start()
    {
        //SummonWaitForSeconds = new WaitForSeconds(SummonTimeInterval);
        //SpinAttackWaitForSeconds = new WaitForSeconds(SpinAttackTimeInterval);
    }

    #region
    //IEnumerator Summon()
    //{
    //    while (true)
    //    {
    //        if (enemyMove.ExistTarget())
    //        {
    //            if (IsSKillAvailable())
    //                enemySkillManager.PlaySkill(0);    // ��ȯ ��ų �ߵ�
    //        }
    //        yield return SummonWaitForSeconds;
    //    }
    //}
    #endregion

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
                enemySkillManager.PlaySkill(2);
            }
        }

        if (enemyMove.ExistTarget())
        {
            #region
            //if (isSummonCoolTime) return;

            //if (IsSkillAvailable())
            //{
            //    StartCoroutine(Summon());
            //}
            #endregion
            enemySkillManager.PlaySkill(0);    // ��ȯ ��ų �ߵ�
        }
    }

    #region
    //IEnumerator Summon()
    //{
    //    enemySkillManager.PlaySkill(0);    // ��ȯ ��ų �ߵ�
    //    isSummonCoolTime = true;
    //    yield return SummonWaitForSeconds;
    //    isSummonCoolTime = false;
    //}

    //IEnumerator SpinAttack()
    //{
    //    enemySkillManager.PlaySkill(2);         // ���� ���� ��ų �ߵ�
    //    isSpinAttackCoolTime = true;
    //    yield return SpinAttackWaitForSeconds;
    //    isSpinAttackCoolTime = false;
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        #region
    //        //print("isSpinAttackCoolTime: " + isSpinAttackCoolTime);
    //        //if (isSpinAttackCoolTime) return;

    //        //if (IsSkillAvailable())
    //        //{
    //        //    StartCoroutine(SpinAttack());
    //        //}
    //        #endregion
    //        enemySkillManager.PlaySkill(2);         // ���� ���� ��ų �ߵ�
    //    }
    //}

    #endregion

}
