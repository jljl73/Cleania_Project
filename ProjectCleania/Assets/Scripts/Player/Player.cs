using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // public
    public StateMachine stateMachine;
    public PlayerMovement playerMove;
    public TestPlayerMove PlayerMoveWithoutNav;
    public PlayerSkillManager playerSkillManager;
    public AbilityStatus abilityStatus;
    public Animator animator;

    public delegate void DelegateVoid();
    public event DelegateVoid OnLevelUp;
    public event DelegateVoid OnDead;
    public event DelegateVoid OnRevive;
    public UnityAction<bool, float> OnStunned;

    void Awake()
    {
        animator = GetComponent<Animator>();

        OnDead += Die;
        OnDead += playerSkillManager.DeactivateAllSkill;

        OnRevive += Revive;

        OnStunned += playerMove.Stunned;
        OnStunned += PlayerMoveWithoutNav.Stunned;
        OnStunned += playerSkillManager.Stunned;
    }

    private void Update()
    {
        if (abilityStatus.HP == 0 && !stateMachine.CompareState(StateMachine.enumState.Dead))
        {
            OnDead();
        }
    }

    public void Revive()
    {
        //animator.SetTrigger("Revive");
        // stateMachine.ResetState();
        abilityStatus.FullHP();
        abilityStatus.FullMP();
    }

    void Die()
    {
        animator.SetTrigger("Die");
        stateMachine.Transition(StateMachine.enumState.Dead);
    }

    public void Move(Vector3 position)
    {
        if (playerMove.enabled)
            playerMove.Move(position);
        else
            PlayerMoveWithoutNav.Move(position);
    }

    public void StopMoving()
    {
        playerMove.StopMoving();
    }


    public void PlaySkill(int id)
    {
        // ��Ȱ ��ų�� ���
        if (id == 1190)
            OnRevive();

        //if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        //    && !animator.IsInTransition(0))
        //{
        //    playerSkillManager.PlaySkill(id);
        //}
        playerSkillManager.PlaySkill(id);
    }

    public void StopSkill(int id)
    {
        playerSkillManager.StopSkill(id);
    }

    //public void ActivateSkill(AnimationEvent myEvent)
    //{
    //    playerSkillManager.ActivateSkill(myEvent);
    //}

    //public void DeactivateSkill(int index)
    //{
    //    print("deactivateSkill");
    //    playerSkillManager.DeactivateSkill(index);
    //}

    //public void activateskilleffect(int index)
    //{
    //    print("ActivateSkillEffect!");
    //    playerSkillManager.ActivateSkillEffect(index);
    //}

    //public void ActivateSkillEffect(AnimationEvent myEvent)
    //{
    //    playerSkillManager.ActivateSkillEffect(myEvent);
    //}

    //public void DeactivateSkillEffect(AnimationEvent myEvent)
    //{
    //    playerSkillManager.DeactivateSkillEffect(myEvent);
    //}

    //public void DeactivateSkillEffect(int index)
    //{
    //    print("DeactivateSkillEffect!");
    //    playerSkillManager.DeactivateSkillEffect(index);
    //    StopMoving();
    //}
}
