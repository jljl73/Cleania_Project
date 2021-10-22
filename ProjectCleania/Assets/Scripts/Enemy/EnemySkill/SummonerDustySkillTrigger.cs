using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerDustySkillTrigger : EnemySkillTrigger
{
    EnemySkillManager enemySkillManager;
    WaitForSeconds SummonWaitForSeconds;
    WaitForSeconds SpinAttackWaitForSeconds;

    public EnemyMove enemyMove;

    [Header("소환 주기")]
    public float SummonTimeInterval = 10.0f;
    bool isSummonCoolTime = false;

    [Header("회오리 공격 주기")]
    public float SpinAttackTimeInterval = 7.0f;
    bool isSpinAttackCoolTime = false;

    private void Awake()
    {
        enemySkillManager = GetComponent<EnemySkillManager>();
    }

    void Start()
    {
        SummonWaitForSeconds = new WaitForSeconds(SummonTimeInterval);
        SpinAttackWaitForSeconds = new WaitForSeconds(SpinAttackTimeInterval);
    }

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

    private void Update()
    {
        if (enemyMove.ExistTarget())
        {
            if (isSummonCoolTime) return;

            if (IsSKillAvailable())
            {
                StartCoroutine(Summon());
            }
        }
    }

    IEnumerator Summon()
    {
        enemySkillManager.PlaySkill(0);    // 소환 스킬 발동
        isSummonCoolTime = true;
        yield return SummonWaitForSeconds;
        isSummonCoolTime = false;
    }

    IEnumerator SpinAttack()
    {
        enemySkillManager.PlaySkill(2);         // 스핀 어택 스킬 발동
        isSpinAttackCoolTime = true;
        yield return SpinAttackWaitForSeconds;
        isSpinAttackCoolTime = false;
    }

    bool IsSKillAvailable()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) && !animator.IsInTransition(0))
            return true;
        else
            return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("isSpinAttackCoolTime: " + isSpinAttackCoolTime);
            if (isSpinAttackCoolTime) return;

            if (IsSKillAvailable())
            {
                StartCoroutine(SpinAttack());
            }
        }
    }
}
