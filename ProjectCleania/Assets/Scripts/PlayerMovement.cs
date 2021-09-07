using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private void Awake()
    {
        // ������Ʈ �ҷ�����
        playerAnimator = GetComponent<Animator>();
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        attackBoxCollider = GetComponent<BoxCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // ���� �ʱ�ȭ
        attackBoxCollider.enabled = false;  // ���� �ݶ��̴� Off
        targetPose = transform.position;    // ��ǥ ��ġ�� ���� ��ġ

        path = new NavMeshPath();           // path �Ҵ�
    }

    void Update()
    {
        // ���콺 Ŭ���� ���� ��ǥ ��ġ �� path ���
        ActivateNavigation();

        // path ����Ͽ� Move
        AccelerateRotation();

        // ���� ��� ����
        Attack();

        // �ִϸ��̼� ������Ʈ
        playerAnimator.SetFloat("Speed", playerNavMeshAgent.velocity.magnitude);
    }

    private void ActivateNavigation()
    {

        // ���콺 Ŭ����, �ش� ��ġ�� �̵�
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit mouseHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Ŭ���� ���� ���� ������, �� ��ġ�� �̵� �� ����
            if (Physics.Raycast(ray, out mouseHit) && mouseHit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // Ÿ��: ���콺 Ŭ���� ��
                targetObj = mouseHit.transform.gameObject;
            }

            else if (Physics.Raycast(ray, out mouseHit))
            {
                // �Ϲ� �����̸�, Ÿ�� ����, ��ǥ ��ġ ����
                targetPose = mouseHit.point;
                targetObj = null;
            }

        }

        // Ÿ�� ������, Ÿ�� ���� ��ǥ ��ġ ����
        if (targetObj != null)
        {
            // Ÿ�� ��ġ���� distanceBetweenTargetObj �ڿ��� �����.
            targetPose = targetObj.transform.position - Vector3.Normalize(targetObj.transform.position - transform.position) * distanceBetweenTargetObj;
        }

        // path ���
        playerNavMeshAgent.SetDestination(targetPose);
        if(!playerNavMeshAgent.CalculatePath(targetPose, path))
        {
            print("path ã�� ����");
        }
    }

    private void AccelerateRotation()
    {
        Vector3 rotateForward = Vector3.zero;

        // Ÿ�� ������ ���� ȸ�� ���� ����
        if (targetObj != null)
            rotateForward = Vector3.Normalize(targetObj.transform.position - transform.position);
        else
            rotateForward = Vector3.Normalize(targetPose - transform.position);

        // ��ǥ ȸ�� ���� ����
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);
        rotateForward = Vector3.Slerp(transform.forward,
                                      rotateForward,
                                      rotateCoef / Vector3.Angle(transform.forward, rotateForward));

        // ȸ��
        transform.LookAt(this.transform.position + rotateForward);
    }

    private void Attack()
    {
        // Ÿ���� �ְ�, Ÿ�� ��ó�� ������ ���� 1ȸ ����
        if (targetObj != null && Vector3.Distance(transform.position, targetPose) < 0.01f)
        {
            // 1ȸ ���� �ǽ�
            playerAnimator.SetTrigger("Attack");

            // �ѹ� ���� �� Ÿ�� = null, �ݺ� ���� ����
            targetObj = null;
        }
    }

    private void SetNormalAttack(GameObject _target, bool _isAttackColliderActivate)
    {
        // Ÿ�� ������Ʈ
        targetObj = _target;

        // �Ϲ� ���� �ݶ��̴� ������Ʈ
        attackBoxCollider.enabled = _isAttackColliderActivate;
    }

    // ���� �ִϸ��̼ǿ��� ȣ���ϴ� �Լ�
    void ActivateEventAboutCollider(float _t)
    {
        StartCoroutine("ActivateAttakCollider", _t);
    }

    IEnumerator ActivateAttakCollider(float _t)
    {
        // �Ϲ� ���� �ݶ��̴� On
        attackBoxCollider.enabled = true;

        yield return new WaitForSeconds(_t);

        // �ѹ� ���� ��,
        // �Ϲ� ���� �ݶ��̴�: Off
        attackBoxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ������ �ֱ�, ���� �ݶ��̴��� trigger ������.
        print(other.name + "damaged");
    }
}
