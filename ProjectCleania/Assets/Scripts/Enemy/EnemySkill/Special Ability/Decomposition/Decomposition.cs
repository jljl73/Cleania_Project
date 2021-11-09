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

    bool isExploding = false;

    NavMeshAgent nav;

    [SerializeField]
    SkillEffectController effectController;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        if (nav == null)
            throw new System.Exception("Decomposition doesnt have nav");
    }

    private void Start()
    {
        effectController.Scale = damageRange * 0.76923f;
    }

    private void Update()
    {
        if (!isSetUp) return;

        nav.SetDestination(target.transform.position);
    }

    public void SetUp(float existTime, float speed, float explodeWaitTime, float explodeDamageRange, float stunTime, GameObject target, AbilityStatus abil, float damageScale)
    {
        this.existTime = existTime;
        this.speed = speed;
        this.explodeWaitTime = explodeWaitTime;
        this.explodeDamageRange = explodeDamageRange;
        this.stunTime = stunTime;
        this.target = target;

        base.SetUp(abil, damageScale);

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
                AbilityStatus abil = collider.gameObject.GetComponent<AbilityStatus>();
                if (abil != null)
                    abil.AttackedBy(ownerAbility, damageScale);
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 2�� ���� �ο�
            print("2�� ���� �ο�!");
            other.gameObject.GetComponent<Player>().OnStunned(true, 2);

            // ����
            if (isExploding) return;
            Invoke("Explode", explodeWaitTime);
        }
    }
}
