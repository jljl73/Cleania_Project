using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TestPlayerMove : MonoBehaviour
{
    public float rotateCoef = 2f;

    private Animator playerAnimator;            // 애니메이터 컴포넌트
    private NavMeshAgent playerNavMeshAgent;    // path 계산 컴포턴트
    private Rigidbody playerRigidbody;          // 리지드바디 컴포넌트

    private BoxCollider attackBoxCollider;      // 공격 시 쓰이는 박스 콜라이더

    private GameObject targetObj;               // 공격 대상
    private Vector3 targetPose;                 // 목표 위치

    private NavMeshPath path;                   // 목표까지 path
    private int currentPathIdx;                 // path 내 현재 목표 지점

    private float distanceBetweenTargetObj = 1f;   // 공격 시, 목표 위치 얼마 앞에서 멈출 것인가

    //private bool isAttackPlaying;
    //bool isAttacking;

    public PlayerSkillL skillL;
    public bool bAttacking = false;
    bool bChasing = false;
    public StateMachine playerStateMachine;


    private void Awake()
    {
        // 컴포넌트 불러오기
        playerAnimator = GetComponent<Animator>();
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        //attackBoxCollider = GetComponent<BoxCollider>();
        //playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // 설정 초기화
        //attackBoxCollider.enabled = false;  // 공격 콜라이더 Off
        targetPose = transform.position;    // 목표 위치는 현재 위치
                                            //isAttackPlaying = false;            // 공격 애니메이션 실행 중 여부 
    }

    void Update()
    {
        //    isAttackPlaying = false;
        //if (isAttackPlaying)
        //    return;
        Move();
        AccelerateRotation();

        // 애니메이션 업데이트
        //playerAnimator.SetFloat("Speed", playerNavMeshAgent.velocity.magnitude);
    }

    private void ActivateNavigation()
    {
        if (playerStateMachine.State == StateMachine.enumState.Attacking)
        {
            targetPose = transform.position;
        }

        if (Input.GetMouseButton(0))// 누르고 있어도
        {
            if (playerStateMachine.State == StateMachine.enumState.Idle || playerStateMachine.State == StateMachine.enumState.Chasing)
            {
                MoveToPosition();
                Targetting();
            }
            
        }
        if (Input.GetMouseButton(1))
        {
            if (playerStateMachine.State == StateMachine.enumState.MoveAttack)
            {
                MoveToPosition();
            }
        }

        if (playerStateMachine.State != StateMachine.enumState.Chasing)
        {
            playerNavMeshAgent.SetDestination(targetPose);
        }
        else
            playerNavMeshAgent.SetDestination(targetObj.transform.position);
    }

    private void AccelerateRotation()
    {
        Vector3 rotateForward; //= Vector3.zero;

        // 타겟 유무에 따른 회전 벡터 결정
        if (targetObj != null)
        {
            rotateForward = Vector3.Normalize(targetObj.transform.position - transform.position);
            //rotateForward = targetObj.transform.position - transform.position;
        }
        else
        {
            rotateForward = Vector3.Normalize(targetPose - transform.position);
            //rotateForward = new Vector3(targetPose.x, transform.position.y, targetPose.z);
        }

        // 목표 회전 벡터 결정
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            rotateCoef * 180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        // 회전
        transform.LookAt(this.transform.position + limit);
    }


    public void MoveToPosition()
    {
        int layerMask = 0;
        layerMask = 1 << 5 | 1 << 6 | 1 << 7;

        if (EventSystem.current.IsPointerOverGameObject(-1)) return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 500.0f, layerMask))
        {
            Debug.Log(hit.collider.name + " " + hit.collider.tag);
            if(hit.collider.tag == "Ground")
            {
                targetPose = hit.point;
            }
        }
    }
    
    public void JumpForward(float dist)
    {
        targetPose = transform.position + transform.forward * dist;
    }

    void Targetting()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bChasing = false;
        // 수정 //
        targetObj = null;

        // --- //
        if (Physics.Raycast(ray, out raycastHit))
        {
            //Debug.Log(raycastHit.transform.tag);
            if (raycastHit.transform.CompareTag("Enemy"))
            {
                targetObj = raycastHit.transform.gameObject;
                bChasing = true;
                // 수정 //
                // playerStateMachine.Transition(StateMachine.enumState.Chasing);
                // --- //
            }
        }

        // 수정 //
        if (bChasing)
            playerStateMachine.Transition(StateMachine.enumState.Chasing);
        else
            playerStateMachine.Transition(StateMachine.enumState.Idle);
        // --- //
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerStateMachine.State == StateMachine.enumState.Chasing)
        // 데미지 주기, 공격 콜라이더만 trigger 설정됨.
        {
            AccelerateRotation();

            //print(other.transform.name + "Collision");
            //transform.LookAt(targetObj.transform);
            targetPose = transform.position;
            skillL.AnimationActivate();
        }
    }


    void Move()
    {
        if (Input.GetMouseButton(0))// 누르고 있어도
        {
            if (playerStateMachine.State == StateMachine.enumState.Idle || playerStateMachine.State == StateMachine.enumState.Chasing)
            {
                MoveToPosition();
                Targetting();
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (playerStateMachine.State == StateMachine.enumState.MoveAttack)
                MoveToPosition();
        }


        if (Vector3.Distance(targetPose, transform.position) < 0.01f)
        {
            playerAnimator.SetFloat("Speed", 0);
            return;
        }

        transform.localPosition = Vector3.MoveTowards(transform.position, targetPose, 5 * Time.deltaTime);
        playerAnimator.SetFloat("Speed", 10 * Time.deltaTime);
        //if (playerStateMachine.State != StateMachine.enumState.Chasing)
    }
}
