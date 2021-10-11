using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill1 : Skill
{
    public GameObject highDusty;
    public GameObject normalDusty;

    public Enemy enemy;
    EnemyMove enemyMove;
    GameObject newEnemy;
    WaitForSeconds waitForSeconds;
    float timeInterval = 10.0f;
    
    void Start()
    {
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
                animator.SetTrigger("Summon");
                animator.SetBool("OnSkill", true);
            }
            yield return waitForSeconds;
        }
    }

    public override void Activate()
    {
        newEnemy = Instantiate(highDusty, Random.insideUnitSphere + transform.position, this.transform.rotation);
        newEnemy.GetComponent<Enemy>().enemySpawner = enemy.enemySpawner;

        for (int i = 0; i < 3; ++i)
        {
            newEnemy = Instantiate(normalDusty, Random.insideUnitSphere + transform.position, this.transform.rotation);
            newEnemy.GetComponent<Enemy>().enemySpawner = enemy.enemySpawner;
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
