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

    bool CheckSkillTriggerAvailable(int id)
    {
        int currentStateHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        if (currentStateHash == Animator.StringToHash("Dead"))
        {
            if (id == 1194 || id == 1195)
                return true;
        }

        if (currentStateHash == Animator.StringToHash("Idle") ||
            currentStateHash == Animator.StringToHash("Run"))
            return true;

        return false;
    }

    void BecomeDead()
    {
        // ���� �ִϸ��̼� ����
        animator.SetTrigger("Die");
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.Die, 0);

        Invoke("ShowDiePanel", 2f);
    }

    void ShowDiePanel() => GameManager.Instance.uiManager.ShowDiePanel(true);

    public void BecomeStunned()
    {
        // ���� �ִϸ��̼� ����
        animator.SetBool("Stunned", true);
    }

    public void OrderSkillID(int id)
    {
        if (!CheckSkillTriggerAvailable(id))
            return;

        //// ��ų ����
        animator.SetBool("Trigger" + id.ToString(), true);

        // ���콺 ���� �Ĵٺ�
        if (movementController.enabled && CheckMovable())
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