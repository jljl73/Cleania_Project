using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
    public enum enumState
    {
        Idle, MoveAttack, Walk, Attacking, Chasing, Attacked, ReadyAttack, Dead
    };

    enumState state = enumState.Idle;
    public enumState State { get { return state; } }

    public void Transition(enumState nextState)
    {
        if (state == enumState.Dead) return;
        state = nextState;
    }

    public bool CompareState(enumState state)
    {
        return this.state == state;
    }
}
