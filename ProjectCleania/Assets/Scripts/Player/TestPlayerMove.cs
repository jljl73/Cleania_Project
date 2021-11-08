using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TestPlayerMove : MonoBehaviour, IStunned
{
    [Header("움직임 사용 스킬")]
    public PlayerSkillRefreshingLeapForward LeapForwardSkill;

    GameObject playerObject;
    Player player;
    Animator playerAnimator;            // 애니메이터 컴포넌트
    BoxCollider attackBoxCollider;      // 공격 시 쓰이는 박스 콜라이더

    GameObject targetObj;               // 공격 대상
    Vector3 targetPos;                 // 목표 위치
    RaycastHit hit;

    [Header("회전 계수")]
    public float rotateCoef = 360f;
    // public PlayerSkillL skillL;
    // public bool bAttacking = false;
    // bool bChasing = false;
    float speed = 1.0f;

    bool isOrderedToMove = false;
    bool isStunned = false;

    private void Awake()
    {
        playerObject = transform.gameObject;
        player = GetComponent<Player>();
        playerAnimator = GetComponent<Animator>();
        targetPos = transform.position;
    }

    void FixedUpdate()
    {
        if (player.stateMachine.CompareState(StateMachine.enumState.Dead) ||
            player.stateMachine.CompareState(StateMachine.enumState.Attacking)) return;

        if (!isOrderedToMove || isStunned)
        {
            targetPos = transform.position;
            return;
        }

        if (Vector3.Distance(targetPos, transform.position) < 0.2f)
        {
            playerAnimator.SetBool("Walk", false);
            targetPos = transform.position;
            isOrderedToMove = false;
            return;
        }

        speed = player.abilityStatus.GetStat(Ability.Stat.MoveSpeed) * 6;
        playerObject.transform.localPosition = Vector3.MoveTowards(playerObject.transform.position, targetPos, speed * Time.deltaTime);
        AccelerateRotation();
    }

    private void AccelerateRotation()
    {
        Vector3 rotateForward; //= Vector3.zero;

        // 타겟 유무에 따른 회전 벡터 결정
        if (targetObj != null)
        {
            rotateForward = Vector3.Normalize(targetObj.transform.position - transform.position);
        }
        else
        {
            rotateForward = Vector3.Normalize(targetPos - transform.position);
        }

        // 목표 회전 벡터 결정
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            rotateCoef * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        // 회전
        playerObject.transform.LookAt(this.transform.position + limit);
    }

    public void Move(Vector3 MousePosition)
    {
        isOrderedToMove = true;
        MoveToPosition(MousePosition);
    }

    void MoveToPosition(Vector3 MousePosition)
    {
        int layerMask = 0;
        layerMask = 1 << 5 | 1 << 7;

        Ray ray = Camera.main.ScreenPointToRay(MousePosition);

        if (Physics.Raycast(ray, out hit, 500.0f, layerMask))
        {
            if (hit.collider.tag == "Ground")
            {
                targetPos = hit.point;
                //print("Ground Hit");
            }
            else if (hit.collider.CompareTag("Enemy"))
                targetPos = hit.collider.transform.position;
        }

        if (Vector3.Distance(targetPos, transform.position) > 0.01f)
        {
            playerAnimator.SetBool("Walk", true);
        }
    }

    public void StopMoving()
    {
        targetPos = transform.position;
    }

    void Targetting()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // bChasing = false;
        // 수정 //
        targetObj = null;

        // --- //
        if (Physics.Raycast(ray, out raycastHit))
        {
            //Debug.Log(raycastHit.transform.tag);
            if (raycastHit.transform.CompareTag("Enemy"))
            {
                targetObj = raycastHit.transform.gameObject;
                // bChasing = true;
            }
        }
    }

    public void ImmediateLookAtMouse()
    {
        RaycastHit rayhitInfo;
        if (CanBeTriggerd("Ground", out rayhitInfo))
        {
            print("ImmediateLookAtMouse it's ground!");
            Vector3 lookAtPointOnSameY = new Vector3(rayhitInfo.point.x, transform.position.y, rayhitInfo.point.z);
            player.gameObject.transform.LookAt(lookAtPointOnSameY);
            //mousePos = lookAtPointOnSameY;
        }
    }

    //Vector3 mousePos = Vector3.zero;
    //Ray mouseRay;
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(mousePos, 2);
    //    Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition));
    //}

    bool CanBeTriggerd(string collideTag, out RaycastHit rayhitInfo)
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


    //public void JumpForward(float value)
    //{
    //    float dist = value;
    //    isOrderedToMove = true;
    //    targetPos = transform.position + transform.forward * dist;
    //}

    public void LeapForwardSkillJumpForward()
    {
        print("LeapForwardSkillJumpForward!");
        if (LeapForwardSkill == null)
            throw new System.Exception("TestPlayerMove dosent have LeapForwardSkillJumpForward");
        float dist = LeapForwardSkill.GetJumpDistance();
        isOrderedToMove = true;
        targetPos = transform.position + transform.forward * dist;
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
