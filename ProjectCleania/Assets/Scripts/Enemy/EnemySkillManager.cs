using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySkillManager : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    //private AbilityStatus playerAbilityStatus;

    public Skill[] skills = new Skill[6];
    Skill curSkill;
    bool isCouroutineRunning;

    public void ActivateSkill(int type)
    {
        skills[type].Activate();
    }

    public void DeactivateSkill(int type)
    {
        skills[type].Deactivate();
    }


    bool isSkillAvailable(int skillIndex)
    {
        if (stateMachine.State == EnemyStateMachine.enumState.Idle ||
            stateMachine.State == EnemyStateMachine.enumState.Chasing)
            return true;
        else
            return false;
    }

}
