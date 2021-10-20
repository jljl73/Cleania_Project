using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public enum enumRank
    {
        Normal, Rare, Bose
    }

    public enumRank rank = enumRank.Normal;
    public enumRank Rank { get { return rank; } set { rank = value; } }

    new void Awake()
    {
        base.Awake();
    }

    public void Transition(enumRank nextState)
    {
        rank = nextState;
    }

    public bool CompareState(enumRank state)
    {
        return this.rank == state;
    }
}
