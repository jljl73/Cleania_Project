using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float rotateCoef = 2f;

    private Animator playerAnimator;            // 애니메이터 컴포넌트
    private NavMeshAgent playerNavMeshAgent;    // path 계산 컴포턴트
    private Rigidbody playerRigidbody;          // 리지드바디 컴포넌트

    private BoxCollider attackBoxCollider;      // 공격 시 쓰이는 박스 콜라이더

    private GameObject targetObj;               // 공격 대상
    private Vector3 targetPose;                 // 목표 위치

    private RaycastHit[] rayCastHits;

    private float distanceBetweenTargetObj = 1f;   // 공격 시, 목표 위치 얼마 앞에서 멈출 것인가

    private bool isAttackPlaying;

    private void Awake()
    {
        // 컴포넌트 불러오기
        playerAnimator = GetComponent<Animator>();
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        attackBoxCollider = GetComponent<BoxCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // 설정 초기화
        attackBoxCollider.enabled = false;  // 공격 콜라이더 Off
        targetPose = transform.position;    // 목표 위치는 현재 위치
        isAttackPlaying = false;            // 공격 애니메이션 실행 중 여부 
    }

    void Update()
    {
        // 공격 중이면 업데이트 x
        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !playerAnimator.IsInTransition(0))
            isAttackPlaying = false;
        if (isAttackPlaying)
            return;

        // 마우스 클릭에 따른 네비게이션 실행
        ActivateNavigation();

        // 회전 가속
        AccelerateRotation();

        // 공격 모션 판정
        Attack();

        // 애니메이션 업데이트
        playerAnimator.SetFloat("Speed", playerNavMeshAgent.velocity.magnitude);

        //playerRigidbody.velocity = Vector3.zero;
    }

    private void ActivateNavigation()
    {
        // 마우스 클릭시, 해당 위치로 이동
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit mouseHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 클릭한 곳에 적이 있으면, 적 위치로 이동 후 공격
            //if (Physics.Raycast(ray, out mouseHit) && mouseHit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            //{
            //    // 타겟: 마우스 클릭된 적
            //    targetObj = mouseHit.transform.gameObject;
            //}

            //else if (Physics.Raycast(ray, out mouseHit))
            //{
            //    // 일반 지형이면, 타겟 없음, 목표 위치 설정
            //    targetPose = mouseHit.point;
            //    targetObj = null;
            //}

            rayCastHits = Physics.RaycastAll(ray);

            if (rayCastHits.Length > 0)
            {
                print("rayCastHits.Length: " + rayCastHits.Length);

                RaycastHit closeHit = rayCastHits[0];
                float closetDist = float.MaxValue;
                foreach (RaycastHit rayCastHit in rayCastHits)
                {
                    // 플레이어와 비교할 때 가장 가까운 곳
                    if (Vector3.Distance(transform.position, rayCastHit.point) < closetDist)
                    {
                        closeHit = rayCastHit;
                    }
                }

                //// 일반 지형이면, 타겟 없음, 목표 위치 설정
                //targetPose = mouseHit.point;
                //targetObj = null;

                // 일반 지형이면, 타겟 없음, 목표 위치 설정
                targetPose = closeHit.point;
                targetObj = null;
            }

        }

        // 타겟 있으면, 타겟 따라서 목표 위치 설정
        if (targetObj != null)
        {
            // 적이 바로 앞이면 움직일 필요x
            if (distanceBetweenTargetObj > Vector3.Distance(targetObj.transform.position, transform.position))
            {
                targetPose = transform.position;
                return;
            }

            // 타겟 위치보다 distanceBetweenTargetObj 뒤에서 멈춘다.
            targetPose = targetObj.transform.position - Vector3.Normalize(targetObj.transform.position - transform.position) * distanceBetweenTargetObj;
        }

        // 네이게이션 실행
        playerNavMeshAgent.SetDestination(targetPose);
    }

    private void AccelerateRotation()
    {
        Vector3 rotateForward = Vector3.zero;

        // 타겟 유무에 따른 회전 벡터 결정
        if (targetObj != null)
            rotateForward = Vector3.Normalize(targetObj.transform.position - transform.position);
        else
            rotateForward = Vector3.Normalize(targetPose - transform.position);

        // 목표 회전 벡터 결정
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        // 회전

        transform.LookAt(this.transform.position + rotateForward);
    }

    private void Attack()
    {
        // 타겟이 있고, 타겟 근처에 왔으면 공격 1회 실행
        if (targetObj != null && Vector3.Distance(transform.position, targetPose) < 0.01f)
        {
            // 1회 공격 실시
            playerAnimator.SetTrigger("Attack");
            isAttackPlaying = true;

            // 한번 공격 후 타겟 = null, 반복 실행 방지
            targetObj = null;
        }
    }

    private void SetNormalAttack(GameObject _target, bool _isAttackColliderActivate)
    {
        // 타겟 업데이트
        targetObj = _target;

        // 일반 공격 콜라이더 업데이트
        attackBoxCollider.enabled = _isAttackColliderActivate;
    }

    // 공격 애니메이션에서 호출하는 함수
    void ActivateEventAboutCollider(float _t)
    {
        StartCoroutine("ActivateAttakCollider", _t);
    }

    IEnumerator ActivateAttakCollider(float _t)
    {
        // 일반 공격 콜라이더 On
        attackBoxCollider.enabled = true;

        yield return new WaitForSeconds(_t);

        // 한번 공격 후,
        // 일반 공격 콜라이더: Off
        attackBoxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 데미지 주기, 공격 콜라이더만 trigger 설정됨.
        print(other.name + "damaged");
    }
}
