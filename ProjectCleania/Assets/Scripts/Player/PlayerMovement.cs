using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour, IStunned
{
    [Header("움직임 사용 스킬")]
    public PlayerSkillRefreshingLeapForward LeapForwardSkill;
    public PlayerSkillRoll rollSkill;

    [Header("회전 계수")]
    public float rotateCoef = 360f;

    bool isStunned = false;
    bool isOrderedToMove = false;
    RaycastHit hit;
    Player player;

    private Animator playerAnimator;            // 애니메이터 컴포넌트
    private NavMeshAgent playerNavMeshAgent;    // path 계산 컴포턴트
    private Rigidbody playerRigidbody;          // 리지드바디 컴포넌트


    private Vector3 targetPose;                 // 목표 위치
    bool bChasing = false;

    private void Awake()
    {
        // 컴포넌트 불러오기
        player = GetComponent<Player>();
        playerAnimator = GetComponent<Animator>();
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        playerNavMeshAgent.enabled = true;
        targetPose = this.transform.position;
    }

    private void OnDisable()
    {
        playerNavMeshAgent.enabled = false;
    }

    void Start()
    {
        // 설정 초기화
        targetPose = transform.position;    // 목표 위치는 현재 위치
    }

    void Update()
    {
        if (player.stateMachine.CompareState(StateMachine.enumState.Dead) ||
            player.stateMachine.CompareState(StateMachine.enumState.Attacking))
        {
            ResetNavigation(this.transform.position);
            return;
        }

        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))

        if (!isOrderedToMove || isStunned)
        {
            targetPose = transform.position;
            return;
        }

        ActivateNavigation();

//#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
//        if (!EventSystem.current.IsPointerOverGameObject())
//            ActivateNavigation();
//#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
//        if (!EventSystem.current.IsPointerOverGameObject(0))
//            ActivateNavigation();
//#endif
        // 회전 가속
        AccelerateRotation();

        // 애니메이션 업데이트
        playerAnimator.SetFloat("Speed", playerNavMeshAgent.velocity.magnitude);
    }

    public void Move(Vector3 pose)
    {
        isOrderedToMove = true;
        MoveToPosition(pose);
    }

    public void StopMoving()
    {
        targetPose = transform.position;
    }

    void ResetNavigation(Vector3 newPose)
    {
        targetPose = transform.position;
        playerNavMeshAgent.SetDestination(targetPose);
    }

    private void ActivateNavigation()
    {
        if (player.stateMachine.State == StateMachine.enumState.Attacking)
        {
            targetPose = transform.position;
        }

        if (player.stateMachine.State != StateMachine.enumState.Chasing)
        {
            playerNavMeshAgent.SetDestination(targetPose);
        }
    }

    private void AccelerateRotation()
    {
        Vector3 rotateForward; //= Vector3.zero;

        rotateForward = Vector3.Normalize(targetPose - transform.position);

        // 목표 회전 벡터 결정
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            rotateCoef * 180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        // 회전
        transform.LookAt(this.transform.position + limit);
    }

    public void MoveToPosition(Vector3 position)
    {
        int layerMask = 0;
        layerMask = 1 << 5 | 1 << 7;

        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out hit, 500.0f, layerMask))
        {
            if (hit.collider.tag == "Ground")
            {
                targetPose = hit.point;
                //print("Ground Hit");
            }
            else if (hit.collider.CompareTag("Enemy"))
                targetPose = hit.collider.transform.position;
        }

        if (Vector3.Distance(targetPose, transform.position) > 0.01f)
        {
            playerAnimator.SetBool("Walk", true);
        }
    }

    public void JumpForward(float dist)
    {
        //playerNavMeshAgent.avoidancePriority = 1;
        targetPose = transform.position + transform.forward * dist;
    }

    bool CanBeTriggerd(string collideTag, out RaycastHit rayhitInfo)
    {
        bool result = false;

        int layerMask = 0;
        layerMask = 1 << 5 | 1 << 7;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 500.0f, layerMask))
        {
            if (raycastHit.collider.CompareTag(collideTag))
                result = true;
        }
        rayhitInfo = raycastHit;

        return result;
    }

    public void ImmediateLookAtMouse()
    {
        RaycastHit rayhitInfo;
        if (CanBeTriggerd("Ground", out rayhitInfo))
        {
            print("ImmediateLookAtMouse it's ground!");
            Vector3 lookAtPointOnSameY = new Vector3(rayhitInfo.point.x, transform.position.y, rayhitInfo.point.z);
            player.gameObject.transform.LookAt(lookAtPointOnSameY);
            //mousePos = lookAtPointOnSameY;
        }
    }

    public void LeapForwardSkillJumpForward()
    {
        print("LeapForwardSkillJumpForward!");
        if (LeapForwardSkill == null)
            throw new System.Exception("TestPlayerMove dosent have LeapForwardSkillJumpForward");
        float dist = LeapForwardSkill.GetJumpDistance();
        isOrderedToMove = true;
        targetPose = transform.position + transform.forward * dist;
    }

    public void Stunned(bool isStunned, float stunnedTime)
    {
        if (isStunned)
        {
            StartCoroutine(StunnedFor(stunnedTime));
        }
        else
        {
            isStunned = false;
        }
    }

    public IEnumerator StunnedFor(float time)
    {
        isStunned = true;
        yield return new WaitForSeconds(time);
        isStunned = false;
    }
}
