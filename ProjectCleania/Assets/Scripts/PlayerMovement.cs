using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float rotateCoef = 2f;

    private Animator playerAnimator;            // �ִϸ����� ������Ʈ
    private NavMeshAgent playerNavMeshAgent;    // path ��� ������Ʈ

    private BoxCollider attackBoxCollider;      // ���� �� ���̴� �ڽ� �ݶ��̴�

    private GameObject targetObj;               // ���� ���
    private Vector3 targetPose;                 // ��ǥ ��ġ

    public float distanceBetweenTargetObj = 1f;   // ���� ��, ��ǥ ��ġ �� �տ��� ���� ���ΰ�
    private bool isAttackPlaying;                 // ���� ����� ����ǰ� �ִ°�

    private void Awake()
    {
        // ������Ʈ �ҷ�����
        playerAnimator = GetComponent<Animator>();
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        attackBoxCollider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        // ���� �ʱ�ȭ
        attackBoxCollider.enabled = false;  // ���� �ݶ��̴� Off
        targetPose = transform.position;    // ��ǥ ��ġ�� ���� ��ġ
        isAttackPlaying = false;            // ���� �ִϸ��̼� ���� �� ���� 
    }

    void Update()
    {
        // ���� �ִϸ��̼��� �۵��ϰ� ���� ������ �׺���̼� �۵�
        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Marry_Attack_01_h") &&
            !playerAnimator.IsInTransition(0))
        {
            isAttackPlaying = false;
            playerNavMeshAgent.isStopped = false;
        }
        // ���� �ִϸ��̼��� �۵����̸� �׺���̼� stop
        if (isAttackPlaying)
        {
            playerNavMeshAgent.isStopped = true;
            return;
        }

        // ���콺 Ŭ���� ���� �׺���̼� ����
        ActivateNavigation();

        // ȸ�� ����
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
            //if (Physics.Raycast(ray, out mouseHit) && mouseHit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            int enemyLayerBitmask = 1 << LayerMask.NameToLayer("Enemy");
            if (Physics.Raycast(ray, out mouseHit, 20f, enemyLayerBitmask))
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
            // ���� �ٷ� ���̸� ������ �ʿ�x
            if (distanceBetweenTargetObj > Vector3.Distance(targetObj.transform.position, transform.position))
            {
                print("enemy front!");
                targetPose = transform.position;
                return;
            }

            // Ÿ�� ��ġ���� distanceBetweenTargetObj �ڿ��� �����.
            targetPose = targetObj.transform.position - Vector3.Normalize(targetObj.transform.position - transform.position) * distanceBetweenTargetObj;
        }

        // ���̰��̼� ����
        playerNavMeshAgent.SetDestination(targetPose);
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

        // ȸ��
        transform.LookAt(this.transform.position + rotateForward);
    }

    private void Attack()
    {
        // <Ÿ���� �ְ�, Ÿ�� ��ó�� �ְ�, Ÿ���� ��������> ���� 1ȸ ����
        if (targetObj != null && Vector3.Distance(transform.position, targetPose) < 0.01f)
        {
            // 1ȸ ���� �ǽ�
            playerAnimator.SetTrigger("NormalAttack");
            isAttackPlaying = true;

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
