using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum enumState
    {
        Idle, MoveAttack, Attacking, Chasing, Attacked
    };

    public enumState _state;
    public enumState State { get { return _state; } }

    private void Start()
    {
        _state = enumState.Idle;
    }

    public void Transition(enumState nextState)
    {
        _state = nextState;
    }
}
