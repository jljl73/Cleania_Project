using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyMovementController : MovementController
{
    Enemy myEnemy;
    NavMeshAgent nav;
    Animator animator;

    public GameObject TargetObject { get; private set; }


    [SerializeField]
    float supposedSmallestEnemySize = 0.1f;
    [SerializeField]
    bool isFixedNavPriority = false;

    MoveMode moveMode = MoveMode.Idle;
    enum MoveMode
    {
        Idle,
        Chasing,
        RunAway,
        StopMoving,
        OnlyChasePosition
    }

    Vector3 moveAwayDistVector;
    Vector3 chaseOnlyPosition;

    protected override void Awake()
    {
        // enemy = transform.parent.GetComponent<Enemy>();
        base.Awake();

        animator = GetComponent<Animator>();
        if (animator == null)
            throw new System.Exception("EnemyMove doesnt have animator");

        nav = GetComponent<NavMeshAgent>();
        if (nav == null)
            throw new System.Exception("EnemyMove doesnt have nav");

        myEnemy = GetComponent<Enemy>();
        if (myEnemy == null)
            throw new System.Exception("EnemyMove doesnt have myEnemy");
    }

    private void OnEnable()
    {
        Start();

        StartCoroutine(SetPositionToTarget());
    }

    void Start()
    {
        // 초기에 꺼두기
        nav.enabled = false;
    }

    void FixedUpdate()
    {
        #region
        //if (stateMachine.CompareState(StateMachine.enumState.Attacking))
        //{
        //    SetDestination(this.transform.position);
        //    return;
        //}

        //if (TargetObject == null || stateMachine.CompareState(StateMachine.enumState.Dead) || !nav.enabled) return;
        #endregion
        animator.SetFloat("Speed", nav.velocity.sqrMagnitude);

        if (!CanMove())
            return;

        // Nav 우선순위 선정
        if (!isFixedNavPriority)
            SetNavAvoidancePriority();

        #region
        //if (stateMachine.CompareState(StateMachine.enumState.Idle))
        //{
        //    nav.isStopped = false;
        //    Rotate();
        //}
        //else
        //    nav.isStopped = true;
        #endregion

        AccelerateRotation();
    }

    protected override bool CanMove()
    {
        if (!IsMovableState())
            return false;

        if (TargetObject == null || !nav.enabled || nav.isStopped)
            return false;

        return true;
    }

    protected override bool IsMovableState()
    {
        bool result = true;

        switch (stateMachine.State)
        {
            case StateMachine.enumState.Attacking:
                SetDestination(this.transform.position);
                //moveMode = MoveMode.StopMoving;
                // nav.isStopped = true;
                result = false;
                break;
            case StateMachine.enumState.Dead:
                result = false;
                break;
            default:
                break;
        }

        return result;
    }

    protected override void SetSpeed(float value)
    {
        speed = value;
        nav.speed = speed;
    }

    void SetNavAvoidancePriority()
    {
        int avoidancePriority = 0;

        if (moveMode == MoveMode.StopMoving)
            avoidancePriority = 0;
        else if (TargetObject != null)
        {
            avoidancePriority = (int)(Vector3.Distance(transform.position, TargetObject.transform.position) / supposedSmallestEnemySize);
            if (avoidancePriority == 0)
                avoidancePriority = 1;
            if (avoidancePriority >= 99)
                avoidancePriority = 99;
        }
        else
            avoidancePriority = 50;

        nav.avoidancePriority = avoidancePriority;
    }

    void SetDestination(Vector3 position)
    {
        if (nav.enabled)
            nav.SetDestination(position);
    }

    public void SetTarget(GameObject target)
    {
        TargetObject = target;
        moveMode = MoveMode.Chasing;
    }

    public void ReleaseTarget()
    {
        TargetObject = null;
        stateMachine.Transition(StateMachine.enumState.Idle);
        moveMode = MoveMode.Idle;
    }

    public void RunAway(float duration)
    {
        if (TargetObject == null) return;

        animator.SetBool("OnSkill", false);
        moveMode = MoveMode.RunAway;

        moveAwayDistVector = (transform.position - TargetObject.transform.position) * 3;

        Invoke("StopRunAway", duration);
    }

    void StopRunAway()
    {
        moveMode = MoveMode.Chasing;
    }

    public void SetOnlyChasePositionMode(bool value, Vector3 targetPose)
    {
        if (value)
        {
            moveMode = MoveMode.OnlyChasePosition;
            chaseOnlyPosition = targetPose;
            nav.stoppingDistance = 0f;
        }
        else
        {
            moveMode = MoveMode.Idle;
            nav.stoppingDistance = 2f;
        }
    }

    void SetTargetPose()
    {
        switch (moveMode)
        {
            case MoveMode.Idle:
            case MoveMode.StopMoving:
                TargetPose = transform.position;
                break;
            case MoveMode.Chasing:
                if (TargetObject != null)
                    TargetPose = TargetObject.transform.position;
                break;
            case MoveMode.RunAway:
                TargetPose = transform.position + moveAwayDistVector;
                break;
            case MoveMode.OnlyChasePosition:
                TargetPose = chaseOnlyPosition;
                break;
            default:
                break;
        }
    }

    public void StopMoving(bool value)
    {
        if (value)
        {
            moveMode = MoveMode.StopMoving;
        }
        else
        {
            moveMode = MoveMode.Idle;
        }

    }

    IEnumerator SetPositionToTarget()
    {
        while (true)
        {
            //SetTargetPose(TargetObject.transform.position);
            SetTargetPose();
            SetDestination(TargetPose);

            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }

    public override void Stunned(bool isStunned, float stunnedTime)
    {
        if (isStunned)
        {
            StartCoroutine(StunnedFor(stunnedTime));
        }
        else
        {
            nav.enabled = true;
        }
    }

    public override IEnumerator StunnedFor(float time)
    {
        nav.enabled = false;
        yield return new WaitForSeconds(time);
        nav.enabled = true;
    }

    #region
    //void SetTargetPose(Vector3 position)
    //{
    //    if (moveMode == MoveMode.OnlyChasePosition)
    //        return;

    //    if (TargetObject != null && !(moveMode == MoveMode.StopMoving))
    //        TargetPose = TargetObject.transform.position;
    //}

    //public void WarpToPosition(Vector3 target)
    //{
    //    transform.position = target;
    //}

    //public bool ExistTarget()
    //{
    //    return TargetObject != null;
    //}
    #endregion
}
