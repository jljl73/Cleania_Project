using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineSetter : StateMachineBehaviour
{
    StateMachine playerStateMachine;

    private void Awake()
    {
        playerStateMachine = FindObjectOfType<Player>().stateMachine;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerStateMachine.Transition(StateMachine.enumState.Idle);
    }
}
