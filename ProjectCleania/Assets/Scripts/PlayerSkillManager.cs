using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public Skill[] skills = new Skill[4];
    Skill skill;
    
    void Update()
    {
        skill = InputHandler();

        if (skill)
            skill.AnimationActivate();
    }

    Skill InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            return skills[0];
        if (Input.GetKeyDown(KeyCode.Alpha2))
            return skills[1];
        if (Input.GetKeyDown(KeyCode.Alpha3))
            return skills[2];
        if (Input.GetKeyDown(KeyCode.Alpha4))
            return skills[3];

        return null;
    }

    public void ActivateSkill(int type)
    {
        skills[type].Activate();
    }

}
