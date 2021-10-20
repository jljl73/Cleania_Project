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
    GameObject targetObject = null;
    Vector3 targetPosition;

    public float SupposedSmallestEnemySize = 0.1f;

    bool isRunAway;

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
        if (targetObject == null || stateMachine.CompareState(StateMachine.enumState.Dead) || !nav.enabled) return;

        // Nav 우선순위 선정
        SetNavAvoidancePriority();

        if (stateMachine.CompareState(StateMachine.enumState.Idle))
        {
            nav.isStopped = false;
            Rotate();
        }
        else
            nav.isStopped = true;

        AccelerateRotation();
        nav.SetDestination(targetPosition);
        animator.SetFloat("Speed", nav.velocity.sqrMagnitude);
    }

    void SetNavAvoidancePriority()
    {
        int avoidancePriority = 0;

        if (isRunAway)
            avoidancePriority = 0;
        else if (targetObject != null)
        {
            avoidancePriority = (int)(Vector3.Distance(transform.position, targetObject.transform.position) / SupposedSmallestEnemySize);
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
            rotateForward = Vector3.Normalize(targetObject.transform.position - transform.position);
        else
            rotateForward = Vector3.Normalize(targetPosition - transform.position);

        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);
        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward, 
            360f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        enemy.transform.LookAt(transform.position + limit);
    }

    public void SetTarget(GameObject target)
    {
        targetObject = target;
    }

    public void ReleaseTarget()
    {
        targetObject = null;
        stateMachine.Transition(StateMachine.enumState.Idle);
    }

    public void RunAway()
    {
        if (targetObject == null) return;

        

        animator.SetBool("OnSkill", false);
        isRunAway = true;

        Vector3 MoveAwayDistVector = (transform.position - targetObject.transform.position) * 3;
        targetPosition = transform.position + MoveAwayDistVector;

        // targetPosition = (transform.position - targetObject.transform.position) * 3;
        // targetPosition += transform.position;
        Invoke("StopRunAway", 5.0f);
    }

    void StopRunAway()
    {
        isRunAway = false;
    }

    public void WarpToPosition(Vector3 target)
    {
        transform.position = target;
    }

    IEnumerator SetPositionToTarget()
    {
        while(true)
        {
            if(targetObject !=null)
                targetPosition = targetObject.transform.position;

            yield return new WaitForSeconds(Random.Range(4.0f, 6.0f));
        }
    }

    private void Rotate()
    {
        Vector3 rotateForward = Vector3.zero;
        //if (targetObject == null) return;

        

        // 타겟 유무에 따른 회전 벡터 결정
        // rotateForward = Vector3.Normalize(targetObject.transform.position - transform.position);
        if (!isRunAway)
            rotateForward = Vector3.Normalize(targetObject.transform.position - transform.position);
        else
            rotateForward = Vector3.Normalize(targetPosition - transform.position);

        // 목표 회전 벡터 결정
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);
        // 회전
        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        transform.LookAt(this.transform.position + limit);
    }

    public bool ExistTarget()
    {
        return targetObject != null;
    }
}
