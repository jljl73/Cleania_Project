using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : CharacterMovement
{
    NavMeshAgent navMeshAgent;              // �׺� �Ž� ������Ʈ
    Rigidbody characterRigidBody;           // ������ٵ� ������Ʈ
    PlayerSkillManager playerSkillManager;  // ��ų �Ŵ��� ������Ʈ
    Animator animator;                      // �ִϸ����� ������Ʈ
    AbilityStatus abilityStatus;            // �����Ƽ�������ͽ� ������Ʈ

    RaycastHit hit;

    float beforeFrameVelocity = -1;
    float currentFrameVelocity;

    //bool bChasing = false;
    bool bPulled = false;
    bool bPushed = false;
    bool isStunned = false;

    MoveMode moveMode = MoveMode.Idle;
    enum MoveMode
    {
        Idle,
        Chasing,
        RunAway,
        StopMoving,
        OnlyChasePosition,
        Pulled,
        Pushed
    }

    protected new void Awake()
    {
        // ������Ʈ �ҷ�����
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterRigidBody = GetComponent<Rigidbody>();
        playerSkillManager = GetComponent<PlayerSkillManager>();
        animator = GetComponent<Animator>();
        abilityStatus = GetComponent<AbilityStatus>();
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

        // ���� �ʿ�!!!!!!!!!!!

        // Ability.Stat ���� ������ ����ϴ� ��ü���� Ability.State.Change�� �����ϴ� ���� �ʿ��ҵ�?

        // �ӵ� ����
        // SetSpeed(abilityStatus.GetStat(Ability.Stat.MoveSpeed) * 6);



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

        if (moveMode != MoveMode.Pulled && moveMode != MoveMode.Pushed)
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    protected override bool CanMove()
    {
        if (IscharacterRigidBodyOn())
            return false;

        if (!IsMovableState())
            return false;

        if (isStunned)
        {
            TargetPose = transform.position;
            return false;
        }
        if (moveMode == MoveMode.StopMoving)
        {
            TargetPose = transform.position;
            return false;
        }

        return true;
    }

    protected override void SetSpeed(float value)
    {
        base.SetSpeed(value);
        navMeshAgent.speed = value;
    }

    protected override bool IsMovableState()
    {
        if (stateMachine.CompareState(StateMachine.enumState.Dead) ||
            stateMachine.CompareState(StateMachine.enumState.Attacking))
        {
            ResetNavigation(this.transform.position);
            return false;
        }
        return true;
    }

    bool IscharacterRigidBodyOn()
    {
        if (!bPushed && moveMode != MoveMode.Pushed)
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
                moveMode = MoveMode.Idle;
            }

            beforeFrameVelocity = currentFrameVelocity;
            return true;
        }
    }

    //public void StopMoving()
    //{
    //    TargetPose = transform.position;
    //}

    void ResetNavigation(Vector3 newPose)
    {
        TargetPose = newPose;
        navMeshAgent.SetDestination(newPose);
    }

    private void ActivateNavigation()
    {
        navMeshAgent.SetDestination(TargetPose);
    }

    public void Move(Vector3 pose)
    {
        if (bPulled)
            return;
        MoveToPosition(pose);
    }

    void MoveToPosition(Vector3 position)
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
            Vector3 lookAtPointOnSameY = new Vector3(rayhitInfo.point.x, transform.position.y, rayhitInfo.point.z);
            this.transform.LookAt(lookAtPointOnSameY);
        }
    }

    #region

    //public void JumpForward(float dist)
    //{
    //    TargetPose = transform.position + transform.forward * dist;
    //}


    //private void AccelerateRotation()
    //{
    //    Vector3 rotateForward; //= Vector3.zero;

    //    rotateForward = Vector3.Normalize(TargetPose - transform.position);

    //    // ��ǥ ȸ�� ���� ����
    //    rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

    //    Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
    //        rotateCoef * 180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

    //    // ȸ��
    //    transform.LookAt(this.transform.position + limit);
    //}



    //public void MoveToPosition(Vector3 position)
    //{
    //    int layerMask = 0;
    //    layerMask = 1 << 5 | 1 << 7;

    //    Ray ray = Camera.main.ScreenPointToRay(position);

    //    if (Physics.Raycast(ray, out hit, 500.0f, layerMask))
    //    {
    //        if (hit.collider.tag == "Ground")
    //        {
    //            TargetPose = hit.point;
    //            //print("Ground Hit");
    //        }
    //        else if (hit.collider.CompareTag("Enemy"))
    //            TargetPose = hit.collider.transform.position;
    //    }

    //    if (Vector3.Distance(TargetPose, transform.position) > 0.01f)
    //    {
    //        animator.SetBool("Walk", true);
    //    }
    //}



    //bool IsMovableLayer(string collideTag, out RaycastHit rayhitInfo)
    //{
    //    bool result = false;

    //    int layerMask = 0;
    //    layerMask = 1 << 5 | 1 << 7;

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit raycastHit;
    //    if (Physics.Raycast(ray, out raycastHit, 500.0f, layerMask))
    //    {
    //        if (raycastHit.collider.CompareTag(collideTag))
    //            result = true;
    //    }
    //    rayhitInfo = raycastHit;

    //    return result;
    //}

    //public void ImmediateLookAtMouse()
    //{
    //    RaycastHit rayhitInfo;
    //    if (IsMovableLayer("Ground", out rayhitInfo))
    //    {
    //        print("ImmediateLookAtMouse it's ground!");
    //        Vector3 lookAtPointOnSameY = new Vector3(rayhitInfo.point.x, transform.position.y, rayhitInfo.point.z);
    //        player.gameObject.transform.LookAt(lookAtPointOnSameY);
    //    }
    //}

    //public void LeapForwardSkillJumpForward()
    //{
    //    print("LeapForwardSkillJumpForward!");
    //    if (LeapForwardSkill == null)
    //        throw new System.Exception("TestPlayerMove dosent have LeapForwardSkillJumpForward");
    //    float dist = LeapForwardSkill.GetJumpDistance();
    //    TargetPose = transform.position + transform.forward * dist;
    //}

    #endregion

    public void AddForce(Vector3 force)
    {
        SetRigidBody(true);
        characterRigidBody.AddForce(force);
        animator.SetBool("Pulled", true);

        bPushed = true;
        moveMode = MoveMode.Pushed;
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
            playerSkillManager.DeactivateAllSkill();
        }
        else
            TargetPose = this.transform.position;

        animator.SetBool("Pulled", value);
    }

    public void MakeNavRadius(float value)
    {
        navMeshAgent.radius = value;
    }

    public void SpeedUp(float value)
    {
        SetSpeed(value);
    }
    public void ResetSpeed()
    {
        SetSpeed(7.5f);
    }

    #region
    public override void Stunned(bool isStunned, float stunnedTime)
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

    public override IEnumerator StunnedFor(float time)
    {
        isStunned = true;
        yield return new WaitForSeconds(time);
        isStunned = false;
    }
    #endregion
}
