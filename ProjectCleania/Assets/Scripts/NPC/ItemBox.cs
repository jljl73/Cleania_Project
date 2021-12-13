using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemBox : MonoBehaviour
{
    public float DropRadius = 1.0f;
    public DropTableSO DropTableData;

    bool IsUsed = false;
    [SerializeField]
    EnemyStateMachine.enumRank monsterRank;
    
    void DropItem()
    {
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

    void OnTriggerStay(Collider other)
    {
        if (!IsUsed &&other.CompareTag("Player") && Input.GetKeyDown(KeyCode.G))
        {
            DropItem();
            IsUsed = true;
        }
    }
}
