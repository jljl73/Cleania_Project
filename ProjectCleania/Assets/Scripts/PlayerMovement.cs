using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float rotateCoef = 2f;

    private Animator playerAnimator;            // �ִϸ����� ������Ʈ
    private NavMeshAgent playerNavMeshAgent;    // path ��� ������Ʈ
    private Rigidbody playerRigidbody;          // ������ٵ� ������Ʈ

    private BoxCollider attackBoxCollider;      // ���� �� ���̴� �ڽ� �ݶ��̴�

    private GameObject targetObj;               // ���� ���
    private Vector3 targetPose;                 // ��ǥ ��ġ

    private NavMeshPath path;                   // ��ǥ���� path
    private int currentPathIdx;                 // path �� ���� ��ǥ ����

    private float distanceBetweenTargetObj = 1f;   // ���� ��, ��ǥ ��ġ �� �տ��� ���� ���ΰ�

    //private bool isAttackPlaying;
    //bool isAttacking;

    public PlayerSkillL skillL;
    public bool bAttacking = false;
    bool bChasing = false;
    public StateMachine playerStateMachine;


    private void Awake()
    {
        // ������Ʈ �ҷ�����
        playerAnimator = GetComponent<Animator>();
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        //attackBoxCollider = GetComponent<BoxCollider>();
        //playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // ���� �ʱ�ȭ
        //attackBoxCollider.enabled = false;  // ���� �ݶ��̴� Off
        targetPose = transform.position;    // ��ǥ ��ġ�� ���� ��ġ
                                            //isAttackPlaying = false;            // ���� �ִϸ��̼� ���� �� ���� 
    }

    void Update()
    {
        // ���� ���̸� ������Ʈ x
        //if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !playerAnimator.IsInTransition(0))
        //    isAttackPlaying = false;
        //if (isAttackPlaying)
        //    return;

        // ���콺 Ŭ���� ���� �׺���̼� ����
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        if (!EventSystem.current.IsPointerOverGameObject())
            ActivateNavigation();
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (!EventSystem.current.IsPointerOverGameObject(0))
            ActivateNavigation();
#endif
        // ȸ�� ����
        AccelerateRotation();

        // ���� ��� ����
        //Attack();

        // �ִϸ��̼� ������Ʈ
        playerAnimator.SetFloat("Speed", playerNavMeshAgent.velocity.magnitude);
    }

    private void ActivateNavigation()
    {
        if (playerStateMachine.State == StateMachine.enumState.Attacking)
        {
            targetPose = transform.position;
        }

        // ���콺 Ŭ����, �ش� ��ġ�� �̵�
        if (Input.GetMouseButton(0))// ������ �־
        {
            if (playerStateMachine.State == StateMachine.enumState.Idle || playerStateMachine.State == StateMachine.enumState.Chasing)
            {
                MoveToPosition();
                Targetting();
            }

#region
            //RaycastHit mouseHit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Ŭ���� ���� ���� ������, �� ��ġ�� �̵� �� ����
            //if (Physics.Raycast(ray, out mouseHit) && mouseHit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            //{
            //    // Ÿ��: ���콺 Ŭ���� ��
            //    targetObj = mouseHit.transform.gameObject;
            //}

            //else if (Physics.Raycast(ray, out mouseHit))
            //{
            //    // �Ϲ� �����̸�, Ÿ�� ����, ��ǥ ��ġ ����
            //    targetPose = mouseHit.point;
            //    targetObj = null;
            //}
#endregion
        }
        if (Input.GetMouseButton(1))
        {
            if (playerStateMachine.State == StateMachine.enumState.MoveAttack)
            {
                MoveToPosition();
            }
        }

#region
        //// Ÿ�� ������, Ÿ�� ���� ��ǥ ��ġ ����
        //if (targetObj != null)
        //{
        //    // ���� �ٷ� ���̸� ������ �ʿ�x
        //    if (distanceBetweenTargetObj > Vector3.Distance(targetObj.transform.position, transform.position))
        //    {
        //        targetPose = transform.position;
        //        return;
        //    }

        //    // Ÿ�� ��ġ���� distanceBetweenTargetObj �ڿ��� �����.
        //    targetPose = targetObj.transform.position - Vector3.Normalize(targetObj.transform.position - transform.position) * distanceBetweenTargetObj;
        //}
#endregion
        // ���̰��̼� ����

        if (playerStateMachine.State != StateMachine.enumState.Chasing)
        {
            playerNavMeshAgent.SetDestination(targetPose);
            // transform.LookAt(targetPose);
        }
        else
            playerNavMeshAgent.SetDestination(targetObj.transform.position);
    }

    private void AccelerateRotation()
    {
        Vector3 rotateForward; //= Vector3.zero;

        // Ÿ�� ������ ���� ȸ�� ���� ����
        if (targetObj != null)
        {
            rotateForward = Vector3.Normalize(targetObj.transform.position - transform.position);
            //rotateForward = targetObj.transform.position - transform.position;
        }
        else
        {
            rotateForward = Vector3.Normalize(targetPose - transform.position);
            //rotateForward = new Vector3(targetPose.x, transform.position.y, targetPose.z);
        }

        // ��ǥ ȸ�� ���� ����
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            rotateCoef * 180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        // ȸ��
        transform.LookAt(this.transform.position + limit);

        //transform.LookAt(rotateForward);
    }

#region
    //private void Attack()
    //{
    //    // Ÿ���� �ְ�, Ÿ�� ��ó�� ������ ���� 1ȸ ����
    //    if (targetObj != null && Vector3.Distance(transform.position, targetPose) < 0.01f)
    //    {
    //        // 1ȸ ���� �ǽ�
    //        playerAnimator.SetTrigger("Attack");
    //        isAttackPlaying = true;

    //        // �ѹ� ���� �� Ÿ�� = null, �ݺ� ���� ����
    //        targetObj = null;
    //    }
    //}

    //private void SetNormalAttack(GameObject _target, bool _isAttackColliderActivate)
    //{
    //    // Ÿ�� ������Ʈ
    //    targetObj = _target;

    //    // �Ϲ� ���� �ݶ��̴� ������Ʈ
    //    attackBoxCollider.enabled = _isAttackColliderActivate;
    //}
#endregion

    public void MoveToPosition()
    {
        RaycastHit[] raycastHits;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        raycastHits = Physics.RaycastAll(ray);
        targetPose = transform.position;

        for (int i = 0; i < raycastHits.Length; ++i)
        {

            if (raycastHits[i].transform.CompareTag("Ground"))
            {
                targetPose = raycastHits[i].point;
            }
        }
    }

    // ���� //
    // --- //

    public void JumpForward(float dist)
    {
        targetPose = transform.position + transform.forward * dist;
    }

    void Targetting()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bChasing = false;
        // ���� //
        targetObj = null;

        // --- //
        if (Physics.Raycast(ray, out raycastHit))
        {
            //Debug.Log(raycastHit.transform.tag);
            if (raycastHit.transform.CompareTag("Enemy"))
            {
                targetObj = raycastHit.transform.gameObject;
                bChasing = true;
                // ���� //
                // playerStateMachine.Transition(StateMachine.enumState.Chasing);
                // --- //
            }
        }

        // ���� //
        if (bChasing)
            playerStateMachine.Transition(StateMachine.enumState.Chasing);
        else
            playerStateMachine.Transition(StateMachine.enumState.Idle);
        // --- //
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerStateMachine.State == StateMachine.enumState.Chasing)
        // ������ �ֱ�, ���� �ݶ��̴��� trigger ������.
        {
            AccelerateRotation();

            //print(other.transform.name + "Collision");
            //transform.LookAt(targetObj.transform);
            targetPose = transform.position;
            skillL.AnimationActivate();
        }
    }
        
}
