using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatSkill2 : EnemySkill
{
    public AbilityStatus myAbility;
    EnemyChase parentEnemyChase;

    float triggerChance;
    int divisionCount;
    float SummonRange;

    public GameObject DivisionObject;

    [SerializeField]
    DivisionSO skillData;

    private new void Start()
    {
        base.Start();
        myAbility = transform.parent.parent.GetComponent<Enemy>().abilityStatus;
        parentEnemyChase = transform.parent.parent.GetComponentInChildren<EnemyChase>();

        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        SkillName = skillData.GetSkillName();
        SkillDetails = skillData.GetSkillDetails();
        CoolTime = skillData.GetCoolTime();
        CreatedMP = skillData.GetCreatedMP();
        ConsumMP = skillData.GetConsumMP();
        SpeedMultiplier = skillData.GetSpeedMultiplier();

        triggerChance = skillData.GetTriggerChance();
        SummonRange = skillData.GetSummonRange();
        divisionCount = skillData.GetDividedCount();
    }

    Vector3 GetRandPosition(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center;
    }

    public override void AnimationActivate()
    {
        // animator.SetTrigger("Attack Soundwave");
    }

    override public void Activate()
    {
        if (!(Random.Range(0f, 1f) <= triggerChance)) return;

        print("분열 발동!");

        for (int i = 0; i < divisionCount; i++)
        {
            GameObject bat1 = Instantiate(DivisionObject);
            // bat1.transform.Translate(bat1.transform.right * bat1.transform.localScale.x);
            bat1.transform.position = GetRandPosition(transform.position, SummonRange);
            bat1.transform.localScale *= 0.5f;
            bat1.GetComponentInChildren<EnemyChase>().EnemySpawner = parentEnemyChase.EnemySpawner;
            bat1.GetComponent<Enemy>().abilityStatus.FullHP();
        }

        //GameObject bat1 = Instantiate(DivisionObject);
        //GameObject bat2 = Instantiate(DivisionObject);

        //bat1.transform.Translate(bat1.transform.right * bat1.transform.localScale.x);
        //bat2.transform.Translate(-bat1.transform.right * bat2.transform.localScale.x);

        //bat1.transform.localScale *= 0.5f;
        //bat2.transform.localScale *= 0.5f;

        //bat1.GetComponentInChildren<EnemyChase>().EnemySpawner = parentEnemyChase.EnemySpawner;
        //bat2.GetComponentInChildren<EnemyChase>().EnemySpawner = parentEnemyChase.EnemySpawner;

        //Status bat1stat = bat1.GetComponent<Enemy>().status;
        //Status bat2stat = bat2.GetComponent<Enemy>().status;

        //bat1stat.Atk = myStat.Atk / 2;
        //bat2stat.Atk = myStat.Atk / 2;

        //bat1stat.BasicHP = myStat.BasicHP / 2;
        //bat2stat.BasicHP = myStat.BasicHP / 2;

        //bat1stat.Def = myStat.Def / 2;
        //bat2stat.Def = myStat.Def / 2;

        //bat1stat.Strength = myStat.Strength / 2;
        //bat2stat.Strength = myStat.Strength / 2;

        //bat1stat.Vitality = myStat.Vitality / 2;
        //bat2stat.Vitality = myStat.Vitality / 2;

        //bat1stat.levelUpStrength = myStat.levelUpStrength / 2;
        //bat2stat.levelUpStrength = myStat.levelUpStrength / 2;

        //bat1stat.levelUpVitality = myStat.levelUpVitality / 2;
        //bat2stat.levelUpVitality = myStat.levelUpVitality / 2;

        //bat1.GetComponent<Enemy>().abilityStatus.FullHP();
        //bat2.GetComponent<Enemy>().abilityStatus.FullHP();
    }

    public override void Deactivate()
    {
    }
}
