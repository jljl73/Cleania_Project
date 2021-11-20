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

    Animator animator;            // �ִϸ����� ������Ʈ
    NavMeshAgent navMeshAgent;    // path ��� ������Ʈ
    Rigidbody characterRigidBody;          // ������ٵ� ������Ʈ
    float beforeFrameVelocity = -1;
    float currentFrameVelocity;

    Vector3 targetPose;                 // ��ǥ ��ġ
    public Vector3 TargetPose { get => targetPose; private set { targetPose = value; } }
    bool bChasing = false;
    bool bPulled = false;
    bool bPushed = false;

    private void Awake()
    {
        // ������Ʈ �ҷ�����
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterRigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        navMeshAgent.enabled = true;
        TargetPose = this.transform.position;
    }

    private void OnDisable()
    {
        navMeshAgent.enabled = false;
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
        if (!bPulled && !bPushed)
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    bool CanMove()
    {
        if (IscharacterRigidBodyOn())
            return false;

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

    bool IscharacterRigidBodyOn()
    {
        if (!bPushed)
        {
            currentFrameVelocity = 0;
            beforeFrameVelocity = -1;
            return false;
        }
        else
        {
            currentFrameVelocity = characterRigidBody.velocity.magnitude;

            if (characterRigidBody.velocity.magnitude <= 1f && (beforeFrameVelocity - currentFrameVelocity) >= 0)
            {
                SetRigidBody(false);
                animator.SetBool("Pulled", false);
                bPushed = false;
            }

            beforeFrameVelocity = currentFrameVelocity;
            return true;
        }
    }

    public void Move(Vector3 pose)
    {
        if (bPulled)
            return;
        MoveToPosition(pose);
    }

    public void StopMoving()
    {
        TargetPose = transform.position;
    }

    void ResetNavigation(Vector3 newPose)
    {
        TargetPose = newPose;
        navMeshAgent.SetDestination(newPose);
    }

    private void ActivateNavigation()
    {
        if (!navMeshAgent.SetDestination(TargetPose))
            print("navMeshAgent.SetDestination(TargetPose) FAILED!!!!");
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
            animator.SetBool("Walk", true);
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

    public void AddForce(Vector3 force)
    {
        SetRigidBody(true);
        characterRigidBody.AddForce(force);
        animator.SetBool("Pulled", true);

        bPushed = true;
    }

    void SetRigidBody(bool value)
    {
        characterRigidBody.isKinematic = !value;
        navMeshAgent.isStopped = value;
    }

    public void Pulled(bool value, Vector3 position)
    {
        bPulled = value;
        if (value)
        {
            SetRigidBody(!value);
            TargetPose = position;
            player.playerSkillManager.DeactivateAllSkill();
        }
        else
            TargetPose = this.transform.position;

        animator.SetBool("Pulled", value);
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
