using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ��� : ���� ��Ȳ�� ���� �ڽ��� ���¸� �����ϰ� �׿� ���� �ൿ��
public class EnemyAI : MonoBehaviour
{
    // ���� : Idle, Chase, Attack, Hurt, Die
    // Idle : Idle �ִϸ��̼��� �����ϸ� �������� �ʴ´�. 
    //      - ���� �Ÿ� �ֺ��� �����ϸ� �÷��̾ �ִ��� Ȯ���Ѵ�. 
    //      - ���� �Ÿ� ���� �÷��̾ ������ chase�� ���� �̵�
    // Chase : ã�� �÷��̾ �����Ѵ�. 
    //      - ������ �� �ִ� ���� ���� �÷��̾ ������ Attack���� ���� �̵�(���Ÿ� ���� ����, �ٰŸ� ���� ����(�ݶ��̴��� ���� ��))
    //      - �þ� ������ �÷��̾ ������� �ٽ� Idle ���·� ���ư���
    //      - (����) chase ��, ����� �� �ִ� �߰� �̵���(���� ���� ��)�� ���� Ȯ�� ������ �� �ְ� ���
    // Attack : �÷��̾ ���� �ֱ�� �����Ѵ�
    //      - ���Ÿ� / �ٰŸ� ���� �Ǵ� �˰����� ���� ����
    //      - ���Ÿ��� �÷��̾� ���� ���(���Ÿ� ���� ���� ��) ���Ÿ� ���� ����. �ٰŸ��� ���� ���(�ٰŸ� ���� ���� ��) �ٰŸ� ���� ����
    // Hurt : ������ ������ hurt �ִϸ��̼� ���� �� Idle ����
    // Die : ������, ���� �ִϸ��̼� ���� �� �׺� �Ž� ������Ʈ Off


    // Additional idea
    // - �ٰŸ��� ���� �� bIsPlayerNear = true �ؼ�, ���Ÿ� ���� ���ϰ� ����
    // - ���Ÿ� ���� ���� ������ ��, ���� ��ġ�� ���� ��ġ�� ����

    // ���� ����
    // 1. ���� ���� ���� ���� Idle -> chase, �ܼ� ���� ��, �ٰŸ� ���� ���� ���� ���� Attack, ���� ������ hurt, ������ Die  // 9�� 10�� ��
    // 2. AbilityStatus �����Ͽ� �����ϰ� Hurt -> Die ����                                                              // 9�� 11�� ��
    // 3. ���Ÿ� ���� ���� ���� & ���Ÿ�, �ٰŸ� ���� �˰��� ����                                                       // 9�� 11, 12�� �� ��


    private NavMeshAgent enemyNavMeshAgent; // �� �׺� �޽� ������Ʈ ������Ʈ 
    private Animator enemyAnimator;         // �� �ִϸ�����

    public float searchDistance = 3f;               // ���� ��� ����
    private Transform targetTransform = null;       // ��ǥ�� transform

    private Vector3 initialPosition;                // �ʱ� ��ġ
    private Vector3 initialDirection;               // �ʱ� ����
    private Vector3 targetPosition;                 // ��ǥ ��ġ

    public float attackPeriod = 1f;             // ���� �ֱ�
    private float lastAttackTime;               // ������ ���� ����

    private bool bIsAttaking = false;           // ���� ����
    private bool bIsDead = true;

    private bool hasTarget
    {
        get
        {
            if (targetTransform != null)
                return true;
            else
                return false;
        }
    }

    private void Awake()
    {
        // ������Ʈ �ʱ�ȭ
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        print("OnEnable called");
        enemyNavMeshAgent.enabled = true;       // �׺���̼� Ȱ��ȭ
        enemyNavMeshAgent.isStopped = false;
        initialPosition = transform.position;   // �ʱ� ��ġ ����
        initialDirection = transform.forward;   // �ʱ� ���� ����
        lastAttackTime = 0f;                    // ������ ���� ���� �ʱ�ȭ
        bIsAttaking = false;                    // ���� ���� �ʱ�ȭ
        bIsDead = false;                        // ���� ���� �ʱ�ȭ

        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;    // �ݶ��̴� Ȱ��ȭ
        }
    }

    void Update()
    {
        if (bIsDead) return;

        // �ִϸ��̼� ����
        enemyAnimator.SetFloat("Speed", enemyNavMeshAgent.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (bIsDead) return;

        // ������Ʈ ã��
        FindObj();

        // �׺���̼� ����
        if (enemyNavMeshAgent.enabled == true)
            enemyNavMeshAgent.SetDestination(targetPosition);
    }

    void FindObj()
    {
        if (hasTarget)
        {
            // Ÿ���� ���� �Ÿ��� ���� ���� �Ǵ�
            if (Vector3.Distance(transform.position, targetTransform.position) > searchDistance)
                targetTransform = null;
            else
            {
                if (bIsAttaking) return;
                targetPosition = targetTransform.position;
            }
        }
        else
        {
            // �� ��ġ���� searchDistance ũ���� �� �ȿ� Player�� �ִ��� Ȯ��
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchDistance);
            bool targetFound = false;
            foreach (Collider collider in colliders)
            {
                if (collider.transform.CompareTag("Player"))
                {
                    targetTransform = collider.transform;
                    targetFound = true;
                    break;
                }
            }

            // �� �ȿ� �� ������, ���� ��ġ�� ��ǥ ��ġ
            if (targetFound)
                return;
            else
                targetPosition = initialPosition;
        }

    }

    public void Die()
    {
        bIsDead = true;

        // ���� �ִϸ��̼� Ȱ��ȭ
        enemyAnimator.SetTrigger("Die");

        // �׺���̼� ��Ȱ��ȭ
        enemyNavMeshAgent.isStopped = true;
        enemyNavMeshAgent.enabled = false;
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        // ��Ȱ ���. ���� ������ 5�� �� ��Ȱ. ���ֵ� �˴ϴ�:)
        StartCoroutine("Revival");
    }

    IEnumerator Revival()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }

    private void AccelerateRotation()
    {
        Vector3 rotateForward = Vector3.zero;

        // Ÿ�� ������ ���� ȸ�� ���� ����
        if (targetTransform != null)
            rotateForward = Vector3.Normalize(targetTransform.position - transform.position);
        else
            rotateForward = Vector3.Normalize(targetPosition - transform.position);

        // ��ǥ ȸ�� ���� ����
        rotateForward = Vector3.ProjectOnPlane(rotateForward, Vector3.up);

        // ȸ��

        transform.LookAt(this.transform.position + rotateForward);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            bIsAttaking = true;

            // �ݶ��̴��� �÷��̾�� �ε����� ����
            targetPosition = transform.position;

            // ȸ�� ����
            AccelerateRotation();

            // ���� �ֱⰡ ������ �� ����
            if (Time.time > lastAttackTime + attackPeriod)
            {
                enemyAnimator.SetTrigger("SlashUp2DownAttack");
                lastAttackTime = Time.time;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            bIsAttaking = false;
        }
    }
}