using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour, IStunned
{
    [Header("회전 계수")]
    public float rotateCoef = 1f;
    protected StateMachine stateMachine;
    
    public Vector3 TargetPose { get; protected set; }

    protected float speed = 7.5f;
    protected virtual void SetSpeed(float value)
    {
        speed = value;
    }

    protected virtual void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    protected abstract bool CanMove();

    protected abstract bool IsMovableState();

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

    public virtual void Stunned(bool isStunned, float stunnedTime)
    {
        if (isStunned)
        {
            StartCoroutine(StunnedFor(stunnedTime));
        }
        else
        {
            //isStunned = false;
        }
    }

    public virtual IEnumerator StunnedFor(float time)
    {
        //isStunned = true;
        yield return new WaitForSeconds(time);
        //isStunned = false;
    }
}
