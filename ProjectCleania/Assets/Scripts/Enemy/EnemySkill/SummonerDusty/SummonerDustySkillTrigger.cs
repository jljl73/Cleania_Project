using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerDustySkillTrigger : EnemySkillTrigger
{
    #region
    //[Header("소환 주기")]
    //public float SummonTimeInterval = 10.0f;
    //bool isSummonCoolTime = false;

    //[Header("회오리 공격 주기")]
    //public float SpinAttackTimeInterval = 7.0f;
    //bool isSpinAttackCoolTime = false;
    #endregion

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
