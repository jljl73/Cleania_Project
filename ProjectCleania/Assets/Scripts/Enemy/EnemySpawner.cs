using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> RareMonsters;
    public float RareMonsterWeight = 100;

    public List<GameObject> NormalMonsters;
    public float NormalMonsterWeight = 2;

    public float SpawnRadius = 10f;

    [Header("특수 능력 부여 갯수")]
    [SerializeField]
    int SpecialSkillCount = 3;

    List<int> SpecialSkillIDs = new List<int>();

    float weight;
    int rareMonsterCount = 1;

    public float Weight { get { return weight; } set { weight = value; } }
    public int RareMonsterCount { set { rareMonsterCount = value; } }

    public void Spawn()
    {
        float tempTotalWeight = weight;

        //SelectRandomSkillID();

        for (int i = 0; i < rareMonsterCount; i++)
        {
            if (tempTotalWeight < 0)
                break;
            GameObject newMonster = GenerateObject(RareMonsters[Random.Range(0, RareMonsters.Count)]);
            newMonster.GetComponentInChildren<EnemyChase>().EnemySpawner = gameObject;
            tempTotalWeight -= RareMonsterWeight;

            EnemySkillManager enemySkillManager = newMonster.GetComponent<EnemySkillManager>();
            if (enemySkillManager == null)
                throw new System.Exception("newMonster doesnt have enemySkillManager");

            if (i == 0)
            {
                ResetSpecialSkillIDs(enemySkillManager);
            }

            SetSpecialSkillTo(enemySkillManager);
        }

        while (tempTotalWeight > 0)
        {
            GameObject newMonster = Instantiate(NormalMonsters[Random.Range(0, NormalMonsters.Count)], GetRandomPointInCircle(this.transform.position, SpawnRadius), this.transform.rotation);
            newMonster.GetComponentInChildren<EnemyChase>().EnemySpawner = gameObject;
            tempTotalWeight -= NormalMonsterWeight;
        }
    }

    GameObject GenerateObject(GameObject obj)
    {
        Vector3 generatedPose = GetRandomPointInCircle(this.transform.position, SpawnRadius);
        EnemyStateMachine.MonsterType monsterType = obj.GetComponent<EnemyStateMachine>().GetMonsterType();
        Enemy enemy;
        switch (monsterType)
        {
            case EnemyStateMachine.MonsterType.HighDusty
            :
                enemy = ObjectPool.SpawnFromPool<Enemy>(ObjectPool.enumPoolObject.HighDusty, generatedPose, this.transform.rotation);
                break;
            case EnemyStateMachine.MonsterType.SummonerDusty:
                enemy = ObjectPool.SpawnFromPool<Enemy>(ObjectPool.enumPoolObject.SummonerDusty, generatedPose, this.transform.rotation);
                break;
            case EnemyStateMachine.MonsterType.Dusty:
                enemy = ObjectPool.SpawnFromPool<Enemy>(ObjectPool.enumPoolObject.Dusty, generatedPose, this.transform.rotation);
                break;
            default:
                enemy = ObjectPool.SpawnFromPool<Enemy>(ObjectPool.enumPoolObject.WildInti, generatedPose, this.transform.rotation);
                break;
        }

        return enemy.gameObject;
    }


    Vector3 GetRandomPointInCircle(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas))
            return hit.position;
        else
            return center;
    }

    void ResetSpecialSkillIDs(EnemySkillManager enemySkillManager)
    {
        SpecialSkillIDs.Clear();
        int selectedCount = 0;
        while (selectedCount < SpecialSkillCount)
        {
            int candidateID = enemySkillManager.GetRandomSpecialSkillAvailableID();
            if (SpecialSkillIDs.Contains(candidateID)) continue;

            SpecialSkillIDs.Add(candidateID);
            selectedCount++;
        }
    }

    void SetSpecialSkillTo(EnemySkillManager enemySkillManager)
    {
        for (int i = 0; i < SpecialSkillIDs.Count; i++)
        {
            enemySkillManager.MakeSpecialSkillAvailable(SpecialSkillIDs[i]);
        }
    }
}
