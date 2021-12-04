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

        // ������ �ִϸ��̼� ������Ʈ
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    bool CheckDeadable()
    {
        if (abilityStatus.HP == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            return true;
        else
            return false;
    }

    bool CheckState()
    {

        return true;
    }

    void BecomeDead()
    {
        // ���� �ִϸ��̼� ����
        animator.SetTrigger("Die");
    }

    public void BecomeStunned()
    {
        // ���� �ִϸ��̼� ����
        animator.SetBool("Stunned", true);
    }

    public void OrderSkillID(int id)
    {
        // ��ų�� ��� �������� Ȯ��
        //if (!skillController.IsSpecificSkillAvailable(id))
        //    return;

        //int skillId = animator.stri

        //// ��ų ����
        animator.SetBool("Trigger" + id.ToString(), true);

        // ���콺 ���� �Ĵٺ�
        if (movementController.enabled)
            movementController.ImmediateLookAtMouse();
    }

    public void OrderMovementTo(Vector3 mousePosition)
    {
        movementController.Move(mousePosition);
    }
}