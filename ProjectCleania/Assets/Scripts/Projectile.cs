using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    void Start()
    {
        Destroy(gameObject, 2.0f);
    }
    

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    // 수정 //
    void GiveDamage(Collider other)
    {
        // 부딛힌 콜라이더에게 데미지 입히기
        AbilityStatus hitObjStatus = other.GetComponent<AbilityStatus>();
        if (hitObjStatus == null)
        {
            Debug.Log("No AbilityStatus on hitObj");
            return;
        }
        // 적에게 데미지 입히기
        AbilityStatus parentStatus = GetComponentInParent<AbilityStatus>();
        if (parentStatus == null)
        {
            Debug.Log("No AbilityStatus on parent");
            return;
        }

        hitObjStatus.AttackedBy(parentStatus, 1f);
    }
    // --- //

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //other.GetComponent<EnemyState>().Damaged();

            // 수정 //
            GiveDamage(other);
            // --- //

            Destroy(gameObject);

        }
    }
}
