using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public StateMachine playerStateMachine;
    public Skill[] skills = new Skill[6];
    Skill skill;
    
    void Update()
    {
        skill = InputHandler();

        if (skill)
            skill.AnimationActivate();
    }

    Skill InputHandler()
    {
        
        // 키보드
        if (Input.GetKeyDown(KeyCode.Alpha1) && isSkillAvailable())
            return skills[0];
        if (Input.GetKeyDown(KeyCode.Alpha2) && isSkillAvailable())
            return skills[1];
        if (Input.GetKeyDown(KeyCode.Alpha3) && isSkillAvailable())
            return skills[2];
        if (Input.GetKeyDown(KeyCode.Alpha4) && isSkillAvailable())
            return skills[3];

        // 마우스
        if(Input.GetKeyDown(KeyCode.C) && isSkillAvailable())
            return skills[4];
        if (Input.GetMouseButton(1) && (isSkillAvailable() || 
            playerStateMachine.State == StateMachine.enumState.MoveAttack))
            return skills[5];

        return null;
    }

    public void ActivateSkill(int type)
    {
        skills[type].Activate();
    }

    public void AnimationDeactivate(int type)
    {
        skills[type].AnimationDeactivate();
        //playerStateMachine.Transition(StateMachine.enumState.Idle);
    }

    bool isSkillAvailable()
    {
        if (playerStateMachine.State == StateMachine.enumState.Idle ||
            playerStateMachine.State == StateMachine.enumState.Chasing)
            return true;
        else
            return false;
    }
}
