using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustySkillSuicide : EnemySkill
{
    float damageScale = 10;
    float angryDuration = 3;
    float skillTriggerPercentage = 0.5f;

    Collider bombCollider;

    bool isFlying = false;

    [SerializeField]
    DustySuicideSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        bombCollider = GetComponent<Collider>();
        if (bombCollider == null)
        {
            throw new System.Exception("DustySkillSuicide doesnt have bombCollider");
        }
        else
            bombCollider.enabled = false;
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
        skillTriggerPercentage = skillData.GetTriggerChance();
        angryDuration = skillData.GetAngryDuration();
    }

    private void Update()
    {
        if (isFlying)
        {
            // print("dist: " + Vector3.Distance(this.transform.position, enemyMove.TargetPosition));
            if (Vector3.Distance(this.transform.position, enemyMove.TargetPose) < 0.1f)
            {
                animator.SetTrigger("Suicide");
                isFlying = false;
            }
        }
    }

    public override bool AnimationActivate()
    {
        if (!(Random.Range(0f, 1f) < skillTriggerPercentage))
            return true;

        animator.SetBool("OnSkill", true);
        animator.SetTrigger("Angry");
        enemyMove.StopMoving(true); 

        StartCoroutine("AngryToFlyForward", angryDuration);

        return true;
    }

    IEnumerator AngryToFlyForward(float angryDuration)
    {
        yield return new WaitForSeconds(angryDuration);
        animator.SetTrigger("AngryToFlyForward");

        isFlying = true;
        enemyMove.StopMoving(false);
        if(enemyMove.TargetObject != null)
            enemyMove.SetOnlyChasePositionMode(true, enemyMove.TargetObject.transform.position);
    }

    public override void Activate()
    {
        bombCollider.enabled = true;
    }

    public override void Deactivate()
    {
        bombCollider.enabled = false;
        animator.SetBool("OnSkill", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Suicide");
            isFlying = false;

            AbilityStatus playerAbil = other.GetComponentInChildren<AbilityStatus>();
            if (playerAbil != null)
            {
                playerAbil.AttackedBy(enemy.abilityStatus, damageScale);
            }
        }
        //else if (other.CompareTag("Ground"))
        //{
        //    animator.SetTrigger("Suicide");
        //    AbilityStatus playerAbil = other.GetComponentInChildren<AbilityStatus>();
        //    if (playerAbil != null)
        //    {
        //        playerAbil.AttackedBy(enemy.abilityStatus, damageScale);
        //    }
        //}

    }
}
