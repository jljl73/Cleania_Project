using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public enum MonsterType
    {
        Dusty = 5001,
        WildInti = 5002,
        HighDusty = 5003,
        SummonerDusty = 5004,
        TheDusty = 7001
    }

    [SerializeField]
    MonsterType monsterType;
    public MonsterType GetMonsterType() { return monsterType; }

    public enum enumRank
    {
        Normal = 0,
        Rare = 1000,
        Boss
    }

    [SerializeField]
    enumRank rank = enumRank.Normal;
    public enumRank Rank { get { return rank; } set { rank = value; } }

    public int ID
    {
        get
        {
            string temp = "";
            if(rank == enumRank.Rare)
                temp = ((int)monsterType + rank).ToString();
            else
                temp = ((int)monsterType).ToString();
            return int.Parse(temp);

        }
    }

    public bool CompareState(enumRank state)
    {
        return this.rank == state;
    }

    public void Transition(enumRank nextState)
    {
        rank = nextState;
    }
}
