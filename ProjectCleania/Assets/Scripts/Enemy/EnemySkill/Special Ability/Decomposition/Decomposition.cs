using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Decomposition : DamagingProperty
{
    float existTime = 7f;
    float speed = 0.5f;
    float explodeWaitTime = 3f;
    float explodeDamageRange = 5f;
    float stunTime = 2f;
    GameObject target;

    bool isSetUp = false;
    bool isExploding = false;

    NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        if (nav == null)
            throw new System.Exception("Decomposition doesnt have nav");
    }

    private void Update()
    {
        if (!isSetUp) return;

        nav.SetDestination(target.transform.position);
    }

    public void SetUp(float existTime, float speed, float explodeWaitTime, float explodeDamageRange, float stunTime, GameObject target)
    {
        this.existTime = existTime;
        this.speed = speed;
        this.explodeWaitTime = explodeWaitTime;
        this.explodeDamageRange = explodeDamageRange;
        this.stunTime = stunTime;
        this.target = target;

        isSetUp = true;
        nav.speed = speed;
        Invoke("ReadyToExplode", existTime);
    }

    void ReadyToExplode()
    {
        // ����
        nav.isStopped = true;
        isExploding = true;
        Invoke("Explode", explodeWaitTime);
    }

    void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, explodeDamageRange);
        foreach (Collider collider in overlappedColliders)
        {
            if (collider.CompareTag("Player"))
            {
                // ���� ������!
                print("�÷��̾�� ���� ������!");
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 2�� ���� �ο�
            print("�÷��̾� 2�� ����!");

            // ����
            if (isExploding) return;
            Explode();
        }
    }
}
