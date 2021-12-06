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
    }
    void Update()
    {
        currentStateHash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;

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
        if (abilityStatus.HP == 0 && !(Animator.StringToHash("Dead") == currentStateHash) && !(Animator.StringToHash("Skill 1194") == currentStateHash))
            return true;
        else
            return false;
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

    bool CheckSkillTriggerAvailable(int id)
    {
        if (Animator.StringToHash("Dead") == currentStateHash)
        {
            if (id == 1194 || id == 1195)
                return true;
        }

        if (Animator.StringToHash("Idle") == currentStateHash ||
            Animator.StringToHash("Run") == currentStateHash)
            return true;

        return false;
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
        // 죽음 애니메이션 실행
        animator.SetTrigger("Die");
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.Die, 0);

        Invoke("ShowDiePanel", 2f);
    }

    public void BecomeStunned()
    {
        // 스턴 애니메이션 실행
        animator.SetBool("Stunned", true);
    }

    public bool OrderSkillID(int id)
    {
        if (!CheckIfSkillAvailable(id))
            return true;

        if (!CheckSkillTriggerAvailable(id))
            return false;

        //// 스킬 실행
        //skillController.AnimationActivate(id);
        
        // 
        animator.SetBool("Trigger" + id.ToString(), true);

        // 마우스 방향 쳐다봄
        if (movementController.enabled && CheckMovable())
            movementController.ImmediateLookAtMouse();

        return true;
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

    public void OnSetUnmovable() => animator.SetBool("Movable", false);

    public void OnSetMovable() => animator.SetBool("Movabl e", true);

    public void OnFullHP() => abilityStatus.FullHP();
    public void OnFullMP() => abilityStatus.FullHP();
    void OnPlayRoll() => movementController.SpeedUp(10);
    void OnEndRoll() => movementController.SpeedUp(6.8f);
    void ShowDiePanel() => GameManager.Instance.uiManager.ShowDiePanel(true);
    void CloseDiePanel() => GameManager.Instance.uiManager.ShowDiePanel(false);

}