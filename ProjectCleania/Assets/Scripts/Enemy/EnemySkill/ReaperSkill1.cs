using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill1 : EnemySkill
{
    public GameObject highDusty;
    public GameObject normalDusty;

    GameObject newEnemy;
    WaitForSeconds waitForSeconds;

    [Header("소환 주기")]
    public float timeInterval = 10.0f;

   
    new void Start()
    {
        base.Start();

        enemy = transform.parent.parent.GetComponent<Enemy>();
        enemyMove = enemy.enemyMove;
        waitForSeconds = new WaitForSeconds(timeInterval);
        StartCoroutine(Summon());
    }

    IEnumerator Summon()
    {
        while(true)
        {
            if(enemyMove.ExistTarget())
            {
                animator.SetBool("OnSkill", true);
                animator.SetTrigger("Summon");
            }
            yield return waitForSeconds;
        }
    }

    public override void Activate()
    {
        newEnemy = Instantiate(highDusty, Random.insideUnitSphere + transform.position, this.transform.rotation);
        // newEnemy.GetComponent<Enemy>().EnemySpawner = enemy.EnemySpawner;
        newEnemy.GetComponent<EnemyChase>().EnemySpawner = enemyChase.EnemySpawner;

        for (int i = 0; i < 3; ++i)
        {
            newEnemy = Instantiate(normalDusty, Random.insideUnitSphere + transform.position, this.transform.rotation);
            newEnemy.GetComponent<EnemyChase>().EnemySpawner = enemyChase.EnemySpawner;
        }
    }

    public override void AnimationActivate()
    {
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
    }

}
