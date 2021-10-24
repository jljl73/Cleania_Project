using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustySkillSuicide : EnemySkill
{
    float damageScale = 10;

    float angryDuration = 3;
    Collider bombCollider;

    float skillTriggerPercentage = 0.5f;

    bool isFlying = false;

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

    private void Update()
    {
        if (isFlying)
        {
            // print("dist: " + Vector3.Distance(this.transform.position, enemyMove.TargetPosition));
            if (Vector3.Distance(this.transform.position, enemyMove.TargetPosition) < 0.1f)
            {
                animator.SetTrigger("Suicide");
                isFlying = false;
            }
        }
    }

    public override void AnimationActivate()
    {
        if (!(Random.Range(0f, 1f) < skillTriggerPercentage))
            return;

        animator.SetBool("OnSkill", true);
        animator.SetTrigger("Angry");
        enemyMove.StopMoving(true);

        StartCoroutine("AngryToFlyForward", angryDuration);
    }

    IEnumerator AngryToFlyForward(float angryDuration)
    {
        yield return new WaitForSeconds(angryDuration);
        animator.SetTrigger("AngryToFlyForward");

        isFlying = true;
        enemyMove.StopMoving(false);
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
