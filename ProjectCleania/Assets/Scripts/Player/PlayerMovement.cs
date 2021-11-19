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
    RaycastHit hit;
    Player player;

    Animator playerAnimator;            // �ִϸ����� ������Ʈ
    NavMeshAgent playerNavMeshAgent;    // path ��� ������Ʈ
    Rigidbody playerRigidbody;          // ������ٵ� ������Ʈ


    Vector3 targetPose;                 // ��ǥ ��ġ
    public Vector3 TargetPose { get => targetPose; private set { targetPose = value; } }
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
        TargetPose = this.transform.position;
    }

    private void OnDisable()
    {
        playerNavMeshAgent.enabled = false;
    }

    void FixedUpdate()
    {
        if (!CanMove())
            return;

        ActivateNavigation();
        #region
        //#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        //        if (!EventSystem.current.IsPointerOverGameObject())
        //            ActivateNavigation();
        //#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        //        if (!EventSystem.current.IsPointerOverGameObject(0))
        //            ActivateNavigation();
        //#endif
        #endregion
        // ȸ�� ����
        AccelerateRotation();

        // �ִϸ��̼� ������Ʈ
        playerAnimator.SetFloat("Speed", playerNavMeshAgent.velocity.magnitude);
    }

    bool CanMove()
    {
        if (player.stateMachine.CompareState(StateMachine.enumState.Dead) ||
            player.stateMachine.CompareState(StateMachine.enumState.Attacking))
        {
            ResetNavigation(this.transform.position);
            return false;
        }

        if (isStunned)
        {
            TargetPose = transform.position;
            return false;
        }

        return true;
    }

    public void Move(Vector3 pose)
    {
        MoveToPosition(pose);
    }

    public void StopMoving()
    {
        TargetPose = transform.position;
    }

    void ResetNavigation(Vector3 newPose)
    {
        TargetPose = transform.position;
        playerNavMeshAgent.SetDestination(TargetPose);
    }

    private void ActivateNavigation()
    {
        if (!playerNavMeshAgent.SetDestination(TargetPose))
            print("playerNavMeshAgent.SetDestination(TargetPose) FAILED!!!!");
    }

    private void AccelerateRotation()
    {
        Vector3 rotateForward; //= Vector3.zero;

        rotateForward = Vector3.Normalize(TargetPose - transform.position);

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
                TargetPose = hit.point;
                //print("Ground Hit");
            }
            else if (hit.collider.CompareTag("Enemy"))
                TargetPose = hit.collider.transform.position;
        }

        if (Vector3.Distance(TargetPose, transform.position) > 0.01f)
        {
            playerAnimator.SetBool("Walk", true);
        }
    }

    public void JumpForward(float dist)
    {
        TargetPose = transform.position + transform.forward * dist;
    }

    bool IsMovableLayer(string collideTag, out RaycastHit rayhitInfo)
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
        if (IsMovableLayer("Ground", out rayhitInfo))
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
        TargetPose = transform.position + transform.forward * dist;
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
