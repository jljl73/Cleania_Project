using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // public
    public StateMachine stateMachine;
    public PlayerMovement playerMove;
    public PlayerSkillManager playerSkillManager;
    public AbilityStatus abilityStatus;
    public Animator animator;

    public delegate void DelegateVoid();
    public event DelegateVoid OnLevelUp;
    public event DelegateVoid OnDead;
    public UnityAction<bool, float> OnStunned;

    void Awake()
    {
        animator = GetComponent<Animator>();

        OnDead += RunDieAnimation;
        OnDead += playerSkillManager.DeactivateAllSkill;

        OnStunned += playerMove.Stunned;
        OnStunned += playerSkillManager.Stunned;
    }

    private void Update()
    {
        if (abilityStatus.HP == 0)
        {
            OnDead();
        }
    }

    public void Revive()
    {
        animator.SetTrigger("Revive");
    }

    void RunDieAnimation()
    {
        animator.SetTrigger("Die");
    }

    public void Move(Vector3 position)
    {
        playerMove.Move(position);
    }

    public void StopMoving()
    {
        playerMove.StopMoving();
    }


    public void PlaySkill(int id)
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            && !animator.IsInTransition(0))
        {
            playerSkillManager.PlaySkill(id);
        }
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
