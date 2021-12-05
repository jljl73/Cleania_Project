using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour, IStunned
{
    // ��� ������Ʈ
    protected StateMachine stateMachine;

    // ������ ���� ����
    [Header("ȸ�� ���")]
    public float rotateCoef = 1f;
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

        // ��ǥ ȸ�� ���� ����
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            rotateCoef * 180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        // ȸ��
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
