using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawnTest : MonoBehaviour
{
    [SerializeField]
    GameObject wildIntiPrefab;
    [SerializeField]
    GameObject dustyPrefab;
    [SerializeField]
    GameObject highDustyPrefab;
    [SerializeField]
    GameObject summonerDustyPrefab;
    [SerializeField]
    GameObject theDustyPrefab;

    GameObject spawnReadyGamePrefab;

    enum enumMonsterSpecialSkillType
    {
        Toxicity = 2901,
        StormWind,
        IngrainedDirt,
        Pollution,
        FastFeet,
        Seal = 2906,
        Decomposition = 2908,
        HPShare = 2909,
        Mine = 2910,
        Stain = 2907,
        Turret = 2912
    }

    [Header("스폰 범위")]
    [SerializeField]
    float spawnedRadius = 3f;

    [Header("몬스터 종류")]
    [SerializeField]
    EnemyStateMachine.MonsterType monsterType = EnemyStateMachine.MonsterType.WildInti;

    [Header("몬스터 등급")]
    [SerializeField]
    EnemyStateMachine.enumRank monsterRank = EnemyStateMachine.enumRank.Normal;

    [Header("특수 스킬")]
    [SerializeField]
    List<enumMonsterSpecialSkillType> specialSkills = new List<enumMonsterSpecialSkillType>();

    [Header("몬스터 레벨")]
    [SerializeField]
    int level = 1;

    [Header("몬스터 수")]
    [SerializeField]
    int count = 0;

    EnemyGroupManager enemyGroupManager;

    //List<GameObject> monsters = new List<GameObject>();
    Stack<GameObject> monsterStack = new Stack<GameObject>();

    private void Awake()
    {
        enemyGroupManager = GetComponent<EnemyGroupManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            print("total count: " + monsterStack.Count);
        }
    }

    public void Spawn()
    {
        ObjectPool.enumPoolObject enumPoolObject = ObjectPool.enumPoolObject.WildInti;

        switch (monsterType)
        {
            case EnemyStateMachine.MonsterType.Dusty:
                enumPoolObject = ObjectPool.enumPoolObject.Dusty;
                spawnReadyGamePrefab = dustyPrefab;
                break;
            case EnemyStateMachine.MonsterType.WildInti:
                enumPoolObject = ObjectPool.enumPoolObject.WildInti;
                spawnReadyGamePrefab = wildIntiPrefab;
                break;
            case EnemyStateMachine.MonsterType.HighDusty:
                enumPoolObject = ObjectPool.enumPoolObject.HighDusty;
                spawnReadyGamePrefab = highDustyPrefab;
                break;
            case EnemyStateMachine.MonsterType.SummonerDusty:
                enumPoolObject = ObjectPool.enumPoolObject.SummonerDusty;
                spawnReadyGamePrefab = summonerDustyPrefab;
                break;
            case EnemyStateMachine.MonsterType.TheDusty:
                enumPoolObject = ObjectPool.enumPoolObject.TheDusty;
                spawnReadyGamePrefab = theDustyPrefab;
                break;
            default:
                break;
        }

        for (int i = 0; i < count; i++)
        {
            //GameObject monster = ObjectPool.SpawnFromPool<Enemy>(enumPoolObject, GetRandomPointInCircle(this.transform.position, spawnedRadius), transform.rotation).gameObject;
            GameObject monster = Instantiate(spawnReadyGamePrefab, GetRandomPointInCircle(this.transform.position, spawnedRadius), transform.rotation);

            // 스포너 설정
            EnemyChase enemyChase = monster.GetComponentInChildren<EnemyChase>();
            enemyChase.EnemySpawner = this.gameObject;

            // 등급 설정
            EnemyStateMachine enemyState = monster.GetComponent<EnemyStateMachine>();
            enemyState.Rank = monsterRank;

            // 특수 스킬 성정
            if (enemyState.Rank == EnemyStateMachine.enumRank.Rare || enemyState.Rank == EnemyStateMachine.enumRank.Boss)
            {
                EnemySkillManager enemySkillManager = monster.GetComponent<EnemySkillManager>();
                for (int j = 0; j < specialSkills.Count; j++)
                {
                    enemySkillManager.MakeSpecialSkillAvailable((int)specialSkills[j]);
                }
            }

            // 레벨 설정
            Status_ArithmeticProgress levelComponent = monster.GetComponent<Status_ArithmeticProgress>();
            levelComponent.Level = level;

            monsterStack.Push(monster);
        }
    }

    public void GroupMosters()
    {
        if (monsterStack.Count == enemyGroupManager.Count())
            return;

        foreach (GameObject monster in monsterStack)
        {
            if (!enemyGroupManager.IsMemberExist(monster))
                enemyGroupManager.AddMember(monster);
        }
    }

    public void ClearMonster()
    {
        GameObject monster = monsterStack.Pop();
        monster.SetActive(false);
        if (enemyGroupManager.IsMemberExist(monster))
            enemyGroupManager.DeleteMember(monster);
    }

    public void ClearAllMonster()
    {
        while (monsterStack.Count > 0)
        {
            GameObject monster = monsterStack.Pop();
            monster.SetActive(false);
            if (enemyGroupManager.IsMemberExist(monster))
                enemyGroupManager.DeleteMember(monster);
        }
       
        monsterStack.Clear();
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

    public void OnMonsterRankEdited(Dropdown sender)
    {
        switch (sender.value)
        {
            case 0:
                monsterRank = EnemyStateMachine.enumRank.Normal;
                break;
            case 1:
                monsterRank = EnemyStateMachine.enumRank.Rare;
                break;
            case 2:
                monsterRank = EnemyStateMachine.enumRank.Boss;
                break;
            default:
                break;
        }
    }

    public void OnMonsterLevelEdited(InputField sender)
    {
        level = int.Parse(sender.text);
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
