using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemDrop : MonoBehaviour
{
    public float DropRadius = 1.0f;
    public DropTableSO DropTableData;

    public Enemy myEnemy;
    EnemyStateMachine enemyStateMachine;

    private void Awake()
    {
        // myEnemy = transform.parent.GetComponent<Enemy>();
        if (myEnemy == null)
            throw new System.Exception("ItemDrop doesnt have myEnemy");

        myEnemy.OnDead += DropItem;

        enemyStateMachine = GetComponent<EnemyStateMachine>();
        if (enemyStateMachine == null)
            throw new System.Exception("ItemDrop doesnt have enemyStateMachine");
    }

    void DropItem()
    {
        // 몬스터 등급 확인
        EnemyStateMachine.enumRank monsterRank = enemyStateMachine.Rank;

        // 몬스터 드랍 테이블 참조하여 드랍 수량 결정
        int dropCount = DropTableData.GetDropCount(monsterRank);

        for (int i = 0; i < dropCount; i++)
        {
            // 전설 등급 결정
            Item.ITEMRANK itemRank = DropTableData.GetDropRank(monsterRank);

            // 아이템 생성
            //ItemInstance.Instantiate(1101001); 아이템 데이터 생성
            SavedData.Instance.Item_World.Add(ItemInstance.Instantiate_RandomByRank((ItemSO.enumRank)Random.Range(0, 3)), GetDropPosition(transform.position, DropRadius));

            //Instantiate(ItemTable.GetRandomItemObj(itemRank), GetDropPosition(transform.position, DropRadius), transform.rotation);
        }

        if(enemyStateMachine.ID == 5001)
            SavedData.Instance.Item_World.Add(ItemInstance.Instantiate(3666001), GetDropPosition(transform.position, DropRadius));
    }

    Vector3 GetDropPosition(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center;
    }
}
