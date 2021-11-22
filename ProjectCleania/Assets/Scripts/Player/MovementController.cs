using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MovementController : MonoBehaviour, IStunned
{
    [Header("회전 계수")]
    public float rotateCoef = 360f;

    protected StateMachine stateMachine;
    protected RaycastHit hit;
    protected Animator animator;            // 애니메이터 컴포넌트
    protected Vector3 targetPose;                 // 목표 위치
    protected AbilityStatus abilityStatus;
    public Vector3 TargetPose { get => targetPose; protected set { targetPose = value; } }

    protected bool isStunned = false;
    protected float speed = 7.5f;
    protected virtual void SetSpeed(float value)
    {
        speed = value;
    }

    protected virtual void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        abilityStatus = GetComponent<AbilityStatus>();
    }

    protected abstract bool CanMove();

    protected virtual bool IsMovableState()
    {
        if (stateMachine.CompareState(StateMachine.enumState.Dead) ||
            stateMachine.CompareState(StateMachine.enumState.Attacking)) return false;
        return true;
    }

    protected virtual void AccelerateRotation()
    {
        Vector3 rotateForward; //= Vector3.zero;

        rotateForward = Vector3.Normalize(TargetPose - transform.position);

        // 목표 회전 벡터 결정
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            rotateCoef * 180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        // 회전
        transform.LookAt(this.transform.position + limit);
    }

    public virtual void Move(Vector3 pose)
    {
        MoveToPosition(pose);
    }

    protected virtual void MoveToPosition(Vector3 position)
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

    protected virtual bool IsMovableLayer(string collideTag, out RaycastHit rayhitInfo)
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

    public virtual void ImmediateLookAtMouse()
    {
        RaycastHit rayhitInfo;
        if (IsMovableLayer("Ground", out rayhitInfo))
        {
            Vector3 lookAtPointOnSameY = new Vector3(rayhitInfo.point.x, transform.position.y, rayhitInfo.point.z);
            this.transform.LookAt(lookAtPointOnSameY);
        }
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
