using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 기능 : 현재 상황에 따라 자신의 상태를 선택하고 그에 따라 행동함
public class EnemyAI : MonoBehaviour
{
    // 상태 : Idle, Chase, Attack, Hurt, Die
    // Idle : Idle 애니메이션을 실행하며 움직이지 않는다. 
    //      - 일정 거리 주변을 감시하며 플레이어가 있는지 확인한다. 
    //      - 일정 거리 내에 플레이어가 있으면 chase로 상태 이동
    // Chase : 찾은 플레이어를 추적한다. 
    //      - 공격할 수 있는 범위 내에 플레이어가 들어오면 Attack으로 상태 이동(원거리 공격 범위, 근거리 공격 범위(콜라이더가 좋을 듯))
    //      - 시야 내에서 플레이어가 사라지면 다시 Idle 상태로 돌아간다
    //      - (선택) chase 시, 사용할 수 있는 추가 이동기(빠른 추적 등)은 이후 확장 구현할 수 있게 고려
    // Attack : 플레이어를 일정 주기로 공격한다
    //      - 원거리 / 근거리 공격 판단 알고리즘은 추후 변경
    //      - 원거리에 플레이어 있을 경우(원거리 공격 가능 시) 원거리 공격 실행. 근거리에 있을 경우(근거리 공격 가능 시) 근거리 공격 실행
    // Hurt : 공격을 맞으면 hurt 애니메이션 실행 후 Idle 복귀
    // Die : 죽으면, 죽음 애니메이션 실행 후 네비 매시 에이전트 Off


    // Additional idea
    // - 근거리에 있을 때 bIsPlayerNear = true 해서, 원거리 공격 안하게 가능
    // - 원거리 공격 가능 범위일 때, 추적 위치는 현재 위치로 고정

    // 구현 순서
    // 1. 일정 범위 내에 오면 Idle -> chase, 단순 추적 후, 근거리 공격 범위 내에 오면 Attack, 공격 맞으면 hurt, 죽으면 Die  // 9월 10일 금
    // 2. AbilityStatus 적용하여 간단하게 Hurt -> Die 구현                                                              // 9월 11일 일
    // 3. 원거리 공격 범위 설정 & 원거리, 근거리 공격 알고리즘 설정                                                       // 9월 11, 12일 일 월


    private NavMeshAgent enemyNavMeshAgent; // 적 네비 메시 에이전트 컴포넌트 
    private Animator enemyAnimator;         // 적 애니메이터

    public float searchDistance = 3f;               // 사주 경계 범위
    private Transform targetTransform = null;       // 목표뮬 transform

    private Vector3 initialPosition;                // 초기 위치
    private Vector3 initialDirection;               // 초기 방향
    private Vector3 targetPosition;                 // 목표 위치

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
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        enemyNavMeshAgent.enabled = true;       // 네비게이션 활성화
        enemyNavMeshAgent.isStopped = false;
        initialPosition = transform.position;   // 초기 위치 설정
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
        if (bIsDead) return;

        // 애니메이션 실행
        enemyAnimator.SetFloat("Speed", enemyNavMeshAgent.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (bIsDead) return;

        // 오브젝트 찾기
        FindObj();

        // 네비게이션 실행
        if (enemyNavMeshAgent.enabled == true)
            enemyNavMeshAgent.SetDestination(targetPosition);
    }

    void FindObj()
    {
        if (hasTarget)
        {
            // 타겟이 과의 거리에 따라 추적 판단
            if (Vector3.Distance(transform.position, targetTransform.position) > searchDistance)
                targetTransform = null;
            else
            {
                if (bIsAttaking) return;
                targetPosition = targetTransform.position;
            }
        }
        else
        {
            // 적 위치에서 searchDistance 크기의 구 안에 Player가 있는지 확인
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchDistance);
            bool targetFound = false;
            foreach (Collider collider in colliders)
            {
                if (collider.transform.CompareTag("Player"))
                {
                    targetTransform = collider.transform;
                    targetFound = true;
                    break;
                }
            }

            // 구 안에 적 없으면, 현재 위치가 목표 위치
            if (targetFound)
                return;
            else
                targetPosition = initialPosition;
        }

    }

    public void Die()
    {
        bIsDead = true;

        // 죽음 애니메이션 활성화
        enemyAnimator.SetTrigger("Die");

        // 네비게이션 비활성화
        enemyNavMeshAgent.isStopped = true;
        enemyNavMeshAgent.enabled = false;
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        // 부활 기능. 죽은 곳에서 5초 후 부활. 없애도 됩니다:)
        StartCoroutine("Revival");
    }

    IEnumerator Revival()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }

    private void AccelerateRotation()
    {
        Vector3 rotateForward = Vector3.zero;

        // 타겟 유무에 따른 회전 벡터 결정
        if (targetTransform != null)
            rotateForward = Vector3.Normalize(targetTransform.position - transform.position);
        else
            rotateForward = Vector3.Normalize(targetPosition - transform.position);

        // 목표 회전 벡터 결정
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        // 회전

        transform.LookAt(this.transform.position + rotateForward);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            bIsAttaking = true;

            // 콜라이더가 플레이어랑 부딛히면 멈춤
            targetPosition = transform.position;

            // 회전 가속
            AccelerateRotation();

            // 공격 주기가 지났을 시 공격
            if (Time.time > lastAttackTime + attackPeriod)
            {
                enemyAnimator.SetTrigger("SlashUp2DownAttack");
                lastAttackTime = Time.time;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            bIsAttaking = false;
        }
    }
}