using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill1 : EnemySkill
{
    GameObject highDusty;
    GameObject normalDusty;

    GameObject newEnemy;
    WaitForSeconds waitForSeconds;

    // [Header("��ȯ �ֱ�")]
    // public float timeInterval = 10.0f;

    [SerializeField]
    SummonSkillSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Start()
    {
        base.Start();
        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        highDusty = skillData.GetHighDustyForSummon();
        normalDusty = skillData.GetNormalDustyForSummon();
    }

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
        //newEnemy.GetComponent<Enemy>().EnemySpawner = enemy.EnemySpawner;
        newEnemy.GetComponentInChildren<EnemyChase>().EnemySpawner = enemyChase.EnemySpawner;

        newEnemy = Instantiate(normalDusty, Random.insideUnitSphere + transform.position, this.transform.rotation);
        newEnemy.GetComponentInChildren<EnemyChase>().EnemySpawner = enemyChase.EnemySpawner;

        //for (int i = 0; i < 3; ++i)
        //{
        //    newEnemy = Instantiate(normalDusty, Random.insideUnitSphere + transform.position, this.transform.rotation);
        //    newEnemy.GetComponentInChildren<EnemyChase>().EnemySpawner = enemyChase.EnemySpawner;
        //}
    }


    public override void Deactivate()
    {
        animator.SetBool("OnSkill", false);
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("Summon");

        return true;
    }
}
