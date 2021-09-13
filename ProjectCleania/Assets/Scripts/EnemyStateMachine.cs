using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public enum EnumState { Idle, NormalChase, DirectionChase, NormalChaseAttack, Attacking, Attacked };
    public EnumState State;

    void Start()
    {
        State = EnumState.Idle;
    }

    public void Transition(EnumState state)
    {
        State = state;
    }
}
