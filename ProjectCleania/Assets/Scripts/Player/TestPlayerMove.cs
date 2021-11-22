using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TestPlayerMove : MovementController, IStunned
{
    [Header("움직임 사용 스킬")]
    public PlayerSkillRefreshingLeapForward LeapForwardSkill;
    public PlayerSkillRoll RollSkill;

    bool isOrderedToMove = false;

    protected new void Awake()
    {
        base.Awake();
        TargetPose = transform.position;
        abilityStatus = GetComponent<AbilityStatus>();
    }

    void FixedUpdate()
    {
        if (!CanMove())
            return;

        // 이동 속도 설정
        SetSpeed(abilityStatus.GetStat(Ability.Stat.MoveSpeed) * 6 * RollSkill.AvoidSpeedMultiplier);

        // 이동
        this.transform.localPosition = Vector3.MoveTowards(this.transform.position, TargetPose, speed * Time.deltaTime);

        // 회전 가속
        AccelerateRotation();
    }

    protected override bool CanMove()
    {

        if (!IsMovableState())
            return false;

        if (!isOrderedToMove || isStunned)
        {
            TargetPose = transform.position;
            return false;
        }

        if (Vector3.Distance(TargetPose, transform.position) < 0.2f)
        {
            animator.SetBool("Walk", false);
            TargetPose = transform.position;
            isOrderedToMove = false;
            return false;
        }

        return true;
    }

    public override void Move(Vector3 MousePosition)
    {
        isOrderedToMove = true;
        MoveToPosition(MousePosition);
    }

    public void StopMoving()
    {
        TargetPose = transform.position;
    }

    public void LeapForwardSkillJumpForward()
    {
        print("LeapForwardSkillJumpForward!");
        if (LeapForwardSkill == null)
            throw new System.Exception("TestPlayerMove dosent have LeapForwardSkillJumpForward");
        float dist = LeapForwardSkill.GetJumpDistance();
        isOrderedToMove = true;
        TargetPose = transform.position + transform.forward * dist;
    }

    #region

    //void Targetting()
    //{
    //    RaycastHit raycastHit;
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    // bChasing = false;
    //    // 수정 //
    //    targetObj = null;

    //    // --- //
    //    if (Physics.Raycast(ray, out raycastHit))
    //    {
    //        //Debug.Log(raycastHit.transform.tag);
    //        if (raycastHit.transform.CompareTag("Enemy"))
    //        {
    //            targetObj = raycastHit.transform.gameObject;
    //            // bChasing = true;
    //        }
    //    }
    //}

    //private void AccelerateRotation()
    //{
    //    Vector3 rotateForward; //= Vector3.zero;

    //    // 타겟 유무에 따른 회전 벡터 결정
    //    if (targetObj != null)
    //    {
    //        rotateForward = Vector3.Normalize(targetObj.transform.position - transform.position);
    //    }
    //    else
    //    {
    //        rotateForward = Vector3.Normalize(TargetPose - transform.position);
    //    }

    //    // 목표 회전 벡터 결정
    //    rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

    //    Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
    //        rotateCoef * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

    //    // 회전
    //    this.transform.LookAt(this.transform.position + limit);
    //}



    //void MoveToPosition(Vector3 MousePosition)
    //{
    //    int layerMask = 0;
    //    layerMask = 1 << 5 | 1 << 7;

    //    Ray ray = Camera.main.ScreenPointToRay(MousePosition);

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



    //public void ImmediateLookAtMouse()
    //{
    //    RaycastHit rayhitInfo;
    //    if (IsMovableLayer("Ground", out rayhitInfo))
    //    {
    //        print("ImmediateLookAtMouse it's ground!");
    //        Vector3 lookAtPointOnSameY = new Vector3(rayhitInfo.point.x, transform.position.y, rayhitInfo.point.z);
    //        player.gameObject.transform.LookAt(lookAtPointOnSameY);
    //        //mousePos = lookAtPointOnSameY;
    //    }
    //}

    //Vector3 mousePos = Vector3.zero;
    //Ray mouseRay;
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(mousePos, 2);
    //    Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition));
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

    //bool CanBeTriggerd(string collideTag, out RaycastHit rayhitInfo)
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


    //public void JumpForward(float value)
    //{
    //    float dist = value;
    //    isOrderedToMove = true;
    //    TargetPose = transform.position + transform.forward * dist;
    //}



    //public void Stunned(bool isStunned, float stunnedTime)
    //{
    //    if (isStunned)
    //    {
    //        StartCoroutine(StunnedFor(stunnedTime));
    //    }
    //    else
    //    {
    //        isStunned = false;
    //    }
    //}

    //public IEnumerator StunnedFor(float time)
    //{
    //    isStunned = true;
    //    yield return new WaitForSeconds(time);
    //    isStunned = false;
    //}
    #endregion
}
