using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Enemy enemy;
    NavMeshAgent nav;
    StateMachine stateMachine;
    Animator animator;
    // GameObject TargetObject = null;
    public GameObject TargetObject { get; private set; }
    public Vector3 TargetPosition { get; private set; }

    public float SupposedSmallestEnemySize = 0.1f;

    bool isRunAway = false;
    bool isStopMoving = false;
    bool isOnlyChasePositionMode = false;

    void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        nav = transform.parent.GetComponent<NavMeshAgent>();
        animator = transform.parent.GetComponent<Animator>();
        stateMachine = transform.parent.GetComponent<StateMachine>();
        StartCoroutine(SetPositionToTarget());
    }

    void FixedUpdate()
    {
        if (TargetObject == null || stateMachine.CompareState(StateMachine.enumState.Dead) || !nav.enabled) return;

        // Nav 우선순위 선정
        SetNavAvoidancePriority();

        if (stateMachine.CompareState(StateMachine.enumState.Idle))
        {
            nav.isStopped = false;
            Rotate();
        }
        else
            nav.isStopped = true;

        // print("dist in enemyMove : " + Vector3.Distance(TargetPosition, transform.position));
        // print("Magnitude in enemyMove : " + Vector3.Magnitude(TargetPosition - transform.position));

        AccelerateRotation();
        nav.SetDestination(TargetPosition);
        animator.SetFloat("Speed", nav.velocity.sqrMagnitude);
    }

    void SetNavAvoidancePriority()
    {
        int avoidancePriority = 0;

        if (isRunAway)
            avoidancePriority = 0;
        else if (TargetObject != null)
        {
            avoidancePriority = (int)(Vector3.Distance(transform.position, TargetObject.transform.position) / SupposedSmallestEnemySize);
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

        if (!isRunAway)
            rotateForward = Vector3.Normalize(TargetObject.transform.position - transform.position);
        else
            rotateForward = Vector3.Normalize(TargetPosition - transform.position);

        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);
        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward, 
            360f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        enemy.transform.LookAt(transform.position + limit);
    }

    public void SetTarget(GameObject target)
    {
        TargetObject = target;
    }

    public void ReleaseTarget()
    {
        TargetObject = null;
        stateMachine.Transition(StateMachine.enumState.Idle);
    }

    public void RunAway()
    {
        if (TargetObject == null) return;

        animator.SetBool("OnSkill", false);
        isRunAway = true;

        Vector3 MoveAwayDistVector = (transform.position - TargetObject.transform.position) * 3;
        TargetPosition = transform.position + MoveAwayDistVector;

        // TargetPosition = (transform.position - TargetObject.transform.position) * 3;
        // TargetPosition += transform.position;
        Invoke("StopRunAway", 5.0f);
    }

    void StopRunAway()
    {
        isRunAway = false;
    }

    public void SetOnlyChasePositionMode(bool value, Vector3 targetPose)
    {
        if (value)
        {
            isOnlyChasePositionMode = true;
            TargetPosition = targetPose;
            nav.stoppingDistance = 0f;
        }
        else
        {
            isOnlyChasePositionMode = false;
            SetTargetPose();
            nav.stoppingDistance = 2f;
        }
    }

    void SetTargetPose()
    {
        if (isOnlyChasePositionMode)
            return;

        if (TargetObject != null && !isStopMoving)
            TargetPosition = TargetObject.transform.position;
    }

    public void StopMoving(bool value)
    {
        if (value)
        {
            isStopMoving = true;
            TargetPosition = transform.position;
        }
        else
        {
            isStopMoving = false;
            SetTargetPose();
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
            SetTargetPose();

            yield return new WaitForSeconds(Random.Range(4.0f, 6.0f));
        }
    }

    private void Rotate()
    {
        Vector3 rotateForward = Vector3.zero;
        //if (TargetObject == null) return;

        

        // 타겟 유무에 따른 회전 벡터 결정
        // rotateForward = Vector3.Normalize(TargetObject.transform.position - transform.position);
        if (!isRunAway)
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
}
