using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineSetter : StateMachineBehaviour
{
    public StateMachine playerStateMachine;

    private void Awake()
    {
        playerStateMachine = FindObjectOfType<Player>().stateMachine;
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        playerStateMachine.Transition(StateMachine.enumState.Idle);
        Debug.Log("OnStateExit to idle!");
    }
}
