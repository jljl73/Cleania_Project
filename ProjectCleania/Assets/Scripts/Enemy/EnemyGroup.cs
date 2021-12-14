using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGroup : MonoBehaviour
{
    [System.Serializable]
    public struct Monster
    {
        //public EnemyStateMachine.MonsterType monsterType;
        public GameObject newMonster;
        public int number;
        public bool isRare;
    }


    public float spawnRadius = 2.0f;
    [SerializeField]
    Monster[] monsters;

    [Header("특수 능력 부여 갯수")]
    [SerializeField]
    int SpecialSkillCount = 3;
    List<int> SpecialSkillIDs = new List<int>();

    [SerializeField]
    int MonsterLevel = 1;
    GameObject newMonster;

    bool isFirst = false;


    private void Start()
    {
        Spawn();
    }


    void Spawn()
    {
        for(int i = 0; i < monsters.Length; ++i)
        {
            for (int j = 0; j < monsters[i].number; ++j)
            {
                Vector3 pos = GetRandomPointInCircle(transform.position, spawnRadius);
                //switch (monsters[i].monsterType)
                //{
                //    case EnemyStateMachine.MonsterType.Dusty:
                //        //newMonster = ObjectPool.SpawnFromPool<Enemy>(ObjectPool.enumPoolObject.Dusty, pos, this.transform.rotation).gameObject;
                //        break;
                //    case EnemyStateMachine.MonsterType.WildInti:
                //        //newMonster = ObjectPool.SpawnFromPool<Enemy>(ObjectPool.enumPoolObject.WildInti, pos, this.transform.rotation).gameObject;
                //        break;
                //    case EnemyStateMachine.MonsterType.HighDusty:
                //        //newMonster = ObjectPool.SpawnFromPool<Enemy>(ObjectPool.enumPoolObject.HighDusty, pos, this.transform.rotation).gameObject;
                //        break;
                //    case EnemyStateMachine.MonsterType.SummonerDusty:
                //        //newMonster= ObjectPool.SpawnFromPool<Enemy>(ObjectPool.enumPoolObject.SummonerDusty, pos, this.transform.rotation).gameObject;
                //        break;
                //}
                newMonster = Instantiate(monsters[i].newMonster, pos, Quaternion.Euler(0, Random.Range(0, 360.0f), 0), transform);
                newMonster.GetComponentInChildren<EnemyChase>().EnemySpawner = gameObject;
                newMonster.GetComponent<Status>().Level = MonsterLevel;

                if (monsters[i].isRare)
                {
                    EnemySkillManager enemySkillManager = newMonster.GetComponent<EnemySkillManager>();
                    if (enemySkillManager == null)
                        throw new System.Exception("newMonster doesnt have enemySkillManager");

                    if (!isFirst)
                    {
                        ResetSpecialSkillIDs(enemySkillManager);
                        isFirst = true;
                    }
                    SetSpecialSkillTo(enemySkillManager);
                }
            }
        }
        
    }

    Vector3 GetRandomPointInCircle(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas))
            return hit.position;
        else
            return center;
    }

    void ResetSpecialSkillIDs(EnemySkillManager enemySkillManager)
    {
        SpecialSkillIDs.Clear();
        int selectedCount = 0;
        // 비효율적임
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
