using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill : Skill
{
    public Enemy enemy;
    protected EnemyMovement enemyMove;
    protected EnemyChase enemyChase;

    protected void Awake()
    {
        if (enemy == null)
            throw new System.Exception("EnemySkill doesnt have enemy");
        enemyMove = enemy.enemyMove;
        enemyChase = enemy.GetComponentInChildren<EnemyChase>();
    }

    public void UpdateSkillData(EnemySkillSO skillData)
    {
        if (skillData == null)
            throw new System.Exception("SpecialAbilityIngrainedDirt no skillData");

        isPassiveSkill = skillData.GetIsPassiveSkill();
        ID = skillData.ID;
        SkillName = skillData.GetSkillName();
        SkillDetails = skillData.GetSkillDetails();
        CoolTime = skillData.GetCoolTime();
        CreatedMP = skillData.GetCreatedMP();
        ConsumMP = skillData.GetConsumMP();
        SpeedMultiplier = skillData.GetSpeedMultiplier();
        triggerPosition = skillData.GetTriggerPosition();
        triggerRange = skillData.GetTriggerRange();
    }

    public override void Deactivate()
    {
        enemy.enemyStateMachine.Transition(EnemyStateMachine.enumState.Idle);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            OnEnemyTriggerZone.Invoke(true, ID);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            OnEnemyTriggerZone.Invoke(false, ID);
    }
}
