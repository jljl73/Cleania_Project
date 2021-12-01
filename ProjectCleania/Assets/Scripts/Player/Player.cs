using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine;
    public PlayerMovement playerMove;
    public PlayerSkillManager playerSkillManager;
    public AbilityStatus abilityStatus;
    public Animator animator;

    public delegate void DelegateVoid();
    public event DelegateVoid OnLevelUp;
    public event DelegateVoid OnDead;
    public event DelegateVoid OnRevive;
    public event DelegateVoid OnVillageRevive;
    public UnityAction<bool, float> OnStunned;

    IEnumerator stopSkillDelay = new WaitForSecondsRealtime(0.5f);
    void Awake()
    {
        animator = GetComponent<Animator>();

        OnDead += Die;
        OnDead += playerSkillManager.DeactivateAllSkill;

        OnRevive += CloseDiePanel;
        OnVillageRevive += CloseDiePanel;

        OnStunned += playerMove.Stunned;
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
        OnRevive();
    }

    public void VillageRevive()
    {
        OnVillageRevive();
    }

    void Die()
    {
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.Die, 0);
        animator.SetTrigger("Die");
        stateMachine.Transition(StateMachine.enumState.Dead);

        Invoke("ShowDiePanel", 2f);
    }

    void ShowDiePanel()
    {
        GameManager.Instance.uiManager.ShowDiePanel(true);
    }

    void CloseDiePanel()
    {
        GameManager.Instance.uiManager.ShowDiePanel(false);
    }

    public void Move(Vector3 position)
    {
        if (playerMove.enabled)
            playerMove.Move(position);
    }

    public void PlaySkill(int id)
    {
        #region
        //// 부활 스킬일 경우
        //if (id == 1194)
        //    OnRevive();
        //else if (id == 1195)
        //    OnVillageRevive();
        #endregion
        playerSkillManager.PlaySkill(id);
    }

    public void StopSkill(int id)
    {
        playerSkillManager.StopSkill(id);
        StartCoroutine("StopSkillDelay", id);
    }
    IEnumerator StopSkillDelay(int id)
    {
        yield return stopSkillDelay;
        playerSkillManager.StopSkill(id);
    }
}
