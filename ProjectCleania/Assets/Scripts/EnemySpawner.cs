using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject RareMonster;
    public float RareMonsterWeight = 100;

    public GameObject NormalMonster;
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
            Instantiate(RareMonster, GetRandomPointInCircle(this.transform.position, SpawnRadius), this.transform.rotation);
            tempTotalWeight -= RareMonsterWeight;
        }

        while (tempTotalWeight > 0)
        {
            Instantiate(NormalMonster, GetRandomPointInCircle(this.transform.position, SpawnRadius), this.transform.rotation);
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
