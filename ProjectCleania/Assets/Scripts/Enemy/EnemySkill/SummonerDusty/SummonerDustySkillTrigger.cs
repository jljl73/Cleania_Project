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
    //[Header("소환 주기")]
    //public float SummonTimeInterval = 10.0f;
    //bool isSummonCoolTime = false;

    //[Header("회오리 공격 주기")]
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
    //                enemySkillManager.PlaySkill(0);    // 소환 스킬 발동
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
                //if (enemySkillManager.PlayRandomSpecialSkill())
                //    return;

                if (enemySkillManager.PlaySkill(2907))
                    return;

                //// 먼지바람
                //if (enemySkillManager.PlaySkill(2402))
                //    return;
            }
        }

        //if (enemyMove.ExistTarget())
        //{
        //    enemySkillManager.PlaySkill(2401);    // 소환 스킬 발동
        //}
    }

    #region
    //IEnumerator Summon()
    //{
    //    enemySkillManager.PlaySkill(0);    // 소환 스킬 발동
    //    isSummonCoolTime = true;
    //    yield return SummonWaitForSeconds;
    //    isSummonCoolTime = false;
    //}

    //IEnumerator SpinAttack()
    //{
    //    enemySkillManager.PlaySkill(2);         // 스핀 어택 스킬 발동
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
    //        enemySkillManager.PlaySkill(2);         // 스핀 어택 스킬 발동
    //    }
    //}

    #endregion

}
