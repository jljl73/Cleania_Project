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

    float weight;
    int rareMonsterCount = 1;

    public float Weight { get { return weight; } set { weight = value; } }
    public int RareMonsterCount { set { rareMonsterCount = value; } }

    public void Spawn()
    {
        float tempTotalWeight = weight;
        print("rareMonsterCount: " + rareMonsterCount);
        // float tempRareMosterCount = rareMonsterCount;
        for (int i = 0; i < rareMonsterCount; i++)
        {
            if (tempTotalWeight < 0)
                break;
            GameObject newMonster = Instantiate(RareMonsters[Random.Range(0, RareMonsters.Count)], GetRandomPointInCircle(this.transform.position, SpawnRadius), this.transform.rotation);
            newMonster.GetComponent<Enemy>().enemySpawner = gameObject;
            tempTotalWeight -= RareMonsterWeight;
        }

        while (tempTotalWeight > 0)
        {
            GameObject newMonster = Instantiate(NormalMonsters[Random.Range(0, NormalMonsters.Count)], GetRandomPointInCircle(this.transform.position, SpawnRadius), this.transform.rotation);
            newMonster.GetComponent<Enemy>().enemySpawner = gameObject;
            tempTotalWeight -= NormalMonsterWeight;
        }
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
}
