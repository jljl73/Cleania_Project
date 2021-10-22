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

   
    //new void Start()
    //{
    //    base.Start();

    //    enemy = transform.parent.parent.GetComponent<Enemy>();
    //    enemyMove = enemy.enemyMove;
    //    waitForSeconds = new WaitForSeconds(timeInterval);
    //    StartCoroutine(Summon());
    //}

    //IEnumerator Summon()
    //{
    //    while(true)
    //    {
    //        if(enemyMove.ExistTarget())
    //        {
    //            animator.SetBool("OnSkill", true);
    //            animator.SetTrigger("Summon");
    //        }
    //        yield return waitForSeconds;
    //    }
    //}

    public override void Activate()
    {

        newEnemy = Instantiate(highDusty, Random.insideUnitSphere + transform.position, this.transform.rotation);
        // newEnemy.GetComponent<Enemy>().EnemySpawner = enemy.EnemySpawner;

        if (newEnemy == null)
            print("newEnemy is null");
        if (enemyChase == null)
            print("enemyChase is null");
        if (newEnemy.GetComponentInChildren<EnemyChase>() == null)
            print("newEnemy.GetComponentInChildren<EnemyChase>() == null");

        newEnemy.GetComponentInChildren<EnemyChase>().EnemySpawner = enemyChase.EnemySpawner;

        for (int i = 0; i < 3; ++i)
        {
            newEnemy = Instantiate(normalDusty, Random.insideUnitSphere + transform.position, this.transform.rotation);
            newEnemy.GetComponentInChildren<EnemyChase>().EnemySpawner = enemyChase.EnemySpawner;
        }
    }


    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("Summon");
    }
}
