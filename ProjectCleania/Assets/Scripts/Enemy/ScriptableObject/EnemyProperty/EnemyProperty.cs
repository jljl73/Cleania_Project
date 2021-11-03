using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperty : MonoBehaviour
{
    public enum MonsterType
    {
        Dusty = 5001,
        WildInti = 5002,
        HighDusty = 5003,
        SummonerDusty = 5004,
        TheDusty = 7001
    }

    public MonsterType monsterType;

    public enum enumRank
    {
        Normal = 0,
        Rare = 1000,
        Bose
    }

    public enumRank rank = enumRank.Normal;
    public enumRank Rank { get { return rank; } set { rank = value; } }

    public void Transition(enumRank nextState)
    {
        rank = nextState;
    }

    public bool CompareState(enumRank state)
    {
        return this.rank == state;
    }

    public int ID
    {
        get
        {
            string temp = "";
            temp += ((int)monsterType + rank).ToString();
            return int.Parse(temp);

        }
    }
}
