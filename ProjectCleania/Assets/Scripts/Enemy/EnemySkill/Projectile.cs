using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AbilityStatus abilityStatus;
    public float moveSpeed = 5.0f;
    public float skillScale = 5.4f;

    void Start()
    {
        Invoke("OnOffCollider", 1.0f);
        Destroy(gameObject, 2.0f);
    }
    

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Enemy")
        //{
        //    if (other.GetComponent<Enemy>().abilityStatus.AttackedBy(abilityStatus, skillScale) == 0)
        //        other.GetComponent<Enemy>().Die();
        //}
    }

    void OnOffCollider()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        collider.enabled = true;
    }

}
