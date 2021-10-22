using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemDrop : MonoBehaviour
{
    public float DropRadius = 1.0f;
    public DropTableSO DropTableData;

    Enemy myEnemy;
    EnemyStateMachine enemyStateMachine;

    private void Awake()
    {
        myEnemy = transform.parent.GetComponent<Enemy>();
        myEnemy.OnDead += DropItem;

        enemyStateMachine = myEnemy.gameObject.GetComponent<EnemyStateMachine>();
    }

    void DropItem()
    {
        print("ItemDrop start!");
        // ���� ��� Ȯ��
        EnemyStateMachine.enumRank monsterRank = enemyStateMachine.Rank;

        // ���� ��� ���̺� �����Ͽ� ��� ���� ����
        int dropCount = DropTableData.GetDropCount(monsterRank);

        print("dropCount: " + dropCount);

        for (int i = 0; i < dropCount; i++)
        {
            // ���� ��� ����
            Item.ITEMRANK itemRank = DropTableData.GetDropRank(monsterRank);

            print("Dropped : " + itemRank.ToString());

            // ������ ����
            //ItemInstance.Instantiate(1101001); ������ ������ ����
            SavedData.Instance.Item_World.Add(ItemInstance.Instantiate(1101001), GetDropPosition(transform.position, DropRadius));

            //Instantiate(ItemTable.GetRandomItemObj(itemRank), GetDropPosition(transform.position, DropRadius), transform.rotation);
        }
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
