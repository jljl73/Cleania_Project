using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillManager : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;
    private Animator enemyAnimator;

    // 0: ���� ����
    // 1: ���Ÿ� ����
    public List<Skill> Skills;
    private Skill skill = null;

    private void Awake()
    {
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        enemyAnimator = GetComponent<Animator>();
    }

    public void ActivateSkill(int idx)
    {
        skill[idx].Activate();
    }

    void Update()
    {
        if (!IsSkillAvailable()) return;

        switch (enemyStateMachine.State)
        {
            case EnemyStateMachine.EnumState.NormalChase:
                skill = null;
                break;
            case EnemyStateMachine.EnumState.DirectionChase:
                // ���Ÿ� ����
                // ���Ÿ� ���� ����
                // ���� ��ȯ

                
                break;

            case EnemyStateMachine.EnumState.Attacking:
                skill = null;
                break;
            case EnemyStateMachine.EnumState.NormalChaseAttack:
                skill = Skills[0];
                enemyStateMachine.Transition(EnemyStateMachine.EnumState.Attacking);
                skill = null;
                break;
            case EnemyStateMachine.EnumState.Attacked:
                skill = null;
                break;

        }

        if (skill != null)
            skill.AnimationActivate();
    }

    bool IsSkillAvailable()
    {
        switch (enemyStateMachine.State)
        {
            case EnemyStateMachine.EnumState.Attacking:
                skill = null;
                break;
        }
        return true;
    }
}
