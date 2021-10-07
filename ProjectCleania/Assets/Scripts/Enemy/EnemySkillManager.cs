using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySkillManager : MonoBehaviour
{
    public StateMachine stateMachine;
    //private AbilityStatus playerAbilityStatus;

    public Skill[] skills = new Skill[6];
    Skill curSkill;
    bool isCouroutineRunning;
       
    public void ActivateSkill(int type)
    {
        skills[type].Activate();
    }

    public void AnimationDeactivate(int type)
    {
        skills[type].AnimationDeactivate();
    }


    bool isSkillAvailable(int skillIndex)
    {
        if (stateMachine.State == StateMachine.enumState.Idle ||
            stateMachine.State == StateMachine.enumState.Chasing)
            return true;
        else
            return false;
    }

}
