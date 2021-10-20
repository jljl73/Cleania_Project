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

    public enumState state;
    public enumState State { get { return state; } }

    protected void Awake()
    {
        state = enumState.Idle;
    }

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
