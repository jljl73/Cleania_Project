using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    AbilityStatus pitcherStatus;
    public AbilityStatus PitcherStatus { get { return pitcherStatus; } set { pitcherStatus = value; } }

    public float moveSpeed = 5.0f;

    // "���� �������� 5.4 = 540%
    float skillScale = 5.4f;

    // "���� �ǰ�ü�� ���� �ִ� ���� ���� Ƚ��"
    float maxHitPerSameObject = 2;

    // "���� �ð�"
    float duration = 2f;

    bool canTakeDamage = true;

    void Start()
    {
        Invoke("OnOffCollider", 1.0f);
        Destroy(gameObject, duration);
    }
    
    public void ResetSetting(float maxHitPerSameObject, float projectileDamageRatePerSec, float duration)
    {
        this.skillScale = projectileDamageRatePerSec;
        this.maxHitPerSameObject = maxHitPerSameObject;
        this.duration = duration;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AbilityStatus enemyAbil = other.GetComponent<Enemy>().abilityStatus;

            if (enemyAbil.HP != 0)
                enemyAbil.AttackedBy(pitcherStatus, skillScale * Time.fixedDeltaTime);
        }
    }

    void OnOffCollider()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        collider.enabled = true;
    }
}
