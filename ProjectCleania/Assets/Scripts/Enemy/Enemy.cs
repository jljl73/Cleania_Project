using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    Animator animator;

    // GameObject enemySpawner;
    // public GameObject EnemySpawner { get { return enemySpawner; } set { enemySpawner = value; } }

    [Header("Need Drag")]
    public AbilityStatus abilityStatus;
    public EnemySkillManager skillManager;
    public EnemyMove enemyMove;
    public EnemyStateMachine enemyStateMachine;

    public delegate void DelegateVoid();
    public event DelegateVoid OnDead;

    public UnityAction<bool, float> OnStunned;

    NavMeshAgent navMeshAgent;
    SkinnedMeshRenderer skinnedMeshRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (abilityStatus == null)
            throw new System.Exception("Enemy doesnt have abilityStatus");

        if (skillManager == null)
            throw new System.Exception("Enemy doesnt have skillManager");

        if (enemyMove == null)
            throw new System.Exception("Enemy doesnt have enemyMove");

        if (enemyStateMachine == null)
            throw new System.Exception("Enemy doesnt have enemyStateMachine");
    }

    private void Start()
    {
        OnDead += Die;
        OnDead += skillManager.DeactivateAllSkill;

        StartCoroutine("InvincibleFor", 2f);
    }

    private void Update()
    {
        if (abilityStatus.HP == 0 && !enemyStateMachine.CompareState(EnemyStateMachine.enumState.Dead))
        {
            // Stunned(false, 0);
            OnStunned(false, 0);
            OnDead();
        }
    }

    IEnumerator InvincibleFor(float time)
    {
        ActivateColliders(false);
        yield return new WaitForSeconds(time);
        ActivateColliders(true);
    }
    //public void Stunned(bool isStunned, float stunnedTime)
    //{
    //    if (isStunned)
    //    {
    //        StartCoroutine("StunnedFor", stunnedTime);
    //    }
    //    else
    //    {
    //        animator.speed = 1;
    //        navMeshAgent.enabled = true;
    //    }
    //}

    //IEnumerator StunnedFor(float time)
    //{
    //    animator.speed = 0;
    //    navMeshAgent.enabled = false;
    //    yield return new WaitForSeconds(time);
    //    animator.speed = 1;
    //    navMeshAgent.enabled = true;
    //}

    public void Die()
    {
        if (enemyStateMachine.CompareState(EnemyStateMachine.enumState.Dead)) return;

        // �׺���̼� Off
        navMeshAgent.enabled = false;

        // �浹ü ����
        ActivateColliders(false);

        // ���� �������� ��ȯ
        enemyStateMachine.Transition(EnemyStateMachine.enumState.Dead);

        // ���� �ִϸ��̼� �ߵ�
        animator.SetTrigger("Die");

        // 3�� �Ŀ� ���� ����
        Invoke("DeactivateSkin", 3.0f);

        // 10�� �Ŀ� �ı�
        Destroy(gameObject, 10.0f);
    }

    public void Revive()
    {
        // �׺���̼� On
        navMeshAgent.enabled = true;
    }

    void ActivateColliders(bool value = true)
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = value;
        }

        colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = value;
        }
    }

    void DeactivateSkin()
    {
        // ���� ����
        skinnedMeshRenderer.enabled = false;
    }

    void ActivateSkin()
    {
        // ���� ����
        skinnedMeshRenderer.enabled = true;
    }

    public void SetTarget(GameObject target)
    {
        enemyMove.SetTarget(target);
    }

    public void ReleaseTarget()
    {
        enemyMove.ReleaseTarget();
    }

    public void ActivateSkillEffect(AnimationEvent myEvent)
    {
        skillManager.ActivateSkillEffect(myEvent);
    }

    public void DeactivateSkillEffect(AnimationEvent myEvent)
    {
        skillManager.DeactivateSkillEffect(myEvent);
    }

    // Listener
    public void ActivateSkill(int type)
    {
        skillManager.ActivateSkill(type);
    }

    public void DeactivateSkill(int type)
    {
        skillManager.DeactivateSkill(type);
    }

}
