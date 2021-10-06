using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    NavMeshAgent nav;
    StateMachine state;
    GameObject targetObject = null;
    Animator animator;

    public GameObject temp;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        state = GetComponent<StateMachine>();

        // �׽�Ʈ��
        SetTarget(temp);
    }
    
    void FixedUpdate()
    {
        if (targetObject == null) return;

        if (state.State == StateMachine.enumState.Attacking)
            nav.isStopped = true;
        else
            nav.isStopped = false;


        nav.SetDestination(targetObject.transform.position);
        animator.SetFloat("Speed", nav.velocity.sqrMagnitude);

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        Rotate();
    }

    public void SetTarget(GameObject target)
    {
        targetObject = target;
        //state.Transition(StateMachine.enumState.Chasing);
    }

    public void ReleaseTarget()
    {
        targetObject = null;
        state.Transition(StateMachine.enumState.Idle);
    }

    private void Rotate()
    {
        Vector3 rotateForward = Vector3.zero;
        //if (targetObject == null) return;

        // Ÿ�� ������ ���� ȸ�� ���� ����
        rotateForward = Vector3.Normalize(targetObject.transform.position - transform.position);
        // ��ǥ ȸ�� ���� ����
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);
        // ȸ��
        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        transform.LookAt(this.transform.position + limit);
    }
}