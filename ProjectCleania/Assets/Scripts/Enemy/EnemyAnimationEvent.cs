using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public EnemySkillManager skillManager;
    
    public void ActivateSkill(int type)
    {
        skillManager.ActivateSkill(type);
    }

    public void AnimationDeactivate(int type)
    {
        skillManager.AnimationDeactivate(type);
    }
}
