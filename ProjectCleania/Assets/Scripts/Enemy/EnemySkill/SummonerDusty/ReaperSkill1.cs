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

    public SummonSkillSO SkillData;

    private new void Awake()
    {
        base.Awake();
        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        SkillName = SkillData.GetSkillName();
        SkillDetails = SkillData.GetSkillDetails();
        CoolTime = SkillData.GetCoolTime();
        CreatedMP = SkillData.GetCreatedMP();
        ConsumMP = SkillData.GetConsumMP();
        SpeedMultiplier = SkillData.GetSpeedMultiplier();

        highDusty = SkillData.GetHighDustyForSummon();
        normalDusty = SkillData.GetNormalDustyForSummon();
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

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("Summon");
    }
}
