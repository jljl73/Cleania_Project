using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawnTest : MonoBehaviour
{
    [Header("스폰 범위")]
    [SerializeField]
    float spawnedRadius = 3f;

    [Header("몬스터 종류")]
    [SerializeField]
    EnemyStateMachine.MonsterType monsterType = EnemyStateMachine.MonsterType.WildInti;

    [Header("몬스터 수")]
    [SerializeField]
    int count = 0;

    //List<GameObject> monsters = new List<GameObject>();
    Stack<GameObject> mostersStack = new Stack<GameObject>();

    public void Spawn()
    {
        ObjectPool.enumPoolObject enumPoolObject = ObjectPool.enumPoolObject.WildInti;

        switch (monsterType)
        {
            case EnemyStateMachine.MonsterType.Dusty:
                enumPoolObject = ObjectPool.enumPoolObject.Dusty;
                break;
            case EnemyStateMachine.MonsterType.WildInti:
                enumPoolObject = ObjectPool.enumPoolObject.WildInti;
                break;
            case EnemyStateMachine.MonsterType.HighDusty:
                enumPoolObject = ObjectPool.enumPoolObject.HighDusty;
                break;
            case EnemyStateMachine.MonsterType.SummonerDusty:
                enumPoolObject = ObjectPool.enumPoolObject.SummonerDusty;
                break;
            case EnemyStateMachine.MonsterType.TheDusty:
                enumPoolObject = ObjectPool.enumPoolObject.TheDusty;
                break;
            default:
                break;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject monster = ObjectPool.SpawnFromPool<Enemy>(enumPoolObject, GetRandomPointInCircle(this.transform.position, spawnedRadius), transform.rotation).gameObject;
            mostersStack.Push(monster);
        }
    }

    public void ClearMonster()
    {
        GameObject monster = mostersStack.Pop();
        monster.SetActive(false);
    }

    public void ClearAllMonster()
    {
        while (mostersStack.Count > 0)
        {
            GameObject monster = mostersStack.Pop();
            monster.SetActive(false);
        }
       
        mostersStack.Clear();
    }

    public void OnMonsterTypeSelected(Dropdown sender)
    {
        switch (sender.value)
        {
            case 0:
                this.monsterType = EnemyStateMachine.MonsterType.WildInti;
                break;
            case 1:
                this.monsterType = EnemyStateMachine.MonsterType.Dusty;
                break;
            case 2:
                this.monsterType = EnemyStateMachine.MonsterType.HighDusty;
                break;
            case 3:
                this.monsterType = EnemyStateMachine.MonsterType.SummonerDusty;
                break;
            case 4:
                this.monsterType = EnemyStateMachine.MonsterType.TheDusty;
                break;
            default:
                break;
        }
    }

    public void OnMonsterCountEdited(InputField sender)
    {
        count = int.Parse(sender.text);
    }

    public void OnMonsterSpawnedRadiusEdited(InputField sender)
    {
        spawnedRadius = int.Parse(sender.text);
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
}
