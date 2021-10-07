using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TestPlayerMove : MonoBehaviour, IPointerEnterHandler
{
    public float rotateCoef = 2f;

    private Animator playerAnimator;            // �ִϸ����� ������Ʈ
    private NavMeshAgent playerNavMeshAgent;    // path ��� ������Ʈ
    private Rigidbody playerRigidbody;          // ������ٵ� ������Ʈ

    private BoxCollider attackBoxCollider;      // ���� �� ���̴� �ڽ� �ݶ��̴�

    private GameObject targetObj;               // ���� ���
    private Vector3 targetPos;                 // ��ǥ ��ġ

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
        //playerNavMeshAgent = GetComponent<NavMeshAgent>();
        //attackBoxCollider = GetComponent<BoxCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // ���� �ʱ�ȭ
        //attackBoxCollider.enabled = false;  // ���� �ݶ��̴� Off
        targetPos = transform.position;    // ��ǥ ��ġ�� ���� ��ġ
        //isAttackPlaying = false;            // ���� �ִϸ��̼� ���� �� ���� 
    }

    void Update()
    {
        //�����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Physics.IgnoreLayerCollision(3, 6);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Physics.IgnoreLayerCollision(3, 6, false);
        }



        Move();
        //AccelerateRotation();
        // �ִϸ��̼� ������Ʈ
        //playerAnimator.SetFloat("Speed", playerNavMeshAgent.velocity.magnitude);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(targetPos, transform.position) < 0.01f) return;
        if (playerStateMachine.State != StateMachine.enumState.Idle &&
            playerStateMachine.State != StateMachine.enumState.MoveAttack) return;


        transform.localPosition = Vector3.MoveTowards(transform.position, targetPos, 5 * Time.deltaTime);
        AccelerateRotation();
    }

    private void ActivateNavigation()
    {
        if (playerStateMachine.State == StateMachine.enumState.Attacking)
        {
            targetPos = transform.position;
        }

        if (Input.GetMouseButton(0))// ������ �־
        {
            if (playerStateMachine.State == StateMachine.enumState.Idle || playerStateMachine.State == StateMachine.enumState.Chasing)
            {
                MoveToPosition();
                Targetting();
            }

        }
        if (Input.GetMouseButton(1))
        {
            if (playerStateMachine.State == StateMachine.enumState.MoveAttack)
            {
                MoveToPosition();
            }
        }

        if (playerStateMachine.State != StateMachine.enumState.Chasing)
        {
            playerNavMeshAgent.SetDestination(targetPos);
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
            rotateForward = Vector3.Normalize(targetPos - transform.position);
            //rotateForward = new Vector3(targetPose.x, transform.position.y, targetPose.z);
        }

        // ��ǥ ȸ�� ���� ����
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        Vector3 limit = Vector3.Slerp(transform.forward, rotateForward,
            rotateCoef * 180.0f * Time.deltaTime / Vector3.Angle(transform.forward, rotateForward));

        // ȸ��
        transform.LookAt(this.transform.position + limit);
    }


    public void MoveToPosition()
    {
        int layerMask = 0;
        layerMask = 1 << 5 | 1 << 7;


        //Debug.Log(EventSystem.current.currentSelectedGameObject);
        //if (EventSystem.current.IsPointerOverGameObject(-1)) return;


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 500.0f, layerMask))
        {
            if (hit.collider.tag == "Ground")
            {
                targetPos = hit.point;
            }
        }
    }

    public void JumpForward(float dist)
    {
        targetPos = transform.position + transform.forward * dist;
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
            targetPos = transform.position;
            skillL.AnimationActivate();
        }
    }


    void Move()
    {
        if (Input.GetMouseButton(0))// ������ �־
        {
            if (playerStateMachine.State == StateMachine.enumState.Idle ||
                playerStateMachine.State == StateMachine.enumState.Chasing)
            {
                MoveToPosition();
                //Targetting();
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (playerStateMachine.State == StateMachine.enumState.MoveAttack)
                MoveToPosition();
        }


        if (Vector3.Distance(targetPos, transform.position) < 0.01f)
            playerAnimator.SetFloat("Speed", 0);
        else
            playerAnimator.SetFloat("Speed", 10);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }
}
