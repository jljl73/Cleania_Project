using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatSkill2 : EnemySkill
{
    // public AbilityStatus myAbility;
    public EnemyChase parentEnemyChase;

    float triggerChance;
    int divisionCount;
    float SummonRange;
    float divisionAvailableCount = 1;
    public float DivisionAvailableCount { get { return divisionAvailableCount; } set { divisionAvailableCount = value; } }

    public GameObject DivisionObject;

    [SerializeField]
    DivisionSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Start()
    {
        base.Start();
        // myAbility = transform.parent.parent.GetComponent<Enemy>().abilityStatus;
        // parentEnemyChase = transform.parent.parent.GetComponentInChildren<EnemyChase>();
        if (parentEnemyChase == null)
            throw new System.Exception("BatSkill2 dosent have parentEnemyChase");

        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        base.UpdateSkillData(skillData);

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

    public override bool AnimationActivate()
    {
        // animator.SetTrigger("Attack Soundwave");

        return true;
    }

    override public void Activate()
    {
        if (!(Random.Range(0f, 1f) <= triggerChance)) return;
        if (divisionAvailableCount == 0) return;

        divisionAvailableCount--;

        for (int i = 0; i < divisionCount; i++)
        {

            //GameObject bat1 = Instantiate(enemy.gameObject);
            Enemy batEnemy = ObjectPool.SpawnFromPool<Enemy>(ObjectPool.enumPoolObject.WildInti, GetRandPosition(transform.position, SummonRange), transform.rotation);
            // bat1.transform.Translate(bat1.transform.right * bat1.transform.localScale.x);
            batEnemy.transform.position = GetRandPosition(transform.position, SummonRange);
            batEnemy.transform.localScale *= 0.5f;
            batEnemy.GetComponentInChildren<EnemyChase>().EnemySpawner = parentEnemyChase.EnemySpawner;
            batEnemy.GetComponentInChildren<BatSkill2>().DivisionAvailableCount = divisionAvailableCount;
            if (batEnemy != null)
            {
                batEnemy.Revive();
                batEnemy.abilityStatus.FullHP();
            }
        }
    }

    public override void Deactivate()
    {
    }
}
