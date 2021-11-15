using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour, IStunned
{
    [Header("������ ��� ��ų")]
    public PlayerSkillRefreshingLeapForward LeapForwardSkill;
    public PlayerSkillRoll rollSkill;

    [Header("ȸ�� ���")]
    public float rotateCoef = 360f;

    bool isStunned = false;
    bool isOrderedToMove = false;
    RaycastHit hit;
    Player player;

    private Animator playerAnimator;            // �ִϸ����� ������Ʈ
    private NavMeshAgent playerNavMeshAgent;    // path ��� ������Ʈ
    private Rigidbody playerRigidbody;          // ������ٵ� ������Ʈ


    private Vector3 targetPose;                 // ��ǥ ��ġ
    bool bChasing = false;

    private void Awake()
    {
        // ������Ʈ �ҷ�����
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
        // ���� �ʱ�ȭ
        targetPose = transform.position;    // ��ǥ ��ġ�� ���� ��ġ
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
        // ȸ�� ����
        AccelerateRotation();

        // �ִϸ��̼� ������Ʈ
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

        // ��ǥ ȸ�� ���� ����
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            rotateCoef * 180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        // ȸ��
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
