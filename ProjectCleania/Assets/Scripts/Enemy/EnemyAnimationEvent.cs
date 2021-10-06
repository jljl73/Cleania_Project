using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    Skill skill = null;

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
    }

    public void ActivateSkill()
    {
        if (skill != null) skill.Activate();
        Debug.Log(skill.gameObject.name);
    }

    public void AnimationDeactivate()
    {
        if (skill != null) skill.AnimationDeactivate();
    }
}
