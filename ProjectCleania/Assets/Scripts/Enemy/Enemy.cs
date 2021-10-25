using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    NavMeshAgent navMeshAgent;
    SkinnedMeshRenderer skinnedMeshRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        OnDead += Die;
    }

    private void Update()
    {
        if (abilityStatus.HP == 0 && !enemyStateMachine.CompareState(EnemyStateMachine.enumState.Dead))
        {
            Stunned(false, 0);
            OnDead();
        }
    }

    public void Stunned(bool isStunned, float stunnedTime)
    {
        if (isStunned)
        {
            StartCoroutine("StunnedFor", stunnedTime);
        }
        else
        {
            animator.speed = 1;
            navMeshAgent.enabled = true;
        }
    }

    IEnumerator StunnedFor(float time)
    {
        animator.speed = 0;
        navMeshAgent.enabled = false;
        yield return new WaitForSeconds(time);
        animator.speed = 1;
        navMeshAgent.enabled = true;
    }

    public void Die()
    {
        if (enemyStateMachine.CompareState(EnemyStateMachine.enumState.Dead)) return;

        // �׺���̼� Off
        navMeshAgent.enabled = false;

        // �浹ü ����
        TurnOffColliders();

        // ���� �������� ��ȯ
        enemyStateMachine.Transition(EnemyStateMachine.enumState.Dead);

        // ���� �ִϸ��̼� �ߵ�
        animator.SetTrigger("Die");

        // 3�� �Ŀ� ���� ����
        Invoke("TurnOffSkin", 3.0f);

        // 10�� �Ŀ� �ı�
        Destroy(gameObject, 10.0f);
    }

    void TurnOffColliders()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    void TurnOffSkin()
    {
        // ���� ����
        skinnedMeshRenderer.enabled = false;
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
