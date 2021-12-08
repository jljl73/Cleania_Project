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
    Buffable buffable;
    StatusAilment statusAilment;
    Collider hitBoxCollider;

    [SerializeField]
    PlayerSkillController skillController;
    [SerializeField]
    PlayerMovementController movementController;

    int currentStateHash;

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

        buffable = GetComponent<Buffable>();
        if (buffable == null)
            throw new System.Exception("PlayerController doesnt have Buffable");

        statusAilment = GetComponent<StatusAilment>();
        if (statusAilment == null)
            throw new System.Exception("PlayerController doesnt have StatusAilment");

        hitBoxCollider = GetComponent<Collider>();
        if (hitBoxCollider == null)
            throw new System.Exception("PlayerController doesnt have BoxCollider");
    }

    void Update()
    {
        currentStateHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;

        if (CheckDeadable())
        {
            BecomeDead();
            return;
        }

        CheckStatusAilment();

        if (!CheckMovable())
            return;

        // ������ �ִϸ��̼� ������Ʈ
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    bool CheckDeadable()
    {
        if (abilityStatus.HP == 0 && !(Animator.StringToHash("Dead") == currentStateHash) && !(Animator.StringToHash("Skill 1194") == currentStateHash))
            return true;
        else
            return false;
    }
    void CheckStatusAilment()
    {
        if (statusAilment[StatusAilment.BehaviorRestrictionType.Restraint] > 0)
            animator.SetBool("Restraint", true);
        else
            animator.SetBool("Restraint", false);

        if (statusAilment[StatusAilment.BehaviorRestrictionType.Stun] > 0)
            animator.SetBool("Stunned", true);
        else
            animator.SetBool("Stunned", false);

        if (statusAilment[StatusAilment.BehaviorRestrictionType.Silence] > 0)
        {
            if (!animator.GetBool("Silenced"))
                skillController.StopAllSkill();
            animator.SetBool("Silenced", true);
        }
        else
            animator.SetBool("Silenced", false);

        if (statusAilment[StatusAilment.BehaviorRestrictionType.Dark] > 0)
            animator.SetBool("Darked", true);
        else
            animator.SetBool("Darked", false);
    }

    bool CheckMovable()
    {
        if (Animator.StringToHash("Dead") == currentStateHash || !animator.GetBool("Movable"))
        {
            navMeshAgent.isStopped = true;
            return false;
        }

        navMeshAgent.isStopped = false;
        return true;
    }

    bool CheckSkillAnimationAvailable(int id)
    {
        bool result = false;

        // ����ó��: ���� ����
        if (Animator.StringToHash("Dead") == currentStateHash)
        {
            if (id == 1194 || id == 1195)
                return true;
            else
                return false;
        }

        // ���̵� & �޸��� ���¸� ����
        if (Animator.StringToHash("Idle") == currentStateHash ||
            Animator.StringToHash("Run") == currentStateHash)
            result = true;

        // ��ų Ʈ���� ���������� �����x
        if (animator.GetBool("Trigger" + id.ToString()))
            result = false;

        // ���º� ��ų ���� ���� ����
        switch (id)
        {
            case 1101:
                break;
            default:
                // if (animator.GetBool("Silenced"))
                if (statusAilment[StatusAilment.BehaviorRestrictionType.Silence] > 0)
                    return false;
                break;
        }
        
        return result;
    }

    bool CheckIfSkillAvailable(int id)
    {
        if (!skillController.IsSpecificSkillAvailable(id))
            return false;

        if (abilityStatus.ConsumeMP(skillController.GetMpValue(id)))
            return true;
        else
            return false;
    }

    void BecomeDead()
    {
        // ���� �ִϸ��̼� ����
        animator.SetTrigger("Die");
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.Die, 0);

        Invoke("ShowDiePanel", 2f);
    }

    public void BecomeStunned()
    {
        // ���� �ִϸ��̼� ����
        animator.SetBool("Stunned", true);
    }

    public bool OrderSkillID(int id)
    {
        // �ִϸ��̼� ���� ���� ����
        if (!CheckSkillAnimationAvailable(id))
            return false;

        // ��ų ���� ���� + ��Ÿ�� + MP �Ҹ�
        if (!CheckIfSkillAvailable(id))
            return true;

        // ��ų �ӵ� ����
        animator.SetFloat("Skill " + id.ToString() + " multiplier", skillController.GetSkillMultiplier(id));

        // ��ų �ִϸ��̼� ����
        if (CheckIfSkillAvailableByInnerLogic(id))
            animator.SetBool("Trigger" + id.ToString(), true);

        // ���콺 ���� �Ĵٺ�
        if (movementController.enabled && CheckMovable())
            movementController.ImmediateLookAtMouse();

        return true;
    }

    bool CheckIfSkillAvailableByInnerLogic(int id)
    {
        switch (id)
        {
            case 1199:
                if (!skillController.AnimationActivate(id))
                {
                    skillController.ResetSkill(id);
                    skillController.StopSkill(id);
                    return false;
                }
                else
                    return true;
            default:
                // ��ų ���� ������ �ִϸ��̼� ���� ���� ���¸�, ��Ÿ�� �ʱ�ȭ
                if (skillController.AnimationActivate(id))
                {
                    skillController.ResetSkill(id);
                    return true;
                }
                else
                    return false;
        }
    }

    public void OrderSkillStop(int id)
    {
        animator.SetBool("Trigger" + id.ToString(), false);
    }

    public void OrderMovementTo(Vector3 mousePosition)
    {
        if (CheckMovable())
        {
            movementController.Move(mousePosition);
        }
    }

    public void Pushed(Vector3 force)
    {
        movementController.AddForce(force);
    }

    public void Pulled(bool value, Vector3 origin)
    {
        movementController.Pulled(value, origin);
    }
    public void Stunned(bool value, float duration)
    {
        movementController.Stunned(value, duration);
    }
    public void OnSetUnmovable() => animator.SetBool("Movable", false);

    public void OnSetMovable() => animator.SetBool("Movabl e", true);

    public void OnHitColliderOn() => hitBoxCollider.enabled = true;
    public void OnHitColliderOff() => hitBoxCollider.enabled = false;
    public void OnHitColliderOnDelay(float duration) => Invoke("OnHitColliderOn", duration);
    public void OnFullHP() => abilityStatus.FullHP();
    public void OnFullMP() => abilityStatus.FullHP();
    void OnPlayRoll() => movementController.SpeedUp(10);
    void OnEndRoll() => movementController.SpeedUp(6.8f);
    void ShowDiePanel() => GameManager.Instance.uiManager.ShowDiePanel(true);
    void CloseDiePanel() => GameManager.Instance.uiManager.ShowDiePanel(false);

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }

}