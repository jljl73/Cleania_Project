using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public
    public StateMachine stateMachine;
    public TestPlayerMove playerMove;
    public PlayerSkillManager playerSkillManager;
    public AbilityStatus abilityStatus;
    public Animator animator;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 position)
    {
        playerMove.Move(position);
    }

    public void StopMoving()
    {
        playerMove.StopMoving();
    }

    public void DoSkill(int index)
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            && !animator.IsInTransition(0))
        {
            playerSkillManager.InputListener(index);
        }
    }

    public void ActivateSkill(int index)
    {
        playerSkillManager.ActivateSkill(index);
    }

    public void DeactivateSkill(int index)
    {
        playerSkillManager.DeactivateSkill(index);
        StopMoving();
    }

}
