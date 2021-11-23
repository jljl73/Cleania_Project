using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyMove : MonoBehaviour, IStunned
{
    // Enemy enemy;
    NavMeshAgent nav;
    public StateMachine stateMachine;
    public Enemy myEnemy;
    Animator animator;
    // GameObject TargetObject = null;
    public GameObject TargetObject { get; private set; }
    public Vector3 TargetPosition { get; private set; }


    [SerializeField]
    float supposedSmallestEnemySize = 0.1f;
    [SerializeField]
    bool isFixedNavPriority = false;

    //bool isRunAway = false;
    //bool isStopMoving = false;
    //bool isOnlyChasePositionMode = false;

    [SerializeField]
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

    private void Awake()
    {
        // enemy = transform.parent.GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        if (animator == null)
            throw new System.Exception("EnemyMove doesnt have animator");

        nav = GetComponent<NavMeshAgent>();
        if (nav == null)
            throw new System.Exception("EnemyMove doesnt have nav");

        if (stateMachine == null)
            throw new System.Exception("EnemyMove doesnt have stateMachine");

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
        //SetDestination(TargetPosition);
    }

    protected bool CanMove()
    {
        if (!IsMovableState())
            return false;

        if (TargetObject == null || !nav.enabled || nav.isStopped)
            return false;

        return true;
    }

    protected virtual bool IsMovableState()
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

    void SetDestination(Vector3 position)
    {
        if (nav.enabled)
            nav.SetDestination(position);
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

    void AccelerateRotation()
    {
        Vector3 rotateForward; //= Vector3.zero;

        //if (!isRunAway)
        //    rotateForward = Vector3.Normalize(TargetObject.transform.position - transform.position);
        //else
        //    rotateForward = Vector3.Normalize(TargetPosition - transform.position);
        rotateForward = Vector3.Normalize(TargetPosition - transform.position);

        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);
        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            180 * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        transform.LookAt(transform.position + limit);
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

    public void RunAway()
    {
        if (TargetObject == null) return;

        animator.SetBool("OnSkill", false);
        //isRunAway = true;
        moveMode = MoveMode.RunAway;

        moveAwayDistVector = (transform.position - TargetObject.transform.position) * 3;
        //TargetPosition = transform.position + MoveAwayDistVector;

        Invoke("StopRunAway", 5.0f);
    }

    void StopRunAway()
    {
        //isRunAway = false;
        moveMode = MoveMode.Idle;
    }

    public void SetOnlyChasePositionMode(bool value, Vector3 targetPose)
    {
        if (value)
        {
            //isOnlyChasePositionMode = true;
            moveMode = MoveMode.OnlyChasePosition;
            // TargetPosition = targetPose;
            chaseOnlyPosition = targetPose;
            nav.stoppingDistance = 0f;
        }
        else
        {
            //isOnlyChasePositionMode = false;
            moveMode = MoveMode.Idle;
            // SetTargetPose(TargetObject.transform.position);
            nav.stoppingDistance = 2f;
        }
    }

    void SetTargetPosition()
    {
        switch (moveMode)
        {
            case MoveMode.Idle:
            case MoveMode.StopMoving:
                TargetPosition = transform.position;
                break;
            case MoveMode.Chasing:
                TargetPosition = TargetObject.transform.position;
                break;
            case MoveMode.RunAway:
                TargetPosition = transform.position + moveAwayDistVector;
                break;
            case MoveMode.OnlyChasePosition:
                TargetPosition = chaseOnlyPosition;
                break;
            default:
                break;
        }
    }

    void SetTargetPose(Vector3 position)
    {
        if (moveMode == MoveMode.OnlyChasePosition)
            return;

        if (TargetObject != null && !(moveMode == MoveMode.StopMoving))
            TargetPosition = TargetObject.transform.position;
    }

    public void StopMoving(bool value)
    {
        if (value)
        {
            //isStopMoving = true;
            moveMode = MoveMode.StopMoving;
            //TargetPosition = transform.position;
        }
        else
        {
            //isStopMoving = false;
            moveMode = MoveMode.Idle;
            //SetTargetPose(TargetObject.transform.position);
        }
        
    }

    public void WarpToPosition(Vector3 target)
    {
        transform.position = target;
    }

    IEnumerator SetPositionToTarget()
    {
        while(true)
        {
            //SetTargetPose(TargetObject.transform.position);
            SetTargetPosition();
            SetDestination(TargetPosition);

            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }

    private void Rotate()
    {
        Vector3 rotateForward = Vector3.zero;

        // 타겟 유무에 따른 회전 벡터 결정
        //if (!isRunAway)
        if (moveMode != MoveMode.RunAway)
            rotateForward = Vector3.Normalize(TargetObject.transform.position - transform.position);
        else
            rotateForward = Vector3.Normalize(TargetPosition - transform.position);

        // 목표 회전 벡터 결정
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);
        // 회전
        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        transform.LookAt(this.transform.position + limit);
    }

    public bool ExistTarget()
    {
        return TargetObject != null;
    }

    public void Stunned(bool isStunned, float stunnedTime)
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

    public IEnumerator StunnedFor(float time)
    {
        nav.enabled = false;
        yield return new WaitForSeconds(time);
        nav.enabled = true;
    }
}
