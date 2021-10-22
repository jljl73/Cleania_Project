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

    public void PlaySkill(int index)
    {
        skills[index].AnimationActivate();
    }

    public void ActivateSkill(int type)
    {
        skills[type].Activate();
    }

    public void DeactivateSkill(int type)
    {
        skills[type].Deactivate();
    }

    public void ActivateSkillEffect(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        // n번 스킬 칸에 있는 스킬의 N번째 스킬 이팩트를 써라
        if (skillEffectIndexSet != null)
            skills[skillEffectIndexSet.GetSkillIndex()].PlayEffects(skillEffectIndexSet.GetEffectIndex());
        else
        {
            for (int i = 0; i < skills[myEvent.intParameter].effectController.Count; i++)
            {
                skills[myEvent.intParameter].PlayEffects(i);
            }
        }
    }

    public void DeactivateSkillEffect(AnimationEvent myEvent)
    {
        SkillEffectIndexSO skillEffectIndexSet = myEvent.objectReferenceParameter as SkillEffectIndexSO;

        // n번 스킬 칸에 있는 스킬의 N번째 스킬 이팩트를 써라
        if (skillEffectIndexSet != null)
            skills[skillEffectIndexSet.GetSkillIndex()].StopEffects(skillEffectIndexSet.GetEffectIndex());
        else
        {
            for (int i = 0; i < skills[myEvent.intParameter].effectController.Count; i++)
            {
                skills[myEvent.intParameter].StopEffects(i);
            }
        }
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
