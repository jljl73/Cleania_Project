using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustySkillSuicide : EnemySkill
{
    [SerializeField]
    DustySuicideSO skillData;

    float damageScale = 10;
    float damageRange = 1.5f;
    float angryDuration = 3;
    float skillTriggerPercentage = 0.5f;
    float triggerHPRate;

    bool isTriggered = false;

    Vector3 targetSuicidePosition;

    [SerializeField]
    enumSkillState skillState = enumSkillState.None;
    enum enumSkillState
    {
        None,
        Angry,
        Fly,
        Bomb
    }

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
        damageRange = skillData.GetDamageRange();
        skillTriggerPercentage = skillData.GetTriggerChance();
        triggerHPRate = skillData.GetTriggerHPRate();
        angryDuration = skillData.GetAngryDuration();
    }

    private void FixedUpdate()
    {
        if (enemy.enemyStateMachine.CompareState(StateMachine.enumState.Dead))
            return;

        switch (skillState)
        {
            case enumSkillState.Angry:
                break;
            case enumSkillState.Fly:
                FlyToSuicide();
                break;
            case enumSkillState.Bomb:
                break;
            default:
                break;
        }
    }

    void DeactivateDelay() => enemy.gameObject.SetActive(false);

    public override bool AnimationActivate()
    {
        if (!(Random.Range(0f, 1f) < skillTriggerPercentage))
            return true;

        animator.SetBool("OnSkill", true);
        animator.SetTrigger("Angry");

        return true;
    }

    void FlyToSuicide()
    {
        if (Vector3.Distance(this.transform.position, targetSuicidePosition) < damageRange)
        {
            animator.SetTrigger("Suicide");
            skillState = enumSkillState.None;
        }
    }

    IEnumerator AngryToFlyForward(float angryDuration)
    {
        yield return new WaitForSeconds(angryDuration);
        animator.SetTrigger("AngryToFlyForward");
        SetOnlyChasePositionMode();

    }

    public override void Activate(int id)
    {
        base.Activate();
        switch (id)
        {
            case 0:
                skillState = enumSkillState.Angry;

                enemyMove.StopMoving(true);
                StartCoroutine("AngryToFlyForward", angryDuration);
                break;
            case 1:
                skillState = enumSkillState.Fly;
                break;
            case 2:
                skillState = enumSkillState.Bomb;
                print("DoSuicide attack!");
                DoSuicideAttack();
                break;
            default:
                break;
        }
        //bombCollider.enabled = true;
    }

    public override void Deactivate(int id)
    {
        base.Activate();
        switch (id)
        {
            case 0:
                skillState = enumSkillState.None;
                enemyMove.StopMoving(false);
                break;
            case 1:
                skillState = enumSkillState.None;
                break;
            case 2:
                skillState = enumSkillState.None;
                break;
            default:
                break;
        }
        //bombCollider.enabled = false;
        animator.SetBool("OnSkill", false);
    }
    void SetOnlyChasePositionMode()
    {
        if (enemyMove.TargetObject != null)
        {
            enemyMove.SetOnlyChasePositionMode(true, enemyMove.TargetObject.transform.position);
            targetSuicidePosition = enemyMove.TargetObject.transform.position;
        }
    }
    void DoSuicideAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, damageRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                AbilityStatus playerAbil = colliders[i].GetComponentInChildren<AbilityStatus>();
                if (playerAbil != null)
                {
                    playerAbil.AttackedBy(enemy.abilityStatus, damageScale);
                    Invoke("DeactivateDelay", 3);
                }
            }
        }
    }

    protected new void OnTriggerStay(Collider other)
    {
        
    }

    protected new void OnTriggerExit(Collider other)
    {

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        animator.SetTrigger("Suicide");
    //        isFlying = false;

    //        AbilityStatus playerAbil = other.GetComponentInChildren<AbilityStatus>();
    //        if (playerAbil != null)
    //        {
    //            playerAbil.AttackedBy(enemy.abilityStatus, damageScale);
    //        }
    //    }
    //    //else if (other.CompareTag("Ground"))
    //    //{
    //    //    animator.SetTrigger("Suicide");
    //    //    AbilityStatus playerAbil = other.GetComponentInChildren<AbilityStatus>();
    //    //    if (playerAbil != null)
    //    //    {
    //    //        playerAbil.AttackedBy(enemy.abilityStatus, damageScale);
    //    //    }
    //    //}

    //}
}
