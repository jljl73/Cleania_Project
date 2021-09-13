using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent enemyNavMeshAgent;         // �� �׺� �޽� ������Ʈ ������Ʈ 
    private Animator enemyAnimator;                 // �� �ִϸ�����
    private EnemyStateMachine enemyStateMachine;    // ���� �ӽ�

    public float SearchNearDist = 3f;              // �ٰŸ� ���� ��� ����
    public float SearchFarDist = 7f;               // ���Ÿ� ���� ��� ����
    public float SearchChaseDist = 10f;             // ���� ��� ����

    private Transform targetTransform = null;       // ��ǥ�� transform

    private Vector3 initialPosition;                // �ʱ� ��ġ
    private Vector3 initialDirection;               // �ʱ� ����
    private Vector3 targetPosition;                 // ��ǥ ��ġ
    private float initialSpeed;                     // �ʱ� �Ӽӵ�

    public float attackPeriod = 1f;             // ���� �ֱ�
    private float lastAttackTime;               // ������ ���� ����

    private bool bIsAttaking = false;           // ���� ����
    private bool bIsDead = true;

    private bool hasTarget
    {
        get
        {
            if (targetTransform != null)
                return true;
            else
                return false;
        }
    }

    private void Awake()
    {
        // ������Ʈ �ʱ�ȭ
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        print("OnEnable called");
        enemyNavMeshAgent.enabled = true;       // �׺���̼� Ȱ��ȭ
        enemyNavMeshAgent.isStopped = false;
        initialPosition = transform.position;   // �ʱ� ��ġ ����
        initialSpeed = enemyNavMeshAgent.speed; // �ʱ� �ӵ� ����
        initialDirection = transform.forward;   // �ʱ� ���� ����
        lastAttackTime = 0f;                    // ������ ���� ���� �ʱ�ȭ
        bIsAttaking = false;                    // ���� ���� �ʱ�ȭ
        bIsDead = false;                        // ���� ���� �ʱ�ȭ

        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;    // �ݶ��̴� Ȱ��ȭ
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // �ٰŸ� ���� ���� ������ �÷��̾� ������ Ÿ�� ����, ����
        if (FindEnemySetMotion(SearchNearDist, EnemyStateMachine.EnumState.NormalChaseAttack, initialSpeed))
        {
        }
        // ���Ÿ� ���� ���� ������ �÷��̾� ������ Ÿ�� ����, ���⸸ ����
        else if (FindEnemySetMotion(SearchFarDist, EnemyStateMachine.EnumState.DirectionChase, 0.1f))
        {
        }
        // ���� ��� ������ �÷��̾� ������ Ÿ�� ����, ����
        else if (FindEnemySetMotion(SearchChaseDist, EnemyStateMachine.EnumState.NormalChase, initialSpeed))
        {
        }
        // �˻� ������ ������ ����ġ + Idle
        else
        {
            enemyStateMachine.Transition(EnemyStateMachine.EnumState.Idle);
            targetPosition = initialPosition;
            enemyNavMeshAgent.updatePosition = true;

            // Set Animation
            enemyAnimator.SetBool("OnChase", false);
        }

        print("targetPosition: " + targetPosition);

        enemyNavMeshAgent.SetDestination(targetPosition);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, SearchChaseDist);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, SearchFarDist);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SearchNearDist);
    }

    bool FindEnemySetMotion(float dist ,EnemyStateMachine.EnumState state, float speed)
    {
        bool isFound = false;
        targetTransform = null; // Ÿ�� ���� �ʱ�ȭ
        Collider[] colliders = Physics.OverlapSphere(transform.position, dist);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                isFound = true;                         // ã���� ����

                // Set Motion
                enemyStateMachine.Transition(state);    // ���� ��ȭ
                enemyNavMeshAgent.speed = speed;        // �ӵ� ��ȭ
                targetTransform = collider.transform;   // Ÿ�� ����
                targetPosition = targetTransform.position;

                // Set Animation
                enemyAnimator.SetBool("OnChase", true);
                break;
            }
        }
        return isFound;
    }
}
