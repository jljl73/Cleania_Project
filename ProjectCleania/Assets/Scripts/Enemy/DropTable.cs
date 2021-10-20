using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : ScriptableObject
{
    public int GetDropCount(EnemyStateMachine.enumRank enemyType)
    {
        int result = 0;

        float percentage = Random.value;

        switch (enemyType)
        {
            case EnemyStateMachine.enumRank.Normal:
                result = 0;
                if (percentage > 0.05)
                    result += 0;
                else
                    result += 1;
                break;

            case EnemyStateMachine.enumRank.Rare:
                result = 2;
                if (percentage > 0.42)
                    result += 0;
                else if (percentage > 0.05)
                    result += 1;
                else if (percentage > 0.02)
                    result += 2;
                else
                    result += 3;
                break;

            case EnemyStateMachine.enumRank.Bose:
                result = 10;
                if (percentage > 0.66)
                    result += 1;
                else if (percentage > 0.33)
                    result += 2;
                else
                    result += 3;
                break;
            default:
                break;
        }
        return result;
    }

    // »óÀÚ

    //public int GetDropCount(EnemyStateMachine.enumRank enemyType)
    //{
    //    int result = 0;

    //    float percentage = Random.value;

        
    //    return result;
    //}
}
