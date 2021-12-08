using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropPercentTable", menuName = "Scriptable Object/DropPercentTable")]
public class DropTableSO : ScriptableObject
{
    [SerializeField]
    float[] NormalAdditionalProb = new float[] { 99.5f };
    [SerializeField]
    float[] RareAdditionalProb = new float[] { 58.0f, 95.0f, 98.0f };
    [SerializeField] 
    float[] BossAdditionalProb = new float[] { 0.0f, 34.0f, 67.0f};

    [SerializeField]
    float[] NormalRankProb = new float[] { 75.0f, 99.0f };
    [SerializeField]
    float[] RareRankProb = new float[] { 10.0f, 85.0f };
    [SerializeField]
    float[] BossRankProb = new float[] { 2.0f, 78.0f };

    [System.Serializable]
    public struct QuestItem
    {
        public Quest quest;
        public int itemID;
        public float Prob;
    }
    [SerializeField]
    QuestItem[] questItems;


    public int GetDropCount(EnemyStateMachine.enumRank rank)
    {
        float value = Random.Range(0.0f, 100.0f);
        int i = 0;
        if(rank == EnemyStateMachine.enumRank.Normal)
        {
            for (; i < NormalAdditionalProb.Length; ++i)
                if (value < NormalAdditionalProb[i]) break;
        }
        else if(rank == EnemyStateMachine.enumRank.Rare)
        {
            for (i = 2; i < RareAdditionalProb.Length; ++i)
                if (value < RareAdditionalProb[i]) break;
        }
        else
        {
            for (i = 10; i < BossAdditionalProb.Length; ++i)
                if (value < BossAdditionalProb[i]) break;
        }
        return i;
    }

    public Item.ITEMRANK GetDropRank(EnemyStateMachine.enumRank rank)
    {
        float value = Random.Range(0.0f, 100.0f);
        int i = 0;
        if (rank == EnemyStateMachine.enumRank.Normal)
        {
            for (; i < NormalRankProb.Length; ++i)
                if (value < NormalRankProb[i]) break;
        }
        else if (rank == EnemyStateMachine.enumRank.Rare)
        {
            for (; i < RareRankProb.Length; ++i)
                if (value < RareRankProb[i]) break;
        }
        else
        {
            for (; i < BossRankProb.Length; ++i)
                if (value < BossRankProb[i]) break;
        }
        return (Item.ITEMRANK)i;
    }

    public bool GetQuestItem(out int itemID)
    {
        itemID = 0;
        float value = Random.Range(0.0f, 100.0f);
        for(int i = 0; i< questItems.Length;++i)
        {
            if(questItems[i].quest.State == Quest.STATE.Assign)
            {
                if (value > questItems[i].Prob)
                    return false;

                itemID = questItems[i].itemID;
                return true;
            }
        }
        return false;
    }

    #region Legacy
    //[Header("일반 등급 몬스터")]
    //public int NormalMonsterFixedDropCnt = 0;
    //public float NormalMonsterAdditionalDrop0Ratio = 0.995f;
    //public float NormalMonsterAdditionalDrop1Ratio = 0.005f;
    //public float NormalMonsterAdditionalDrop2Ratio = 0;
    //public float NormalMonsterAdditionalDrop3Ratio = 0;

    //public float NormalMonsterDropItemRankRareRatio = 0.24f;
    //public float NormalMonsterDropItemRankLegendRatio = 0.01f;

    //[Header("희귀 등급 몬스터")]
    //public int RareMonsterFixedDropCnt = 2;
    //public float RareMonsterAdditionalDrop0Ratio = 0.58f;
    //public float RareMonsterAdditionalDrop1Ratio = 0.37f;
    //public float RareMonsterAdditionalDrop2Ratio = 0.03f;
    //public float RareMonsterAdditionalDrop3Ratio = 0.02f;

    //public float RareMonsterDropItemRankRareRatio = 0.75f;
    //public float RareMonsterDropItemRankLegendRatio = 0.15f;

    //[Header("보스 등급 몬스터")]
    //public int BoseMonsterFixedDropCnt = 10;
    //public float BoseMonsterAdditionalDrop0Ratio = 0;
    //public float BoseMonsterAdditionalDrop1Ratio = 0.34f;
    //public float BoseMonsterAdditionalDrop2Ratio = 0.33f;
    //public float BoseMonsterAdditionalDrop3Ratio = 0.33f;

    //public float BoseMonsterDropItemRankRareRatio = 0.76f;
    //public float BoseMonsterDropItemRankLegendRatio = 0.22f;

    //[Header("상자")]
    //public int BoxFixedDropCnt = 0;
    //public float BoxAdditionalDrop0Ratio = 0;
    //public float BoxAdditionalDrop1Ratio = 0.80f;
    //public float BoxAdditionalDrop2Ratio = 0.15f;
    //public float BoxAdditionalDrop3Ratio = 0.05f;

    //public float BoxDropItemRankRareRatio = 0.32f;
    //public float BoxDropItemRankLegendRatio = 0.07f;

    //public int GetDropCount(EnemyStateMachine.enumRank enemyType)
    //{
    //    int result = 0;

    //    float percentage = Random.value;

    //    Debug.Log("GetDropCount");

    //    switch (enemyType)
    //    {
    //        case EnemyStateMachine.enumRank.Normal:
    //            Debug.Log("Normal Monster Get drop count!");
    //            result = NormalMonsterFixedDropCnt;
    //            if (percentage > (1 - NormalMonsterAdditionalDrop0Ratio))
    //                result += 0;
    //            else if (percentage > (NormalMonsterAdditionalDrop0Ratio + NormalMonsterAdditionalDrop1Ratio))
    //                result += 1;
    //            else if (percentage > (NormalMonsterAdditionalDrop0Ratio + NormalMonsterAdditionalDrop1Ratio + NormalMonsterAdditionalDrop2Ratio))
    //                result += 2;
    //            else
    //                result += 3;
    //            break;

    //        case EnemyStateMachine.enumRank.Rare:
    //            Debug.Log("Rare Monster Get drop count!");
    //            result = RareMonsterFixedDropCnt;
    //            if (percentage > (1 - RareMonsterAdditionalDrop0Ratio))
    //                result += 0;
    //            else if (percentage > (RareMonsterAdditionalDrop0Ratio + RareMonsterAdditionalDrop1Ratio))
    //                result += 1;
    //            else if (percentage > (RareMonsterAdditionalDrop0Ratio + RareMonsterAdditionalDrop1Ratio + RareMonsterAdditionalDrop2Ratio))
    //                result += 2;
    //            else
    //                result += 3;
    //            break;

    //        case EnemyStateMachine.enumRank.Bose:
    //            Debug.Log("Bose Monster Get drop count!");
    //            result = BoseMonsterFixedDropCnt;
    //            if (percentage > (1 - BoseMonsterAdditionalDrop0Ratio))
    //                result += 0;
    //            else if (percentage > (BoseMonsterAdditionalDrop0Ratio + BoseMonsterAdditionalDrop1Ratio))
    //                result += 1;
    //            else if (percentage > (BoseMonsterAdditionalDrop0Ratio + BoseMonsterAdditionalDrop1Ratio + BoseMonsterAdditionalDrop2Ratio))
    //                result += 2;
    //            else
    //                result += 3;
    //            break;
    //        default:
    //            Debug.Log("default!");
    //            break;
    //    }
    //    return result;
    //}

    //// 상자

    //public int GetBoxDropCount()
    //{
    //    int result = 0;

    //    float percentage = Random.value;

    //    result = BoxFixedDropCnt;
    //    if (percentage > (1 - BoxAdditionalDrop0Ratio))
    //        result += 0;
    //    else if (percentage > (BoxAdditionalDrop0Ratio + BoxAdditionalDrop1Ratio))
    //        result += 1;
    //    else if (percentage > (BoxAdditionalDrop0Ratio + BoxAdditionalDrop1Ratio + BoxAdditionalDrop2Ratio))
    //        result += 2;
    //    else
    //        result += 3;

    //    return result;
    //}


    //public Item.ITEMRANK GetDropRank(EnemyStateMachine.enumRank enemyType)
    //{
    //    Item.ITEMRANK itemRank = Item.ITEMRANK.Normal;

    //    float percentage = 0f;

    //    switch (enemyType)
    //    {
    //        case EnemyStateMachine.enumRank.Normal:
    //            percentage = Random.value;
    //            if (percentage > (1 - NormalMonsterDropItemRankRareRatio))
    //            {
    //                itemRank = Item.ITEMRANK.Rare;
    //                break;
    //            }

    //            percentage = Random.value;
    //            if (percentage > (1 - NormalMonsterDropItemRankLegendRatio))
    //            {
    //                itemRank = Item.ITEMRANK.Legendary;
    //                break;
    //            }
    //            break;

    //        case EnemyStateMachine.enumRank.Rare:
    //            percentage = Random.value;
    //            if (percentage > (1 - RareMonsterDropItemRankRareRatio))
    //            {
    //                itemRank = Item.ITEMRANK.Rare;
    //                break;
    //            }

    //            percentage = Random.value;
    //            if (percentage > (1 - RareMonsterDropItemRankLegendRatio))
    //            {
    //                itemRank = Item.ITEMRANK.Legendary;
    //                break;
    //            }
    //            break;

    //        case EnemyStateMachine.enumRank.Bose:
    //            percentage = Random.value;
    //            if (percentage > (1 - BoseMonsterDropItemRankRareRatio))
    //            {
    //                itemRank = Item.ITEMRANK.Rare;
    //                break;
    //            }

    //            percentage = Random.value;
    //            if (percentage > (1 - BoseMonsterDropItemRankLegendRatio))
    //            {
    //                itemRank = Item.ITEMRANK.Legendary;
    //                break;
    //            }
    //            break;

    //        default:
    //            break;
    //    }
    //    return itemRank;
    //}

    //public Item.ITEMRANK GetBoxDropRank()
    //{
    //    Item.ITEMRANK itemRank = Item.ITEMRANK.Normal;

    //    float percentage = 0f;

    //    percentage = Random.value;
    //    if (percentage > (1 - BoxDropItemRankRareRatio))
    //    {
    //        itemRank = Item.ITEMRANK.Rare;
    //    }

    //    percentage = Random.value;
    //    if (percentage > (1 - BoxDropItemRankLegendRatio))
    //    {
    //        itemRank = Item.ITEMRANK.Legendary;
    //    }

    //    return itemRank;
    #endregion
}

