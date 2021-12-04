using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    AbilityStatus abilityStatus;
    NavMeshAgent navMeshAgent;
    Animator animator;

    [SerializeField]
    PlayerSkillController skillController;
    [SerializeField]
    PlayerMovementController movementController;

    void Awake()
    {
        abilityStatus = GetComponent<AbilityStatus>();
        if (abilityStatus == null)
            throw new System.Exception("PlayerController doesnt have AbilityStatus");

        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            throw new System.Exception("PlayerController doesnt have NavMeshAgent");

        animator = GetComponent<Animator>();
        if (animator == null)
            throw new System.Exception("PlayerController doesnt have Animator");
    }

    private void Update()
    {
        if (CheckDeadable())
        {
            BecomeDead();
            return;
        }

        if (!CheckMovable())
            return;

        // 움직임 애니메이션 업데이트
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    bool CheckDeadable()
    {
        if (abilityStatus.HP == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            return true;
        else
            return false;
    }

    bool CheckMovable()
    {
        int currentStateHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        if (currentStateHash == Animator.StringToHash("Idle") ||
            currentStateHash == Animator.StringToHash("Run") ||
            currentStateHash == Animator.StringToHash("Skill 1102"))
        {
            navMeshAgent.isStopped = false;
            return true;
        }

        navMeshAgent.isStopped = true;
        return false;
    }

    void BecomeDead()
    {
        // 죽음 애니메이션 실행
        animator.SetTrigger("Die");
    }

    public void BecomeStunned()
    {
        // 스턴 애니메이션 실행
        animator.SetBool("Stunned", true);
    }

    public void OrderSkillID(int id)
    {
        // 스킬이 사용 가능한지 확인
        //if (!skillController.IsSpecificSkillAvailable(id))
        //    return;

        //int skillId = animator.stri

        //// 스킬 실행
        animator.SetBool("Trigger" + id.ToString(), true);

        // 마우스 방향 쳐다봄
        if (movementController.enabled)
            movementController.ImmediateLookAtMouse();
    }

    public void OrderSkillStop(int id)
    {
        animator.SetBool("Trigger" + id.ToString(), false);
    }

    public void OrderMovementTo(Vector3 mousePosition)
    {
        if (CheckMovable())
            movementController.Move(mousePosition);
    }
}