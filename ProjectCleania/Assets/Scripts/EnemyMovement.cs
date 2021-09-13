using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent enemyNavMeshAgent;         // 적 네비 메시 에이전트 컴포넌트 
    private Animator enemyAnimator;                 // 적 애니메이터
    private EnemyStateMachine enemyStateMachine;    // 상태 머신

    public float SearchNearDist = 3f;              // 근거리 공격 경계 범위
    public float SearchFarDist = 7f;               // 원거리 공격 경계 범위
    public float SearchChaseDist = 10f;             // 사주 경계 범위

    private Transform targetTransform = null;       // 목표뮬 transform

    private Vector3 initialPosition;                // 초기 위치
    private Vector3 initialDirection;               // 초기 방향
    private Vector3 targetPosition;                 // 목표 위치
    private float initialSpeed;                     // 초기 속속도

    public float attackPeriod = 1f;             // 공격 주기
    private float lastAttackTime;               // 마지막 공격 시점

    private bool bIsAttaking = false;           // 공격 상태
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
        // 컴포넌트 초기화
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        print("OnEnable called");
        enemyNavMeshAgent.enabled = true;       // 네비게이션 활성화
        enemyNavMeshAgent.isStopped = false;
        initialPosition = transform.position;   // 초기 위치 설정
        initialSpeed = enemyNavMeshAgent.speed; // 초기 속도 설정
        initialDirection = transform.forward;   // 초기 방향 설정
        lastAttackTime = 0f;                    // 마지막 공격 시점 초기화
        bIsAttaking = false;                    // 공격 상태 초기화
        bIsDead = false;                        // 생존 상태 초기화

        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;    // 콜라이더 활성화
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // 근거리 공격 가능 범위에 플레이어 있으면 타겟 설정, 따라감
        if (FindEnemySetMotion(SearchNearDist, EnemyStateMachine.EnumState.NormalChaseAttack, initialSpeed))
        {
        }
        // 원거리 공격 가능 범위에 플레이어 있으면 타겟 설정, 방향만 따라감
        else if (FindEnemySetMotion(SearchFarDist, EnemyStateMachine.EnumState.DirectionChase, 0.1f))
        {
        }
        // 사주 경계 범위에 플레이어 있으면 타겟 설정, 따라감
        else if (FindEnemySetMotion(SearchChaseDist, EnemyStateMachine.EnumState.NormalChase, initialSpeed))
        {
        }
        // 검색 범위에 없으면 원위치 + Idle
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
        targetTransform = null; // 타겟 유무 초기화
        Collider[] colliders = Physics.OverlapSphere(transform.position, dist);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                isFound = true;                         // 찾은지 여부

                // Set Motion
                enemyStateMachine.Transition(state);    // 상태 변화
                enemyNavMeshAgent.speed = speed;        // 속도 변화
                targetTransform = collider.transform;   // 타겟 설정
                targetPosition = targetTransform.position;

                // Set Animation
                enemyAnimator.SetBool("OnChase", true);
                break;
            }
        }
        return isFound;
    }
}
